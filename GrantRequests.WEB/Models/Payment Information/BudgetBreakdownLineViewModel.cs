using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrantRequests.WEB.Models
{
    public class BudgetBreakdownLineViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Expense Type")]
        [Required]
        public string ExpenseType { get; set; }
        [Display(Name = "Estimated Total")]
        [Required]
        [Range(0.01, 1000000)]
        public decimal EstimatedTotal { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
    }
}