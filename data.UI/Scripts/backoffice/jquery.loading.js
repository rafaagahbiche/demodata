(function ($) {
    $.fn.loading = function () {
        //var settings = $.extend({}, { loadingClass: ".loading" }, options);
        //var $base = $(this);
        //$base.isLoading = !$(settings.loadingClass).hasClass("hide-loading") && $(settings.loadingClass).hasClass("show-loading");
        //$base.isLoading = !$(settings.loadingClass).hasClass("hide-loading") && $(settings.loadingClass).hasClass("show-loading");
        this.turnOnLoading = function () {
            //if (!$base.isLoading) {
            this.show();
            //}
        };

        this.turnOffLoading = function () {
            //if ($base.isLoading) {
            this.hide();
            //}
        };

        return this;
    }
}(jQuery));