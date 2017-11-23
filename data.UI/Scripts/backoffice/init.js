var tabLinksSelector = 'ul.tab-links';
var confirmSelector = 'div.del-confirm';
var backgroundSelector = 'div.del-background';
var hiddenBlocSelector = 'div.hidden-bloc';

// Called by PageTab OnBegin
var selectCurrentTab = function () {
    $('ul#tabs').children('li.active').removeClass('active');
    $(this).parent().addClass('active');
}

// Called by Page.Delete OnBegin
var turnOffDeleteStyle = function () {
    disableTabLinks(false);
    showDeleteWarning(false);
}

// Called by Page.Delete OnComplete
var selectFirstTab = function () {
    deleteCurrentTab();
    initEventsForSelectedTab();
}

// Called by ArticleTab OnBegin
var selectActiveArticle = function () {
    $('div#article-list').children('div.active').removeClass('active');
    $(this).parent().addClass('active');
}

var initButtonFunctions = function () {
    initArticleFunctions();
    initPageFunctions();
}

var loadingObj = $(".loading").loading("Saving page");

$(function () {
    initButtonFunctions();
    initAddArticleTabOnclick();
    $('div.article-link').first().addClass('active');
});