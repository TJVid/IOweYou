using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOUTrackerApp.Models
{
    public class IOUViewModel
    {
        public IOU iOU { get; set; }
        public List<IOU> allIOUsOfUser { get; set; }
        public List<IOUStatus> allIOUStatuses { get; set; }
    }
}