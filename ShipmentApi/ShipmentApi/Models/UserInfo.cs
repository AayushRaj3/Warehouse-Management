using System;
using System.Collections.Generic;

namespace ShipmentApi.Models
{
    public partial class UserInfo
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
