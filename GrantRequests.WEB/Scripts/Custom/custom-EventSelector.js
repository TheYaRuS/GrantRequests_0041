var EventSelector = new function () {
    var uiSelectors = {
        isEventSelector: null,
        eventInformationContainer: '#eventInformationContainer'
    };
    var uiElements =
    {
        isEvent: null,
        eventInformation: null
    }

    this.Init = function (isEventSelector) {
        uiSelectors.isEventSelector = isEventSelector;
       
        initUI();
        subscribeToEvents();

        uiElements.isEvent.trigger('change');
    }
    function initUI()
    {         
        uiElements.isEvent = $(uiSelectors.isEventSelector);
    }
    function subscribeToEvents()
    {
        uiElements.isEvent.change(function (event)
        {
            var sender = $(this);

            if (sender.prop("checked")) 
                $(uiSelectors.eventInformationContainer).show();
            else
                 $(uiSelectors.eventInformationContainer).hide();
        });
    }
};