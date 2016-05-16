var BenefitSelector = new function () {
    var uiSelectors = {
        isBenefitSelector: null,
        benefitContainer: '#benefitContainer'
    };
    var uiElements =
    {
        isBenefit: null,
        benefit: null
    }

    this.Init = function (isBenefitSelector) {
        uiSelectors.isBenefitSelector = isBenefitSelector;
       
        initUI();
        subscribeToEvents();

        uiElements.isBenefit.trigger('change');
    }
    function initUI()
    {         
        uiElements.isBenefit = $(uiSelectors.isBenefitSelector);
    }
    function subscribeToEvents()
    {
        uiElements.isBenefit.change(function (event)
        {
            var sender = $(this);

            if (sender.val()=="true") 
                $(uiSelectors.benefitContainer).show();
            else
                 $(uiSelectors.benefitContainer).hide();
        });
    }
};