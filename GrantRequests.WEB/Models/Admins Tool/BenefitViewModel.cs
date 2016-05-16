using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrantRequests.WEB.Models
{
    public class BenefitViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Title name")]
        [Required]
        [StringLength(512)]
        public string Title { get; set; }
    }
}