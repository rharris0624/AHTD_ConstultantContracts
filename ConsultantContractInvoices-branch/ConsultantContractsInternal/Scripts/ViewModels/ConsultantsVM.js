var Consultant = function(data) {
    var self = this;

    self.ConsultantId = -1;
    //self.TaxId = ko.observable('').extend({ number: true, required: { params: true, message: 'Tax Id is required' }, pattern: {params: '^[0-9]{9}'}});
    self.TaxId = ko.observable('').extend({ number: true });
    self.SeqNo = ko.observable('').extend({number: true});
    self.Name = ko.observable('').extend({ required: { params: true} });
    self.PrimaryContactFirstName = ko.observable('').extend({ required: { params: true} });
    self.PrimaryContactLastName = ko.observable('').extend({ required: { params: true} });
    self.SecondaryContactFirstName = ko.observable('');
    self.SecondaryContactLastName = ko.observable('');
    self.PhysicalAddress = ko.observable('').extend({ required: { params: true} });
    self.City = ko.observable('').extend({ required: { params: true} });
    self.State = ko.observable('').extend({ required: { params: true }, pattern: { params: '^[A-Z]{2}$', message: 'State Abbreviation' } });
    self.StateName = ko.observable('');
    self.CountryCode = ko.observable('US').extend({ required: { params: true}, pattern: { params: '^[A-Z]{2}$', message: 'Country Abbreviation' }});
    self.PostalCode = ko.observable('').extend({
        pattern: {
            params: '^[0-9]{5}(?:-[0-9]{4})?$',
            onlyIf: function () { return self.CountryCode() === 'US'; },
            message: 'US Zip Code format required'
        },
        required: { params: true}
    });
    self.BusinessPhoneNumber = ko.observable('').extend({
        pattern: '^\([0-9]{3}\) [0-9]{3} [0-9]{4}$'
    });
    self.ContactPhoneNumber = ko.observable('');
    self.BusinessPhoneNumber = ko.observable('');
    self.EmailAddress = ko.observable('').extend({ emaillist: true, required: { params: true } });
    self.HomeOfficeOverheadRateMax = ko.observable('').extend({ number: true, max: 999 });
    self.FCCM = ko.observable('').extend({ number: true, max: 999 });
    self.FieldServiceOverheadRateMax = ko.observable('').extend({ number: true, max: 999 });
    self.Multiplier = ko.observable('').extend({ number: true, max: 99 });
    self.LastUpdateDate = ko.observable(new Date());
    self.LastUpdateUser = ko.observable('');

    self.TaxIdOld = ko.observable(data ? data.TaxId : null);
    self.TaxIdChanged = ko.computed(function () {
        return self.TaxId() !== self.TaxIdOld();
    });
    self.SeqNoOld = ko.observable(data ? data.SeqNo : null);
    self.SeqNoChanged = ko.computed(function () {
        return self.SeqNo() !== self.SeqNoOld();
    });
    self.NameOld = ko.observable(data ? data.Name : null);
    self.NameChanged = ko.computed(function () {
        return self.Name() !== self.NameOld();
    });
    self.PrimaryContactFirstNameOld = ko.observable(data ? data.PrimaryContactFirstName : null);
    self.PrimaryContactFirstNameChanged = ko.computed(function () {
        return self.PrimaryContactFirstName() !== self.PrimaryContactFirstNameOld();
    });
    self.PrimaryContactLastNameOld = ko.observable(data ? data.PrimaryContactLastName : null);
    self.PrimaryContactLastNameChanged = ko.computed(function () {
        return self.PrimaryContactLastName() !== self.PrimaryContactLastNameOld();
    });
    self.SecondaryContactFirstNameOld = ko.observable(data ? data.SecondaryContactFirstName : null);
    self.SecondaryContactFirstNameChanged = ko.computed(function () {
        return self.SecondaryContactFirstName() !== self.SecondaryContactFirstNameOld();
    });
    self.SecondaryContactLastNameOld = ko.observable(data ? data.SecondaryContactLastName : null);
    self.SecondaryContactLastNameChanged = ko.computed(function () {
        return self.SecondaryContactLastName() !== self.SecondaryContactLastNameOld();
    });
    self.PhysicalAddressOld = ko.observable(data ? data.PhysicalAddress : null);
    self.PhysicalAddressChanged = ko.computed(function () {
        return self.PhysicalAddress() !== self.PhysicalAddressOld();
    });
    self.CityOld = ko.observable(data ? data.City : null);
    self.CityChanged = ko.computed(function () {
        return self.City() !== self.CityOld();
    });
    self.StateOld = ko.observable(data && data.State !== null ? data.State.trim() : null);
    self.StateChanged = ko.computed(function () {
        return self.State() !== self.StateOld();
    });
    self.CountryCodeOld = ko.observable(data ? data.CountryCode : null);
    self.CountryCodeChanged = ko.computed(function () {
        return self.CountryCode() !== self.CountryCodeOld();
    });
    self.PostalCodeOld = ko.observable(data && data.PostalCode !== null ? data.PostalCode.trim() : null);
    self.PostalCodeChanged = ko.computed(function () {
        return self.PostalCode() !== self.PostalCodeOld();
    });
    self.EmailAddressOld = ko.observable(data ? data.EmailAddress : null);
    self.EmailAddressChanged = ko.computed(function () {
        return self.EmailAddress() !== self.EmailAddressOld();
    });
    self.HomeOfficeOverheadRateMaxOld = ko.observable(data ? data.HomeOfficeOverheadRateMax : null);
    self.HomeOfficeOverheadRateMaxChanged = ko.computed(function () {
        return self.HomeOfficeOverheadRateMax() !== self.HomeOfficeOverheadRateMaxOld();
    });
    self.FCCMOld = ko.observable(data ? data.FCCM : null);
    self.FCCMChanged = ko.computed(function () {
        return self.FCCM() !== self.FCCMOld();
    });
    self.FieldServiceOverheadRateMaxOld = ko.observable(data ? data.FieldServiceOverheadRateMax : null);
    self.FieldServiceOverheadRateMaxChanged = ko.computed(function () {
        return self.FieldServiceOverheadRateMax() !== self.FieldServiceOverheadRateMaxOld();
    });
    self.MultiplierOld = ko.observable(data ? data.Multiplier : null);
    self.MultiplierChanged = ko.computed(function () {
        return self.Multiplier() !== self.MultiplierOld();
    });

    if (data) {
        this.init(data);
    }

    self.taxSeq = ko.computed(function() {
        return '{0}-{1}'.format(self.TaxId(), self.SeqNo());
    });

    //self.BusinessPhoneNumberStripped = ko.computed(function () {
    //    return self.BusinessPhoneNumberFormatted().replace(/\D/g, '');
    //});
    //self.ContactPhoneNumberStripped = ko.computed(function () {
    //    return self.ContactPhoneNumber().replace(/\D/g, '');
    //});
    self.BusinessPhoneNumberOld = ko.observable(data ? data.BusinessPhoneNumber : null);
    self.BusinessPhoneNumberChanged = ko.computed(function () {
        return self.BusinessPhoneNumber() !== self.BusinessPhoneNumberOld();
    });

    self.ContactPhoneNumberOld = ko.observable(data ? data.ContactNumberPhone : null);
    self.ContactNumberPhoneChanged = ko.computed(function () {
        return self.ContactPhoneNumber() !== self.ContactPhoneNumberOld();
    });

    self.VendorAddress = ko.observable('');
    self.Vendorfulladdress = ko.observable('');
    self.Phone = ko.observable('');
    self.Fax = ko.observable('');
};


