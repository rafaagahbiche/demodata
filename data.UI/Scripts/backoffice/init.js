var tabLinksSelector = 'ul.tab-links';
var confirmSelector = 'div.del-confirm';
var backgroundSelector = 'div.del-background';
var hiddenBlocSelector = 'div.hidden-bloc';

// Called by Page.Tab OnBegin
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
    bindSelectedPageEvents();
}

// Called by Article.Tab OnBegin
var selectActiveArticle = function () {
    $('div#article-list').children('div.active').removeClass('active');
    $(this).parent().addClass('active');
}

var bindEvents = function () {
    bindArticleEvents();
    bindPageEvents();
}

var loadingObj = $(".loading").loading("Saving page");

$(function () {
    bindEvents();
    bindAddArticleTabEvent();
    $('div.article-link').first().addClass('active');
});