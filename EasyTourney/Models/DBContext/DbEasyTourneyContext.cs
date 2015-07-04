using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EasyTourney.Models.DBContext
{
    public partial class User
    {

        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MaxLength(15)]
        [MinLength(5)]
        [Required]
        public string Pass { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [MaxLength(15)]
        public string Phone { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "City")]
        [Required]
        public string City { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Country")]
        [Required]
        public string Country { get; set; }    
    }

    

}