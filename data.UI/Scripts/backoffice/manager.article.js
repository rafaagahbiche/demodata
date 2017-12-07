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
                disableMenuItems.turnOff();
                bindSelectedPageEvents();
            }

            bindEvents();
            loadingArticle.turnOff();
        },
        error: function (err) {
            loadingArticle.turnOff();
        }
    });
}

var bindSaveArticleEvent = function () {
    $('a.save-article').bind('click', function (e) {
        var title = $('div#article').find('input#Title').val();
        if (title != "") {
            loadingArticle.turnOn();
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

var bindDeleteArticleEvent = function () {
    $('a.delete-article').bind('click', function (e) {
        $('.article-delete').show();
    });
    $('.article-delete').find('.cancel').bind('click', function (e) {
        $('.article-delete').hide();
    });
}


//$('a.delete-article').bind('click', function (e) {
    //    var articleId = $('div#article').children('input[type="hidden"]#Id').val();
    //    loadingArticle.turnOn();
    //    $.ajax({
    //        url: this.href,
    //        type: "POST",
    //        data: { id: articleId },
    //        success: function (data) {
    //            $('div#article-details').html(data);
    //            deleteCurrentArticleTab();
    //            bindEvents();
    //            disableMenuItems.turnOff();
    //            loadingArticle.turnOff();
    //        }
    //    });

    //    e.preventDefault();
    //});


var bindAddArticleTabEvent = function () {
    var addArticleTab = $("a#add-article-tab").addNewMenuItem({
        menuItems: disableMenuItems,
        callBack: bindEvents,
        addNewAction: '/Article/AddNewTab',
        showEmptyContentAction: '/Article/ShowArticleContent',
        addNewActionExtraData: null,
        showEmptyContentExtraData: { articleId: -1 },
        insertBeforeSelector: 'div.plus',
        activeLinkSelector: 'div.article-link.active',
        containerSelector: 'div#article-details'
    });
}

var bindArticleEvents = function () {
    bindSaveArticleEvent();
    bindDeleteArticleEvent();
}