export function getLanguageCulture() {
    return window.localStorage['LanguageCulture'];
};
export function setLanguageCulture(value) {
    window.localStorage['LanguageCulture'] = value;
};