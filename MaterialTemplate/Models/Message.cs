using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaterialTemplate.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string MessageSubject { get; set; }
        public string MessageContent { get; set; }

        public int AdvertID { get; set; }

        public int MessageSender { get; set; }

        public int MessageReceiver { get; set; }
        
        public virtual User UserSender { get; set; }

        public virtual User UserReceiver { get; set; }

        public virtual Advert Advert { get; set; }

        public DateTime DT_CREATED { get; set; }

        public Message()
        {
            DT_CREATED = DateTime.Now;
        }
    }
}