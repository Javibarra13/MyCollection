﻿using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
