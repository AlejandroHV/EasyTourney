using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EasyTourney.Models
{
    [MetadataType(typeof(tblUserMetaData))]
    public partial class tblUser
    {
        public bool IsEventAdmin { get; set; }
    }

    public class tblUserMetaData
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

        [Display(Name="Rol")]
        public Nullable<System.Guid> RolId { get; set; }


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

    [MetadataType(typeof(tblEventMetaData))]
    public partial class tblEvent
    {        
    }

    public class tblEventMetaData
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [StringLength(200, ErrorMessage = "Name cannot be longer than 200 characters.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Start date")]
        public Nullable<System.DateTime> StarDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Created date")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public Nullable<decimal> Price { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters.")]
        public string City { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Country")]
        [StringLength(50, ErrorMessage = "Country cannot be longer than 50 characters.")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Max Participants")]
        [Range(1, 200)]
        public Nullable<int> MaxParticipants { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public Nullable<bool> IsActive { get; set; }
    }
}