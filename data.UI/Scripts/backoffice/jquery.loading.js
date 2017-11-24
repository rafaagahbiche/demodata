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

    // add "disable" class to the object if it doesn't have it
    // remove "disable" class from the object if it has it
    $.fn.disable = function () {
        return this.each(function () {
            if (this.hasClass('disabled')) {
                this.removeClass('disabled');
            } else {
                this.addClass('disabled');
            }
        });
    }

    // Add "active" class to current object and remove "active" class from sibilings
    $.fn.activate = function () {
        return this.each(function () {
            var base = this;
            base.sibilings('.active').removeClass('active');
            base.addClass('active');
        });
    }
}(jQuery));