function preventback() {
    window.history.forward();
}

setTimeout(function () {
    preventback();
}, 0);

window.onunload = function () {
    null
};
