using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ManagerModel
    {
        [Display(Name = "RSS")]
        public string RSSlink { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
