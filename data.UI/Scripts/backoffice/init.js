// Called by Page.Tab OnBegin
var selectCurrentTab = function () {
    $(this).parent().activate();
}

// Called by Page.Delete OnBegin
var turnOffDeleteStyle = function () {
    disablePageTabs.turnOff();
    $("div.page-delete").hide();
}

// Called by Page.Delete OnComplete
var selectFirstTab = function () {
    deleteCurrentTab();
    bindSelectedPageEvents();
}

var bindEvents = function () {
    bindArticleEvents();
    bindPageEvents();
}

var loadingPage = null;
var loadingArticle = null;
var disableMenuItems = null;
var disablePageTabs = null;

$(function () {
    disableMenuItems = $("div#article-list").disable();
    loadingArticle = $(".loading-article").loading();
    bindEvents();
    bindAddArticleTabEvent();
    $('div.article-link').first().addClass('active');
});