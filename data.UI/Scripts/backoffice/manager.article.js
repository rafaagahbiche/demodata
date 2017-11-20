var initArticleFunctions = function () {
    initSaveArticleEvent();
    initDeleteArticleEvent();
}

function disableTabArticleLinks(disable) {
    if (disable) {
        $("div#article-list").children('div').each(function () {
            if (!$(this).hasClass("disabled")) {
                $(this).addClass("disabled");
            }
        });
    } else {
        $("div#article-list").children('div').each(function () {
            if ($(this).hasClass("disabled")) {
                $(this).removeClass("disabled");
            }
        });
    }
}

var deleteCurrentArticleTab = function () {
    $('div.article-link.active').remove();
    $('div.article-link:first-child').addClass('active');
}

var AssignTabToArticle = function (oldId, newId) {
    var link = $('div.article-link.active').find('a')[0].href;
    $('div.article-link.active').find('a')[0].href = link.replace(oldId, newId);
}

function callSaveArticle(url, articleViewModel) {
    var oldArticleId = articleViewModel.Id;
    $.ajax({
        url: url,
        cache: false,
        type: "POST",
        data: articleViewModel,
        success: function (data) {
            $('div#article-details').html(data);
            $('div.article-link.active').find('div').html(articleViewModel.Title);
            if (oldArticleId == -1) {
                var newArticleId = $('div#article').children('input[type="hidden"]#Id').val();
                AssignTabToArticle(oldArticleId, newArticleId);
                disableTabArticleLinks(false);
                initEventsForSelectedTab();
            }

            initButtonFunctions();
            $(".loading-article").hide();
        },
        error: function (err) {
            $(".loading-article").hide();
        }
    });
}

var initSaveArticleEvent = function () {
    $('a.save-article').bind('click', function (e) {
        var title = $('div#article').find('input#Title').val();
        if (title != "") {
            $(".loading-article").show();
            var articleId = $('div#article').children('input[type="hidden"]#Id').val();
            var desc = $('div#article').find('textarea#Description').val();
            var articleViewModel = {
                Id: articleId,
                Title: title,
                Description: desc
            };

            callSaveArticle(this.href, articleViewModel);
        }

        e.preventDefault();
    });
}

var initDeleteArticleEvent = function () {
    $('a.delete-article').bind('click', function (e) {
        var articleId = $('div#article').children('input[type="hidden"]#Id').val();
        $(".loading-article").show();
        $.ajax({
            url: this.href,
            type: "POST",
            data: { id: articleId },
            success: function (data) {
                $('div#article-details').html(data);
                deleteCurrentArticleTab();
                disableTabArticleLinks(false);
                initButtonFunctions();
                $(".loading-article").hide();
            }
        });

        e.preventDefault();
    });
}

var initAddArticleTabOnclick = function () {
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
                $('div#article-details').html(data);
                initButtonFunctions();
            }
        });

        e.preventDefault();
    });
}