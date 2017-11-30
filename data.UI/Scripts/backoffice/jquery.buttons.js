(function ($) {
    $.fn.deleteButton = function (options) {

        var defaults = {
            deleteAction: null,
            callBack: null,
            deleteButtonSelector: '.delete',
            confirmDeleteSelector: '.plus',
            containerSelector: '#article-details'
        }
        var methods = {};

        var settings = $.extend({}, defaults, options);
        var base = this;

        methods.events(this);

        methods.events = function () {
            function callSavePage(url, pageViewModel) {
                var oldPageId = pageViewModel.Id;
                $.ajax({
                    url: settings.deleteAction,
                    cache: false,
                    type: "POST",
                    data: pageViewModel,
                    success: function (data) {
                        $('div#pageinfos').html(data);
                        if (oldPageId == -1) {
                            $('li.active').find('a').html($('li.tab').length);
                            AssignTabToEditor();
                            disablePageTabs.turnOff();
                        }

                        loadingPage.turnOff();
                    },
                    error: function (err) {
                        loadingPage.turnOff();
                    }
                });
            }

        }

        return this.each(function () {
        });
    };
}(jQuery));