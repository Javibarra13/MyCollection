﻿using Microsoft.AspNetCore.Http;
using MyCollection.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Models
{
    public class CustomerImageViewModel : CustomerImage
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}