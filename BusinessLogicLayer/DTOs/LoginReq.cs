﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DTOs
{
    public class LoginReq
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}