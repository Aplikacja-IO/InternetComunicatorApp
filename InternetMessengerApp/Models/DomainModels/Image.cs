using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMessengerApp.DomainModels
{
    public partial class Image
    {
        public int ComponentId { get; set; }
        public string ImageUrl { get; set; }

        public virtual Component Component { get; set; }
    }
}
