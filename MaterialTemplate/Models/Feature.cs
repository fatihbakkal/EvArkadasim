using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaterialTemplate.Models
{
    public class Feature
    {
        [Key]
        public int FeatureID { get; set; }

        public string Feature_Title { get; set; }

        public DateTime DT_CREATED { get; set; }

        public virtual ICollection<AdvertFeature> AdvertFeature { get; set; }

        public Feature()
        {
            DT_CREATED = DateTime.Now;
            AdvertFeature = new List<AdvertFeature>();
        }
    }
}