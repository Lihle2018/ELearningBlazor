window.scrollFunctions = {
    scrollToTop: function() {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    },

    getScrollPosition: function() {
        return window.pageYOffset || document.documentElement.scrollTop;
    },

    scrollToElement: function(elementId) {
        const element = document.getElementById(elementId);
        if (element) {
            element.scrollIntoView({ behavior: 'smooth' });
        }
    },

    addScrollListener: function(dotNetRef) {
        window.addEventListener('scroll', () => {
            dotNetRef.invokeMethodAsync('HandleScroll');
        });
    }
};