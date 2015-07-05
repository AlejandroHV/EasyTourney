using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EasyTourney.Models.Entities
{
    public class UserLoginObject
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "The username field must be a valid email")]
        [Display(Name = "UserName /Email")]
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MaxLength(15, ErrorMessage = "Password is too long")]
        [MinLength(6, ErrorMessage = "Password is too short")]
        [Required]
        public string PassWord { get; set; }
    }
}