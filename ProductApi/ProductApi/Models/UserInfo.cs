using System;
using System.Collections.Generic;

namespace ProductApi.Models
{
    public partial class UserInfo
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
