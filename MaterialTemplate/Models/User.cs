using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaterialTemplate.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Lütfen Kullanıcı Adı Giriniz.")]
        public string User_Username { get; set; }

        [Required(ErrorMessage = "Lütfen Adınızı Giriniz.")]
        public string User_Name { get; set; }

        [Required(ErrorMessage = "Lütfen Soyadınızı Giriniz.")]
        public string User_Surname { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz.")]
        public string User_Password { get; set; }

        [Required(ErrorMessage = "Lütfen Mail Adresinizi Giriniz.")]
        [EmailAddress]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Lütfenn geçerli bir mail adresi giriniz.")]
        public string User_Mail { get; set; }

        public int? User_Gender { get; set; }

        //[Required(ErrorMessage = "Lütfen Telefon Numaranızı Giriniz")]
        [Phone]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçersiz Telefon Numarası.")]
        public string User_Phone { get; set; }
        public string User_About { get; set; }
        public string User_Title { get; set; }

        [Required(ErrorMessage = "Lütfen Doğum Tarihinizi Giriniz.")]
        public DateTime User_Birthdate { get; set; }

        public DateTime User_Created { get; set; }

        public virtual ICollection<Advert> Advert { get; set; }

        public User()
        {
            Advert = new List<Advert>();
            User_Created = DateTime.Now;
        }
    }
}
