﻿using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMessengerApp.DomainModels
{
    public partial class GroupMembership
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }

        public virtual Group Group { get; set; }
        public virtual RegisterUser User { get; set; }
    }
}
