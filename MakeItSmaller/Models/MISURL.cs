using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MakeItSmaller.Models
{
    public class MISURL
    {
        [Display(Name = "MIS URL")]
        public string MISURLstring { get; set; }
    }
}