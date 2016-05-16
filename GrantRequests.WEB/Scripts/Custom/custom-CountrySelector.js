var CountrySelector = new function () {
    var uiSelectors = {
        countryDropDownSelector: null,
        stateDropDownSelector: null,
        territorySelector: null,
        contryContainerSelector: '#contryContainer',
        stateContainerSelector: '#stateContainer',
        territoryContainerSelector: '#territoryContainer'
    };
    var uiElements =
    {
        countryDropDown: null,
        stateDropDown: null,
        territory: null
    }

    this.Init = function (countryDropDownSelector, stateDropDownSelector, territorySelector) {
        uiSelectors.countryDropDownSelector = countryDropDownSelector;
        uiSelectors.stateDropDownSelector = stateDropDownSelector;
        uiSelectors.territorySelector = territorySelector;
       
        initUI();
        subscribeToEvents();

        uiElements.countryDropDown.trigger('change');
    }
    function initUI()
    {         
        uiElements.countryDropDown = $(uiSelectors.countryDropDownSelector);
        uiElements.stateDropDown = $(uiSelectors.stateDropDownSelector);
        uiElements.territory = $(uiSelectors.territorySelector);
    }
    function subscribeToEvents()
    {
        uiElements.countryDropDown.change(function (event)
        {
            var sender = $(this);
            var countryId = sender.val();
            if (countryId == 1) {
                $(uiSelectors.stateContainerSelector).show();
                $(uiSelectors.territoryContainerSelector).hide();
            }
            else
            {
                $(uiSelectors.stateContainerSelector).hide();
                $(uiSelectors.territoryContainerSelector).show();
            }
        });
    }
};