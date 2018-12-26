var formatNumber = function (element, valueAccessor, allBindingsAccessor, format) {
    // Provide a custom text value
    var value = valueAccessor();
    var allBindings = allBindingsAccessor();
    var numeralFormat = allBindingsAccessor.numeralFormat || format;
    var strNumber = ko.utils.unwrapObservable(value);
    if (strNumber) {
        return numeral(strNumber).format(numeralFormat);
    }
    return '';
};

var formatMoney = function (num) {
    var str = num.toString().replace("$", ""),
        parts = false,
        output = [],
        i = 1,
        formatted = null;
    if (str.indexOf(".") > 0) {
        parts = str.split(".");
        str = parts[0];
    }
    str = str.split("").reverse();
    for (var j = 0, len = str.length; j < len; j++) {
        if (str[j] !== ",") {
            output.push(str[j]);
            if (i % 3 === 0 && j < (len - 1)) {
                output.push(",");
            }
            i++;
        }
    }
    formatted = output.reverse().join("");
    return ("$" + formatted + ((parts) ? "." + parts[1].substr(0, 2) : ""));
};

var formatPercentage = function (toFormat, multiplyWithHundred) {
    if (isNaN(toFormat)) //called in wrong fashion
        return toFormat;

    return parseFloat(toFormat) * (multiplyWithHundred ? 100 : 1) + '%';
};

var unformatMoney = function (formatted) {
    var unformatted = formatted.replace(',', '').replace('$', '');
    if (isNaN(unformatted))
        return unformatted;

    return parseFloat(unformatted);
};

var unformatPercentage = function (formatted, divideByHundred) {
    var unformatted = formatted.replace('%', '');
    if (isNaN(unformatted))
        return unformatted;

    return parseFloat(unformatted) / (divideByHundred ? 100 : 1);
};
$('.rateAmt').change(function (e) {
    var mval = null;
    if (e.target.id) {
        mval = $('#' + e.target.id).val().trim();

        if (mval)
            $('#' + e.target.id).val(numeral(mval).format("(0.0%)"));
        else
            $('#' + e.target.id).val = '';
    }
});

ko.bindingHandlers.moneyvalue = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        $(element).val(formatNumber(element, valueAccessor, allBindingsAccessor, "-$0,0.00"));

        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($(element).val());
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        $(element).val(formatNumber(element, valueAccessor, allBindingsAccessor, "-$0,0.00"));
    }
};

ko.bindingHandlers.moneytext = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        $(element).text(formatNumber(element, valueAccessor, allBindingsAccessor, "-$0,0.00"));
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        $(element).text(formatNumber(element, valueAccessor, allBindingsAccessor, "-$0,0.00"));
        determineNegativeClass(element);
    }
};

ko.bindingHandlers.numeraltext = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        $(element).text(formatNumber(element, valueAccessor, allBindingsAccessor, "-0,0.00"));  
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        $(element).text(formatNumber(element, valueAccessor, allBindingsAccessor, "-0,0.00"));
    }
};

ko.bindingHandlers.numeralvalue = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        $(element).val(formatNumber(element, valueAccessor, allBindingsAccessor, "-0,0.00"));

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($(element).val());
        });        
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        $(element).val(formatNumber(element, valueAccessor, allBindingsAccessor, "-0,0.00"));
    }
};

ko.bindingHandlers.percenttext = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        $(element).text(formatNumber(element, valueAccessor, allBindingsAccessor, "-0.000 %"));
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        $(element).text(formatNumber(element, valueAccessor, allBindingsAccessor, "-0.000 %"));
    }
};

ko.bindingHandlers.percentvalue = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        $(element).val(formatNumber(element, valueAccessor, allBindingsAccessor, "-0.000 %"));

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            if ($(element).val() !== 0 && !$(element).val().isNaN)
                observable($(element).val() / 100);
            else
                observable(0.00);
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        $(element).val(formatNumber(element, valueAccessor, allBindingsAccessor, "-0.000 %"));
    }
};

ko.bindingHandlers.maskedInput = {
    init: function (element, valueAccesssor, allBindings, viewModel, bindingContext) {
        ko.bindingHandlers.value.init(element, valueAccessor, allBindings, viewModel, bindingContext);
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        ko.bindingHandlers.value.update(element, valueAccessor, allBindings, viewModel, bindingContext);
        $(element).mask(allBindings.get('mask'));
    }
};