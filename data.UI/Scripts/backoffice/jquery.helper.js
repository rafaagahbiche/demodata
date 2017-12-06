(function ($) {
    $.fn.loading = function () {
        var $this = $(this);
        return {
            turnOn: function () {
                $this.show();
                return $this;
            },

            turnOff: function () {
                $this.fadeOut(1000);
                return $this;
            }
        }
    }

    // add "disable" class to the object if it doesn't have it
    // remove "disable" class from the object if it has it
    $.fn.disable = function () {
        var $base = $(this);
        return {
            turnOn: function () {
                $base.children().each(function () {
                    if (!$(this).hasClass("disabled")) {
                        $(this).addClass("disabled");
                    }
                });

                return $base;
            },
            turnOff: function () {
                $base.children().each(function () {
                    if ($(this).hasClass("disabled")) {
                        $(this).removeClass("disabled");
                    }
                });

                return $base;
            }
        }
    }

    // Add "active" class to current object and remove "active" class from sibilings
    $.fn.activate = function () {
        return this.each(function (i, el) {
            $(el).siblings('.active').removeClass('active');
            $(el).addClass('active');
        });
    }

    $.fn.addNewMenuItem = function (options) {
        var defaults = {
            menuItems: null,
            callBack : null,
            addNewAction: null,
            showEmptyContentAction: null,
            addNewActionExtraData: null,
            showEmptyContentExtraData: null,
            insertBeforeSelector: '.plus',
            activeLinkSelector: '.active',
            containerSelector: '#article-details'
        }

        var settings = $.extend({}, defaults, options);
        var base = this;
        return this.each(function () {
            base.bind('click', function (e) {
                $(settings.activeLinkSelector).removeClass('active');
                settings.menuItems.turnOn();
                $.ajax({
                    url: settings.addNewAction,
                    type: "GET",
                    data: settings.addNewActionExtraData,
                    success: function (data) {
                        $(data).insertBefore(settings.insertBeforeSelector);
                    }
                });

                $.ajax({
                    url: settings.showEmptyContentAction,
                    type: "GET",
                    data: settings.showEmptyContentExtraData,
                    success: function (data) {
                        $(settings.containerSelector).html(data);
                        settings.callBack();
                    }
                });

                e.preventDefault();
            });
        });
    }

}(jQuery));