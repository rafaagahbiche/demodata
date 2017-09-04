var loadingSelector = 'div.loading';
var tabLinksSelector = 'ul.tab-links';
var confirmSelector = 'div.del-confirm';
var backgroundSelector = 'div.del-background';
var hiddenBlocSelector = 'div.hidden-bloc';

function switchLoading(activate) {
    if (activate) {
        if ($(loadingSelector).hasClass("hide-loading") && !$(loadingSelector).hasClass("show-loading")) {
            $(loadingSelector).removeClass("hide-loading");
            $(loadingSelector).addClass("show-loading");
        }
    } else {
        if (!$(loadingSelector).hasClass("hide-loading") && $(loadingSelector).hasClass("show-loading")) {
            $(loadingSelector).addClass("hide-loading");
            $(loadingSelector).removeClass("show-loading");
        }
    }
}

function disableTabArticleLinks(disable) {
    if (disable) {
        $("div#article-list").children('div').each(function () {
            if (!$(this).hasClass("disabled")) {
                $(this).addClass("disabled");
            }
        });
    }
    else {
        $("div#article-list").children('div').each(function () {
            if ($(this).hasClass("disabled")) {
                $(this).removeClass("disabled");
            }
        });
    }
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

            switchLoading(false);
        },
        error: function (err) {
           
        }
    });
}

function callSaveArticle(url, articleViewModel) {
    var oldArticleId = articleViewModel.Id;
    $.ajax({
        url: url,
        cache: false,
        type: "POST",
        data: articleViewModel,
        success: function (data) {
            $('div#article').html(data);
            $('div.article-link.active').find('div').html(articleViewModel.Title);
            if (oldArticleId == -1) {
                var newArticleId = $('div#article').children('input[type="hidden"]#Id').val();
                AssignTabToArticle(oldArticleId, newArticleId);
                disableTabArticleLinks(false);
                initEventsForSelectedTab();
            }
        },
        error: function (err) {
            
        }
    });
}

var AssignTabToArticle = function (oldId, newId) {
    var link = $('div.article-link.active').find('a')[0].href;
    $('div.article-link.active').find('a')[0].href = link.replace(oldId, newId);
}


var AssignTabToEditor = function () {
    var newPageId = $('div#pageinfos').find('input[type="hidden"]#Id').val();
    var link = $('li.tab').last().children('a')[0].href;
    var oldPageId = link.substring(link.indexOf('=') + 1, link.indexOf('&'));
    $('li.tab').last().children('a')[0].href = link.replace(oldPageId, newPageId);
}

var initPageTabOnclick = function () {
    $("a#tabplus").bind('click', function (e) {
        $('ul#tabs').children('li.active').removeClass('active');
        disableTabLinks(true);
        var parentId = $('div#article').children('input[type="hidden"]#Id').val();
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
                $('div#tobeupdated').html(data);
                initEventsForSelectedTab();
            }
        });

        e.preventDefault();
    });
}

var initArticleTabOnclick = function () {
    $("a#add-article-tab").bind('click', function (e) {
        $('div.article-link.active').removeClass('active');
        disableTabArticleLinks(true);
        $.ajax({
            url: '/Article/AddNewTab',
            type: "GET",
            success: function (data) {
                $(data).insertBefore('div.plus');
            }
        });

        $.ajax({
            url: '/Article/ShowArticleContent',
            type: "GET",
            data: { articleId: -1 },
            success: function (data) {
                $('div#article').html(data);
            }
        });

        e.preventDefault();
    });
}

var selectFocusTab = function () {
    $('ul#tabs').children('li.active').removeClass('active');
    $(this).parent().addClass('active');
}

var selectActiveArticle = function () {
    $('div#article-list').children('div.active').removeClass('active');
    $(this).parent().addClass('active');
}

var initEventsForSelectedTab = function () {
    initSavePageEvent();
    editDeleteEvent();
}

var initSavePageEvent = function () {
    $('a.save-page').bind('click', function (e) {
        var contentToSend = tinymce.activeEditor.getContent();
        if (contentToSend != "") {
            $(loadingSelector).find('span').html("Saving page content...");
            switchLoading(true);
            var id = $('div#pageinfos').find('input[type="hidden"]#Id').val();
            var parentId = $('div#article').children('input[type="hidden"]#Id').val();
            var pageViewModel = { Id: id, ArticleId: parentId, Content: $('<div></div>').text(contentToSend).html() };
            callSavePage(this.href, pageViewModel);
        }
        e.preventDefault();
    });
}

var initSaveArticleEvent = function () {
    $('a.save-article').bind('click', function (e) {
        var title = $('div#article').find('input#Title').val();
        if (title != "") {
            var articleId = $('div#article').children('input[type="hidden"]#Id').val();
            var desc = $('div#article').find('textarea#Description').val();
            var articleViewModel = { Id: articleId, Title: title, Description: desc };
            callSaveArticle(this.href, articleViewModel);
        }
        e.preventDefault();
    });
}


var initDeleteArticleEvent = function () {
    $('a.delete-article').bind('click', function (e) {
        var articleId = $('div#article').children('input[type="hidden"]#Id').val();
        $.ajax({
            url: this.href,
            type: "POST",
            data: { id: articleId },
            success: function (data) {
                $('div#article').html(data);
                deleteCurrentArticleTab();
                disableTabArticleLinks(false);
            }
        });

        e.preventDefault();
    });
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

var deleteCurrentArticleTab = function () {
    $('div.article-link.active').remove();
    $('div.article-link:first-child').addClass('active');
}

var turnOffDeleteStyle = function () {
    disableTabLinks(false);
    showDeleteWarning(false);
    deleteCurrentTab();
    initEventsForSelectedTab();
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

var initArticleFunctions = function () {
    $('div.article-link').first().addClass('active');
    initArticleTabOnclick();
    initSaveArticleEvent();
    initDeleteArticleEvent();
}

var initPageFunctions = function () {
    initPageTabOnclick();
    initSavePageEvent();
    editDeleteEvent();
}

$(function () {
    initPageFunctions();
    initArticleFunctions();
});


