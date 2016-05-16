using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantRequests.Common;
namespace GrantRequests.WEB.Models
{
    public class VoteResultViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int RequestId { get; set; }

        [Display(Name = "Approver")]
        public int UserId { get; set; }

        [Display(Name = "Approver")]
        public string Approver { get; set; }

        public VotingOptions? Voting { get; set; }

        [Display(Name = "Date/Time")]
        public DateTime? DateTime { get; set; }
    }
}