var AdminsAjaxMenu = new function () {
    var uiSelectors = {
        ajaxMenuItemClassSelector: '.ajaxMenuItem',
        adminsEditContainer: '#adminsEditContainer'
    };
    var uiElements =
    {
        ajaxMenuItems: null,
        adminsEdit: null
    };

    this.Init = function () {
        initUI();
        subscribeToEvents();
    }
    function initUI() {
        uiElements.ajaxMenuItems = $(uiSelectors.ajaxMenuItemClassSelector);
        uiElements.adminsEdit = $(uiSelectors.adminsEditContainer);
    }
    function subscribeToEvents() {
        uiElements.ajaxMenuItems.click(function (event) {
            var sender = this;
            event.preventDefault();
            $.post(
                sender.href,
                function (data)
                {
                    uiElements.adminsEdit.html(data);
                }
                );
        })
    }
};