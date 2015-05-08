using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MCVShop.Models
{
    public class TestLoggedIn : ValidationAttribute
    {
        private bool isLogged;

        public TestLoggedIn(bool isLogged) : base()
        {
            this.isLogged = isLogged;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(isLogged == false)
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }
            return ValidationResult.Success;
        }
    }
}