Consultant.prototype.init = function(data) {
    this.ConsultantId = data.ConsultantId || -1;
    this.TaxId(data.TaxId !== undefined ? data.TaxId.trim() : '');
    this.SeqNo((data.SeqNo === 0 || data.SeqNo ) ?  data.SeqNo : '');
    this.Name(data.Name || '');
    this.PrimaryContactFirstName(data.PrimaryContactFirstName || '');
    this.PrimaryContactLastName(data.PrimaryContactLastName || '');
    this.SecondaryContactFirstName(data.SecondaryContactFirstName || '');
    this.SecondaryContactLastName(data.SecondaryContactLastName || '');
    this.PhysicalAddress(data.PhysicalAddress || '');
    this.City(data.City || '');
    this.State(data.State.trim() || '');
    this.CountryCode(data.CountryCode || '');
    this.PostalCode(data.PostalCode.trim() || '');
    this.EmailAddress(data.EmailAddress || '');
    this.HomeOfficeOverheadRateMax(data.HomeOfficeOverheadRateMax || '');
    this.FCCM(data.FCCM || '');
    this.FieldServiceOverheadRateMax(data.FieldServiceOverheadRateMax || '');
    this.Multiplier(data.Multiplier || '');
    this.LastUpdateDate(data.LastUpdateDate || '');
    this.LastUpdateUser(data.LastUpdateUser || '');
    this.BusinessPhoneNumber(data.BusinessPhoneNumber || '');
    this.ContactPhoneNumber(data.ContactPhoneNumber || '');
};