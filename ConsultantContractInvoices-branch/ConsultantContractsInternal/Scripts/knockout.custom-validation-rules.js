ko.validation.rules['nullableDecimal'] = {
    validator: function (val, validate) {
        return val === null || val === "" || (validate && /^-?\d*(?:\.\d*)?$/.test(val.toString()));
    },
    message: 'Must be empty or a decimal value'
};

//.extend ( equalNumbers: { params: otherNumber, message: "Number must equal otherNumber"});
ko.validation.rules['equalNumbers'] = {
    validator: function (val, otherField) {
        n1 = Number(val);
        n2 = Number(ko.unwrap(otherField));
        return (n1 === n2);
    },
    message: 'This number must equal another number'
}

//.extend( greaterThan: { params: otherVariable, message: "Variable must be greater than otherVariable" });
ko.validation.rules['greaterThan'] = {
    validator: function (val, otherField) {
        if (!isNaN(val) && !isNaN(otherField)) {
            return (Number(val) >= Number(otherField));
        } else return false;
    },
    message: 'This field must have a greater value'
};

ko.validation.rules['lessThan'] = {
    validator: function (val, otherField) {
        if (!isNaN(val) && !isNaN(otherField)) {
            return (Number(val) <= Number(otherField));
        } else return false;
    },
    message: 'This field must have a lesser value'
};

//greaterThanDate: { params: self.earlierDate }
ko.validation.rules['greaterThanDate'] = {
    validator: function (val, otherField) {
        if (!val)
            return true;
        if (otherField !== undefined) {
            var d1 = moment(val);
            var d2 = moment(otherField);

            return (d1 >= d2);
        } else return false;
    },
    message: 'This date can\'t precede another'
};

//eitherOr: {params: self.Multiplier}
ko.validation.rules['eitherOr'] = {
    validator: function (val, otherField) {
        if ((val !== undefined) && (val !== '') && (otherField !== undefined) && (otherField !== '')) {
            return false;
        } else return true;
    },
    message: 'Can\'t have values in both of these fields'
};

//dateFormat:
ko.validation.rules['properDateFormat'] = {
    validator: function (val) {
        if (!val)
            return true;
        //var date = moment(val).format('MM/DD/YYYY');
        if (val.match(/^(?:1[0-2]|0?[1-9])\/(?:3[01]|[12][0-9]|0?[1-9])\/[0-9]{4}$/gm) === null)
            return false;
        else return true;
    },
    message: 'MM/DD/YYYY'
}

ko.validation.rules['properDateRange'] = {
    validator: function (val) {
        if (!val)
            return true;
        var maxdate = moment().add(10, 'y');

        return (maxdate > moment(val));
    },
    message: 'Date too far in advance'
}

ko.validation.rules['emaillist'] = {
    validator: function (val, validate) {
        if (!validate) return true;
        if (ko.validation.utils.isEmptyVal(val)) return true;

        var emailrule = ko.validation.rules['email'];

        var vals = val.split(/\s*;\s*/);
        for (var i = 0; i < vals.length; i++) {
            if (!emailrule.validator(vals[i], true)) {
                return false;
            }
        }
        return true;
    },
    message: "email list contains invalid entries. Separate valid entries with the use of a semicolon."
};
ko.validation.addExtender('emaillist');


ko.validation.registerExtenders();