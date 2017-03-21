using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IOUTrackerApp.Models
{
    public class Color
    {
        [Key]
        public int id { get; set; }
        public string color { get; set; }
        public Color(string _color) { color = _color; }
        public Color() { }
    }
}