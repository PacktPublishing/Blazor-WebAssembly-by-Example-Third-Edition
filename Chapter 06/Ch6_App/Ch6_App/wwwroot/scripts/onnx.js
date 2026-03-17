let session = null;
let vocabMap = null;

export async function initOnnx() {
    try {
        await loadVocabMap();
        const modelPath = "./models/model.onnx";
        session = await ort.InferenceSession.create(modelPath, {
            executionProviders: ['wasm']
        });

        console.log('Model loaded:', modelPath);
        console.log('Input names:', session.inputNames);
        console.log('Output names:', session.outputNames);
        return true;

    } catch (error) {
        console.error('Model load failed:', error);
        return false;
    }
}

async function loadVocabMap() {
    const vocab = await fetch("./models/vocab.txt");
    const text = await vocab.text();
    const lines = text.split(/\r?\n/);

    vocabMap = new Map();
    for (let i = 0; i < lines.length; i++) {
        const token = lines[i];
        if (!token) continue;
        vocabMap.set(token, BigInt(i));
    }
    console.log(`Vocab loaded (${vocabMap.size} tokens)`);
}
function tokenize(text) {
    console.log('Tokenizing text:', text);

    const cleanedText = text
        .toLowerCase()
        .replace(/[^a-z0-9\s\-']/g, ' ')  // Keep letters, numbers, spaces, hyphens, apostrophes
        .replace(/\s+/g, ' ')              // Normalize multiple spaces to single space
        .trim();
    console.log('Cleaned text:', cleanedText);

    var words = cleanedText.split(/\s+/).filter(token => token.length > 0);
    console.log('Words:', words);

    const tokens = ['[CLS]'];
    for (let word of words) {
        tokens.push(...tokenizeWord(word));
    }
    tokens.push('[SEP]');
    console.log('Tokens:', tokens);

    var finalTokens = tokens.map(token =>
        vocabMap.has(token) ? vocabMap.get(token) : vocabMap.get("[UNK]"));
    console.log('Token IDs:', finalTokens);
    return finalTokens;
}

function tokenizeWord(word) {
    if (vocabMap.has(word)) return [word];

    // WordPiece tokenization: greedy longest-match first
    const subTokens = [];
    let start = 0;

    while (start < word.length) {
        let end = word.length;
        let foundToken = null;

        // Try progressively shorter substrings
        while (start < end) {
            let substr = word.substring(start, end);

            // Add ## prefix for continuation tokens (not the first subtoken)
            if (start > 0) {
                substr = '##' + substr;
            }

            if (vocabMap.has(substr)) {
                foundToken = substr;
                break;
            }
            end--;
        }

        if (foundToken) {
            subTokens.push(foundToken);
            start = end;
        } else {
            console.warn(`No valid subtoken found for "${word}" at position ${start}`);
            subTokens.push('[UNK]');
            break;
        }
    }

    console.log(`Tokenized "${word}" to:`, subTokens);
    return subTokens;
}

export async function runInference(text) {
    if (!session || !vocabMap) {
        throw new Error('Model not initialized. Call initOnnx() first.');
    }

    const tokens = tokenize(text);

    const inputIds = new BigInt64Array(tokens);
    const attentionMask = new BigInt64Array(tokens.length).fill(1n);
    const tokenTypeIds = new BigInt64Array(tokens.length).fill(0n);

    const feeds = {
        input_ids: new ort.Tensor('int64', inputIds, [1, tokens.length]),
        attention_mask: new ort.Tensor('int64', attentionMask, [1, tokens.length]),
        token_type_ids: new ort.Tensor('int64', tokenTypeIds, [1, tokens.length])
    };

    const results = await session.run(feeds);
    const out = results['last_hidden_state'];

    const [batchSize, sequenceLength, embeddingSize] = out.dims;
    console.log(`Batch size: ${batchSize}, Sequence length: ${sequenceLength}, Embedding size: ${embeddingSize}`);

    const pooledEmbedding = meanPooling(out.data, sequenceLength, embeddingSize, attentionMask);
    return pooledEmbedding;
}

function meanPooling(tokenEmbeddings, sequenceLength, embeddingSize, attentionMask) {
    // Initialize the pooled embedding
    const pooled = new Array(embeddingSize).fill(0);

    // Sum all token embeddings (weighted by attention mask)
    let validTokenCount = 0;
    for (let tokenIdx = 0; tokenIdx < sequenceLength; tokenIdx++) {
        const maskValue = Number(attentionMask[tokenIdx]); // Convert BigInt to number

        if (maskValue === 1) {
            validTokenCount++;
            for (let dim = 0; dim < embeddingSize; dim++) {
                const index = tokenIdx * embeddingSize + dim;
                pooled[dim] += tokenEmbeddings[index];
            }
        }
    }

    // Average by dividing by number of valid tokens
    for (let dim = 0; dim < embeddingSize; dim++) {
        pooled[dim] /= validTokenCount;
    }

    console.log(`Mean pooling: averaged ${validTokenCount} token embeddings`);
    return pooled;
}

export async function compare(referenceText, candidateText) {
    // Get embeddings for two sentences
    const embedding1 = await runInference(referenceText);
    const embedding2 = await runInference(candidateText);

    // Compare them
    const similarity = cosineSimilarity(embedding1, embedding2);

    return similarity;
}

function cosineSimilarity(vecA, vecB) {
    if (vecA.length !== vecB.length) {
        throw new Error(`Vector dimensions mismatch: ${vecA.length} vs ${vecB.length}`);
    }

    // Dot product: sum(a[i] * b[i])
    const dotProduct = vecA.reduce((sum, a, i) => sum + a * vecB[i], 0);

    // Magnitudes using efficient Math.hypot()
    const magnitudeA = Math.hypot(...vecA);
    const magnitudeB = Math.hypot(...vecB);

    if (magnitudeA === 0 || magnitudeB === 0) return 0;

    return dotProduct / (magnitudeA * magnitudeB);
}

export async function compareCandidates(query, candidatesWithEmbeddings) {
    const queryEmbedding = await runInference(query);

    const scores = [];
    for (const candidate of candidatesWithEmbeddings) {
        const candidateEmbedding = candidate.embeddings;
        const score = cosineSimilarity(queryEmbedding, candidateEmbedding);
        scores.push({ category: candidate.category, score });
    }
    console.log('Similarity scores:', scores);
    return scores.sort((a, b) => b.score - a.score);
}

export async function dispose() {
    if (session) {
        await session.release();
        session = null;
    }
    vocabMap?.clear();
    vocabMap = null;
    embeddingCache?.clear();
}