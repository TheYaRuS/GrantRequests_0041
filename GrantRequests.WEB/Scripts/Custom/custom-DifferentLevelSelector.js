var DifferentLevelSelector = new function () {
    var uiSelectors = {
        isDifferentLevelSelector: null,
        differentContainer: '#differentLevelContainer'
    };
    var uiElements =
    {
        isLevel: null,
        level: null
    }

    this.Init = function (isDifferentLevelSelector) {
        uiSelectors.isDifferentLevelSelector = isDifferentLevelSelector;

        initUI();
        subscribeToEvents();

        uiElements.isLevel.trigger('change');
    }
    function initUI() {
        uiElements.isLevel = $(uiSelectors.isDifferentLevelSelector);
    }
    function subscribeToEvents() {
        uiElements.isLevel.change(function (event) {
            var sender = $(this);

            if (sender.val() == "true")
                $(uiSelectors.differentContainer).show();
            else
                $(uiSelectors.differentContainer).hide();
        });
    }
};