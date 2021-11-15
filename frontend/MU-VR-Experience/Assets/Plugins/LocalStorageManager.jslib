mergeInto(LibraryManager.library, {
    GetFromStorage: function(key) {
        var localStorage = window.localStorage;
        return localStorage.getItem(key);
    },

    WriteToStorage: function(key, value) {
        var localStorage = window.localStorage;
        localStorage.setItem(key, value);
    },

    DeleteFromStorage: function(key) {
        var localStorage = window.localStorage;
        localStorage.removeItem(key);
    },
});