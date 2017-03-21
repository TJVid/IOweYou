using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOUTrackerApp.Models
{
    [Authorize]
    public class IOU
    {
        [Key]
        [Display(Name = "I Owe you ItemID")]
        public int IOUId { get; set; }
        //[Required]
        [Display(Name = "User ID")]
        public string userID { get; set; }
        [Required]
        [Display(Name ="Name of the Lender")]
        public string lenderName { get; set; }
        [Required]
        [Display(Name ="Amount Borrowed")]
        public int amount { get; set; }
        //[Required]
        [DataType(DataType.Date)]
        [Display(Name ="Date the Amount was Borrowed")]
        public DateTime takenDt { get; set; }                   //should be taken automatically when status changes
        [DataType(DataType.Date)]
        [Display(Name ="Date on which the Borrow Status changed")]
        public DateTime statusChangedDt { get; set; }           //should be taken automatically
        [Display(Name ="Date on which the amount is planned to be returned")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime plannedReturnDt { get; set; }
        [ForeignKey("status")]
        public int statusId{ get; set; }
        [Display(Name ="Status")]
        public IOUStatus status { get; set; }
    }
}