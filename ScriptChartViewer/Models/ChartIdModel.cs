using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptChartViewer.Models
{
    public class ChartIdModel
    {
        [Required]
        public string ChartId { get; set; }
    }
}
