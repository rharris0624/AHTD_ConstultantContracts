using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ConsultantContractsInternal.Models;

namespace ConsultantContractsInternal.ViewModels
{
    public class InvoiceSearchVM
    {
        public InvoiceSearchVM() 
        {
            OrderBy = "ContractCode";
            SortOrder = "ASCENDING";
            page = 1;
        }
        public static string DefaultSortOrder = "DESCENDING";
        public static string SortingByDefault = "ContractCode";

        public static Dictionary<string, string> OrderByFields = new Dictionary<string, string>
            {
                {"ContractCode", "Contract Code"},
                {"InvoiceDate", "Invoice Date"},
                {"ConsultantId", "ConsultantId"},
                {"ConsultantName", "ConsultantName"}
            };

        public static Dictionary<string, string> SortOrders = new Dictionary<string, string>
            {
                {"ASCENDING", "Ascending"},
                {"DESCENDING", "Descending"}
            };
        private string _consultantName;
        private string _orderBy;
        private string _sortOrder;

        [DisplayName("Payment From Date")]
        public DateTime? PaymentFromDate { get; set; }
        [DisplayName("Payment To Date")]
        public DateTime? PaymentToDate { get; set; }
        [DisplayName("Contract")]
        public int ContractCode { get; set; }
        [DisplayName("Invoice From Date")]
        public DateTime? InvoiceFromDate { get; set; }
        [DisplayName("Invoice To Date")]
        public DateTime? InvoiceToDate { get; set; }
        [DisplayName("Consultant")]
        public int ConsultantId{ get; set; }
        [DisplayName("Consultant Name")]
        public string ConsultantName 
        { 
            get
            {
                if (_consultantName == null)
                    _consultantName = "";
                return _consultantName;
            }
            set
            {
                _consultantName = value;
            }
        }

        [DisplayName("Sort Order")]
        public string SortOrder { get; set; }

        public List<UnpaidInvoice> UnpaidInvoices { get; set; }
        [DisplayName("Order By")]
        public string OrderBy 
        {
            get
            {
                if (_orderBy == null)
                    _orderBy = "ContractCode";
                return _orderBy;
            }
            set {_orderBy = value; } 
        }
        public int page { get; set; }
        public string Search { get; set; }

    }
}