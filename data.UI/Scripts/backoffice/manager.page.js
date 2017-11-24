var AssignTabToEditor = function () {
    var newPageId = $('div#pageinfos').find('input[type="hidden"]#Id').val();
    var link = $('li.tab').last().children('a')[0].href;
    var oldPageId = link.substring(link.indexOf('=') + 1, link.indexOf('&'));
    $('li.tab').last().children('a')[0].href = link.replace(oldPageId, newPageId);
}

var initEventsForSelectedTab = function () {
    initSavePageEvent();
    editDeleteEvent();
}

var initPageFunctions = function () {
    initPageTabOnclick();
    initEventsForSelectedTab();
}

function disableTabLinks(disable) {
    if (disable) {
        $(tabLinksSelector).children('li').each(function () {
            $(this).addClass("disabled");
        });
    }
    else {
        $(tabLinksSelector).children('li').each(function () {
            $(this).removeClass("disabled");
        });
    }
}

function showDeleteWarning(show) {
    if (show) {
        $(confirmSelector).show();
        $(backgroundSelector).show();
    } else {
        $(confirmSelector).hide();
        $(backgroundSelector).hide();
    }
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
                disableTabLinks(false);
            }

            //loadingObj.turnOffLoading();
            $('div.loading').hide();
        },
        error: function (err) {
            //loadingObj.turnOffLoading();
            $('div.loading').hide();
        }
    });
}

var initPageTabOnclick = function () {
    $("a#tabplus").bind('click', function (e) {
        $('ul#tabs').children('li.active').removeClass('active');
        disableTabLinks(true);
        var parentId = $('div#article')
            .children('input[type="hidden"]#Id').val();
        $.ajax({
            url: '/Page/AddNewTab',
            type: "GET",
            data: { articleId: parentId },
            success: function (data) {
                $(data).insertBefore('li.plus');
            }
        });

        $.ajax({
            url: '/Page/ShowPageContent',
            type: "GET",
            data: { pageId: -1, articleId: parentId },
            success: function (data) {
                $('div#page-editor').html(data);
                initEventsForSelectedTab();
            }
        });

        e.preventDefault();
    });
}

var initSavePageEvent = function () {
    $('a.save-page').bind('click', function (e) {
        var contentToSend = tinymce.activeEditor.getContent();
        if (contentToSend != "") {
            //loadingObj.turnOnLoading();
            $('div.loading').show();
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

var editDeleteEvent = function () {
    $('.delete-page').bind('click', function () {
        disableTabLinks(true);
        showDeleteWarning(true);
    });

    $('.edit-del-cancel').bind('click', function () {
        disableTabLinks(false);
        showDeleteWarning(false);
    });
}

