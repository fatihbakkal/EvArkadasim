using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaterialTemplate.Models
{
    public class Property
    {
        [Key]
        public int PropertyID { get; set; }
        public int Property_Type { get; set; }
        public string Property_Title { get; set; }
    }
}