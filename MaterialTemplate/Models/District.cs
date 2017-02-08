using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaterialTemplate.Models
{
    public class District
    {
        [Key]
        public int DistrictID { get; set; }
        public string District_Name { get; set; }
        public int ProvinceID { get; set; }
    }
}