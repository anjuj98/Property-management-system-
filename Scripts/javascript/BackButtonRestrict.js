
//Preventing back button by setting time out of zero seconds
function preventback() {
    window.history.forward();
}

setTimeout(function () {
    preventback();
}, 0);

window.onunload = function () {
    null
};
