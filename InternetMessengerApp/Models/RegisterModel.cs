using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMessengerApp.Models
{
    public class RegisterModel
    {
        public int UserId { get; set; }
        [DisplayName("Nazwa użytkownika")]
        public string UserName { get; set; }
        [DisplayName("Hasło")]
        public string UserPassword { get; set; }
        
        
    }
}
