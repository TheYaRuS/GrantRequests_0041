using GrantRequests.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantRequests.Common;

namespace GrantRequests.WEB.Models
{
    public class InformationViewModelBase
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int RequestId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public RequestType RequestType { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsValid { get; set; }


        public bool[] verificationPart { get; set; }
    }
}