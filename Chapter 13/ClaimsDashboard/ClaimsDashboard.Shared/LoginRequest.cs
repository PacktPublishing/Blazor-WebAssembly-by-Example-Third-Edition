using System;
using System.Collections.Generic;
using System.Text;

namespace ClaimsDashboard.Shared
{
    public class LoginRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
