﻿using SM.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Services.Repository.Account
{
    public class CreateAccountDto
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;   
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public GenderEnum Gender { get; set; }
        public DateTime Dob { get; set; }
    }
}
