mergeInto(LibraryManager.library, {
    GetFromStorage: function(key) {
        let localStorage = window.localStorage;
        return localStorage.getItem(key);
    },

    WriteToStorage: function(key, value) {
        let localStorage = window.localStorage;
        localStorage.setItem(key, value);
    },

    DeleteFromStorage: function(key) {
        let localStorage = window.localStorage;
        localStorage.removeItem(key);
    },
});