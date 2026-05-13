const DATABASE_NAME = "ExpenseTracker";
const DATABASE_VERSION = 1;
const STORE_NAME = "expenses";

function openDatabase() {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(DATABASE_NAME, DATABASE_VERSION);

        request.onupgradeneeded = event => {
            const database = event.target.result;

            if (!database.objectStoreNames.contains(STORE_NAME)) {
                const store =
                    database.createObjectStore(STORE_NAME, { keyPath: "id" });
            }
        };

        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject(request.error);
    });
}

function completeTransaction(database, transaction, getResult) {
    return new Promise((resolve, reject) => {
        transaction.oncomplete = () => {
            database.close();
            resolve(getResult());
        };

        transaction.onerror = () => {
            database.close();
            reject(transaction.error);
        };
    });
}

export async function getExpenses() {
    const database = await openDatabase();
    const transaction = database.transaction(STORE_NAME, "readonly");
    const store = transaction.objectStore(STORE_NAME);
    const request = store.getAll();

    return completeTransaction(database, transaction, () => request.result);
}

export async function getExpense(id) {
    const database = await openDatabase();
    const transaction = database.transaction(STORE_NAME, "readonly");
    const store = transaction.objectStore(STORE_NAME);
    const request = store.get(id);

    return completeTransaction(database, transaction, () => request.result);
}

export async function saveExpense(expense) {
    const database = await openDatabase();
    const transaction = database.transaction(STORE_NAME, "readwrite");
    const store = transaction.objectStore(STORE_NAME);

    if (!expense.id || expense.id === "00000000-0000-0000-0000-000000000000") {
        expense.id = crypto.randomUUID();
    }

    store.put(expense);

    return completeTransaction(database, transaction, () => expense);
}

export async function deleteExpense(id) {
    const database = await openDatabase();
    const transaction = database.transaction(STORE_NAME, "readwrite");
    const store = transaction.objectStore(STORE_NAME);

    store.delete(id);

    return completeTransaction(database, transaction, () => undefined);
}
