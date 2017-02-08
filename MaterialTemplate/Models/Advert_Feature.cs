using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaterialTemplate.Models
{
    public class AdvertFeature
    {
        public int AdvertFeatureID { get; set; }

        public int AdvertID { get; set; }

        public int FeatureID { get; set; }

        public DateTime DT_CREATED { get; set; }

        [ForeignKey("AdvertID")]
        public virtual Advert Advert { get; set; }

        [ForeignKey("FeatureID")]
        public virtual Feature Feature { get; set; }

        public AdvertFeature()
        {
            DT_CREATED = DateTime.Now;
        }

    }
}