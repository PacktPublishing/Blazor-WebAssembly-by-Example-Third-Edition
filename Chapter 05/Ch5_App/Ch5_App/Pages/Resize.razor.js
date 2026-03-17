let dotNetHelper;
let resizeHandler;

export function registerResizeHandler(dotNetObjectRef) {
    dotNetHelper = dotNetObjectRef;

    resizeHandler = function () {
        if (dotNetHelper) {
            dotNetHelper.invokeMethodAsync('GetWindowSize', {
                width: window.innerWidth,
                height: window.innerHeight
            });
        }
    };

    resizeHandler(); // Call immediately for initial size
    window.addEventListener("resize", resizeHandler);
}

export function dispose() {
    window.removeEventListener('resize', resizeHandler);
    dotNetHelper = null;
    resizeHandler = null;
}
