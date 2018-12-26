using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ConsultantContractsInternal.ViewModels
{
    public class LoginViewModel
    {
        [Required, AllowHtml]
        public string Username { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}