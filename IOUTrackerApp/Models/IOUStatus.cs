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
    public class IOUStatus
    {
        [Key]
        [Display(Name ="Status ID")]
        public int statusID { get; set; }
        [Required]
        [Display(Name ="Status")]
        public string status { get; set; }
        [ForeignKey("color")]
        public int colorId { get; set; }
        [Display(Name ="Color")]
        public Color color { get; set; }
    }
}