//var Contract = function(data) {
//    var self = this;

//    self.ContractCode = null;
//    self.JobNo = ko.observable('').extend({ required: { params: true, message: '*' } });
//    self.ConsultantId = ko.observable('').extend({ required: { params: true, message: '*' }, number: true });

//    self.ContractTypeId = ko.observable('');
//    self.ContractStatus = ko.observable('');
//    self.AgreementDate = ko.observable('');
//    self.NoticeProceedDate = ko.observable('');
//    self.ConsultantContractNo = ko.observable('');
//    self.AgreementType = ko.observable('');
//    self.ResponibleDivision = ko.observable('');
//    self.ContractCeiling = ko.observable('');
//    self.T1SvcsCeiling = ko.observable('');
//    self.T1SvcsCeilingOrig = ko.observable('');
//    self.T1FixedFeeMax = ko.observable('');
//    self.T1FixedFeeMaxOrig = ko.observable('');
//    self.HomeOfficeOverheadRateMax = ko.observable('');
//    self.HomeOfficeOverheadRateMaxOrig = ko.observable('');
//    self.FCCM = ko.observable('');
//    self.FCCMOrig = ko.observable('');
//    self.T2SvcsCeiling = ko.observable('');
//    self.T2SvcsCeilingOrig = ko.observable('');
//    self.T2FixedFeeMax = ko.observable('');
//    self.T2FixedFeeMaxOrig = ko.observable('');
//    self.FieldServiceOverheadRateMax = ko.observable('');
//    self.FieldServiceOverheadRateMaxOrig = ko.observable('');
//    self.Multiplier = ko.observable('');
//    self.MultiplierOrig = ko.observable('');
//    self.ScheduledCompletionDate = ko.observable('');
//    self.CompletionDate = ko.observable('');

//    self.LastUpdateDate = ko.observable('');
//    self.LastUpdateUser = ko.observable('');

//    self.Consultant = ko.observable(new Consultant());
//    self.ContractAllotments = ko.observableArray([]);
//    self.ContractRemarks = ko.observableArray([]);
//    self.Invoices = ko.observableArray([]);
//    self.SalaryRates = ko.observableArray([]);
//    self.SuppAgreements = ko.observableArray([]);
//    self.SubConsultants = ko.observableArray([]);

//    if (data) {
//        this.init(data);
//    }

//    self.taxSeq = ko.computed(function() {
//        return '{0}-{1}'.format(self.TaxId(), self.SeqNo());
//    });
//};


//Contract.prototype.init = function (data) {
//    this.ContractCode = data.ConsultantId || -1;
//    this.JobNo(data.JobNo || '');
//    this.ConsultantId(data.ConsultantId || '');
//    this.ContractTypeId(data.ContractTypeId || '');
//    this.ContractStatus(data.ContractStatus || '');
//    this.AgreementDate(data.AgreementDate || '');
//    this.NoticeProceedDate(data.NoticeProceedDate || '');
//    this.ConsultantContractNo(data.ConsultantContractNo || '');
//    this.AgreementType(data.AgreementType || '');
//    this.ResponibleDivision(data.ResponibleDivision || '');
//    this.TypeWorkConst(data.TypeWorkConst || '');
//    this.TypeWorkDesign(data.TypeWorkDesign || '');
//    this.TypeWorkROW(data.TypeWorkROW || '');
//    this.TypeWorkEnv(data.TypeWorkEnv || '');
//    this.TypeWorkSurvey(data.TypeWorkSurvey || '');
//    this.TypeWorkOther(data.TypeWorkOther || '');
//    this.ContractCeiling(data.ContractCeiling || '');
//    this.T1SvcsCeiling(data.T1SvcsCeiling || '');
//    this.T1SvcsCeilingOrig(data.T1SvcsCeilingOrig || '');
//    this.T1FixedFeeMax(data.T1FixedFeeMax || '');
//    this.T1FixedFeeMaxOrig(data.T1FixedFeeMaxOrig || '');
//    this.HomeOfficeOverheadRateMax(data.HomeOfficeOverheadRateMax || '');
//    this.HomeOfficeOverheadRateMaxOrig(data.HomeOfficeOverheadRateMaxOrig || '');
//    this.FCCM(data.FCCM || '');
//    this.FCCMOrig(data.FCCMOrig || '');
//    this.T2SvcsCeiling(data.T2SvcsCeiling || '');
//    this.T2SvcsCeilingOrig(data.T2SvcsCeilingOrig || '');
//    this.T2FixedFeeMax(data.T2FixedFeeMax || '');
//    this.T2FixedFeeMaxOrig(data.T2FixedFeeMaxOrig || '');
//    this.FieldServiceOverheadRateMax(data.FieldServiceOverheadRateMax || '');
//    this.FieldServiceOverheadRateMaxOrig(data.FieldServiceOverheadRateMaxOrig || '');
//    this.Multiplier(data.Multiplier || '');
//    this.MultiplierOrig(data.MultiplierOrig || '');
//    this.ScheduledCompletionDate(data.ScheduledCompletionDate || '');
//    this.CompletionDate(data.CompletionDate || '');
//    this.LastUpdateDate(data.LastUpdateDate || '');
//    this.LastUpdateUser(data.LastUpdateUser || '');
//}