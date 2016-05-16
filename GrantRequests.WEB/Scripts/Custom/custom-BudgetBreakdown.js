var BudgetBreakdown = new function () {
    var uiSelectors = {
        programBudgetSelector: '#programBudget',
        CreateProgramBudgetBreakdownSelector: '#CreateProgramBudgetBreakdown',
        UpdateProgramBudgetBreakdownSelector: '#UpdateProgramBudgetBreakdown',
        CancelForBudgetBreakdownSelector: '#cancelForBudgetBreakdown',
        RemoveBudgetBreakdownClassSelector: '.RemoveBudgetBreakdown',
        AddBudgetBreakdownClassSelector: '.AddBudgetBreakdown'


    };
    var uiElements =
    {
        programBudget: null
    }

    var addline = 'http://localhost:58228/Home/AddLine';

    this.Init = function () {
       
        initUI();
        subscribeToEvents();
    }
    function initUI()
    {
        uiElements.programBudget = $(uiSelectors.programBudgetSelector);
    }
    function subscribeToEvents()
    {
        uiElements.programBudget.on('click', uiSelectors.CreateProgramBudgetBreakdownSelector, function () {
            var sender = $(this);
            sender.hide();            
            $(uiSelectors.UpdateProgramBudgetBreakdownSelector, uiElements.programBudget).show();
        });

        uiElements.programBudget.on('click', uiSelectors.RemoveBudgetBreakdownClassSelector, function () {
            var sender = $(this);
            var removeTR = sender.closest('tr');
            removeTR.remove();
            calc();
            return false;
        });

        uiElements.programBudget.on('click', uiSelectors.AddBudgetBreakdownClassSelector, function () {
            var sender = $(this);
            $.get(addline, null, function (htmlResponse) {
                $('#EditTable', uiElements.programBudget).find('> tbody').append(htmlResponse);
            });
            return false;
        });
        uiElements.programBudget.on('change', '.estotal', function () {
            var sender = $(this);
            calc();
        });
        uiElements.programBudget.on('click', uiSelectors.CancelForBudgetBreakdownSelector, function () {
            var sender = $(this);
            $(uiSelectors.programBudgetSelector + " input[type='text']").val(null);
            calc();
        })

        function calc() {
            var sum = 0;
            uiElements.programBudget.find('.estotal').each(function (index, element) {
                var $element = $(element);

                if ($.isNumeric($element.val()))
                    sum += parseFloat($element.val());
            });
            $('#SectionTotal').text(sum);
        }
    }
};

