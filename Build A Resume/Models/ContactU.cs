﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Build_A_Resume.Models
{
    public partial class ContactU
    {
        public int ContactUsId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
