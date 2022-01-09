using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMessengerApp.DomainModels
{
    public partial class Post
    {
        public int ComponentId { get; set; }
        public string PostText { get; set; }

        public virtual Component Component { get; set; }
    }
}
