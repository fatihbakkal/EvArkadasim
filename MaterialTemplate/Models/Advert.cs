using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MaterialTemplate.Models
{
    public class Advert
    {
        [Key]
        public int AdvertID { get; set; }
        [Required(ErrorMessage = "Lütfen İlan Başlığı Giriniz!")]
        public string Advert_Title { get; set; }
        [Required(ErrorMessage = "Lütfen İlan Tanımı Giriniz!")]
        [StringLength(300, MinimumLength = 15, ErrorMessage = "Tanım çok kısa!")]
        public string Advert_Describe { get; set; }
        [Required(ErrorMessage = "Lütfen Ücret Giriniz!")]
        [DataType(DataType.Currency, ErrorMessage = "Geçersiz ücret değeri girdiniz!")]
        public double Advert_Price { get; set; }
        [Required(ErrorMessage = "Lütfen Adres Giriniz!")]
        [StringLength(150, MinimumLength = 20, ErrorMessage = "Adres çok kısa!")]
        public string Advert_Address { get; set; }
        public int? Advert_Field { get; set; }
        public int? Advert_Bed { get; set; }
        public int? Advert_Bath { get; set; }
        public int? Advert_HeatingType { get; set; }
        [Required(ErrorMessage = "Lütfen İlan Kategorisi Seçiniz!")]
        public int Advert_Category { get; set; }
        public string Advert_ExtraInformation { get; set; }

        public string Advert_Latitude { get; set; }
        public string Advert_Longitude { get; set; }

        [Required(ErrorMessage = "Lütfen İl Seçiniz!")]
        public int ProvinceID { get; set; }

        [Required(ErrorMessage = "Lütfen İlçe Seçiniz!")]
        public int DistrictID { get; set; }

        public int UserID { get; set; }

        public DateTime DT_CREATED { get; set; }

        public DateTime? DT_MODIFIED { get; set; }

        [ForeignKey("ProvinceID")]
        public virtual Province Province { get; set; }

        [ForeignKey("DistrictID")]
        public virtual District District { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public virtual ICollection<AdvertFeature> AdvertFeature { get; set; }

        public Advert()
        {
            DT_CREATED = DateTime.Now;
            AdvertFeature = new List<AdvertFeature>();
        }
    }
}