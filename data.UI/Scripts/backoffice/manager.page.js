var AssignTabToEditor = function () {
    var newPageId = $('div#pageinfos').find('input[type="hidden"]#Id').val();
    var link = $('li.tab').last().children('a')[0].href;
    var oldPageId = link.substring(link.indexOf('=') + 1, link.indexOf('&'));
    $('li.tab').last().children('a')[0].href = link.replace(oldPageId, newPageId);
}

var deleteCurrentTab = function () {
    if ($('ul#tabs').children("li.tab").length > 1) {
        $('ul#tabs').find('li.active').remove();
    }
    else {
        AssignTabToEditor();
        $('ul#tabs').children('li.plus').addClass("disabled");
    }

    var i = 1;
    $('ul#tabs').children('li.tab').each(function () {
        $(this).find('a').html(i);
        i++;
    });

    $('ul#tabs').children('li:first-child').addClass('active');
}

function callSavePage(url, pageViewModel) {
    var oldPageId = pageViewModel.Id;
    $.ajax({
        url: url,
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

var bindSavePageEvent = function () {
    $('a.save-page').bind('click', function (e) {
        var contentToSend = tinymce.activeEditor.getContent();
        if (contentToSend != "") {
            loadingPage.turnOn();
            var id = $('div#pageinfos').find('input[type="hidden"]#Id').val();
            var parentId = $('div#article')
                .children('input[type="hidden"]#Id').val();
            var pageViewModel = {
                Id: id,
                ArticleId: parentId,
                Content: $('<div></div>').text(contentToSend).html()
            };

            callSavePage(this.href, pageViewModel);
        }

        e.preventDefault();
    });
}

var bindEditDeletePageEvent = function () {
    $('.delete-page').bind('click', function () {
        disablePageTabs.turnOn();
        $("div.page-delete").show();
    });

    $('.edit-del-cancel').bind('click', function () {
        disablePageTabs.turnOff();
        $("div.page-delete").fideOut(1000);
    });
}

var bindAddPageTabEvent = function () {
    var parentId = $('div#article').children('input[type="hidden"]#Id').val();
    var addPageTab = $("a#tabplus").addNewMenuItem({
        menuItems: disablePageTabs,
        callBack: bindSelectedPageEvents,
        addNewAction: '/Page/AddNewTab',
        showEmptyContentAction: '/Page/ShowPageContent',
        addNewActionExtraData: { articleId: parentId },
        showEmptyContentExtraData: { pageId: -1, articleId: parentId },
        insertBeforeSelector: 'li.plus',
        activeLinkSelector: 'li.tab.active',
        containerSelector: 'div#page-editor'
    });
}

var bindSelectedPageEvents = function () {
    disablePageTabs = $('ul.tab-links').disable();
    loadingPage = $(".loading").loading();
    bindSavePageEvent();
    bindEditDeletePageEvent();
}

var bindPageEvents = function () {
    bindSelectedPageEvents();
    bindAddPageTabEvent();
}
