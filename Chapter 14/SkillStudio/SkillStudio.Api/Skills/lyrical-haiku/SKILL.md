---
name: lyrical-haiku
description: Transform a single sentence into a lyrical three-line haiku that preserves the original emotional core through vivid imagery and graceful language.
---

# Lyrical Haiku

Use when
- the user provides a sentence, short thought, or brief emotional statement and wants it rewritten as a lyrical haiku

Do not use when
- the user wants a literal rewrite, summary, or paraphrase

## Goal
Convert one sentence into a three-line haiku that feels poetic, musical, and emotionally faithful to the source.

## Instructions
1. Read the input and identify the central emotion, image, or moment.
2. Remove nonessential detail and keep the strongest emotional thread.
3. Translate abstract ideas into concrete sensory imagery when possible.
4. Write a three-line haiku with natural rhythm and graceful phrasing.
5. Prefer elegance and clarity over rigid literal accuracy.
6. Output only the final haiku text.

## Output
Return only the haiku.

The response must:
- Be exactly three lines
- Aim for a 5-7-5 syllable structure when reasonably possible
- Preserve the source sentence's main feeling, image, or meaning
- Use vivid, lyrical, sensory language
- Contain no title, no labels, no quotation marks, and no explanation

## Style rules
- Favor imagery involving weather, light, silence, memory, distance, wind, water, shadow, motion, or season when it fits naturally
- Keep the tone lyrical, restrained, and emotionally clear
- Avoid clichés when possible
- Avoid archaic or ornamental diction unless the user explicitly asks for it
- Do not explain choices or summarize the source sentence
- Do not add surrounding prose

## Examples
### Example 1
**Input**

```text
I miss you more every time it rains.
```

**Output**

```text
Rain on the window
Your name moves through the dim room
Longer than the storm
```

### Example 2
**Input**

```text
I finally feel at peace after a difficult year.
```

**Output**

```text
Winter light grows warm
The old ache loosens its grip
Spring stays in my chest
```