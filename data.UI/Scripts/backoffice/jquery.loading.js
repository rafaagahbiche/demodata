(function ($) {
    $.fn.loading = function (options) {
        //$base.isLoading = !$(settings.loadingClass).hasClass("hide-loading") && $(settings.loadingClass).hasClass("show-loading");
        //$base.isLoading = !$(settings.loadingClass).hasClass("hide-loading") && $(settings.loadingClass).hasClass("show-loading");
        var methods = {}

        var defaults = {
            loadingText: "Loading..."
        }

        var settings = $.extend({}, defaults, options);
        //var base = this;

        methods.turnOnLoading = function () {
            var $this = $(this);
            $this.show();
        };

        methods.turnOffLoading = function () {
            var $this = $(this);
            $this.hide();
        };

        return this;
    }
}(jQuery));