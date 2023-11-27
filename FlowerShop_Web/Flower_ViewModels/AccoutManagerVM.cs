using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_ViewModels
{
    public class AccoutManagerVM
    {
        public string? ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
      
        public int? ID_CustomerType { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? ID_Shop { get; set; }
        public string? ID_Role { get; set; }
        public double? Spend { get; set; }


        //[Required(ErrorMessage = "Current password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string? OldPassword { get; set; }

        //[Required(ErrorMessage = "New password is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()?/>.<,:;'\\|}\]{[_~`+=-]+$", ErrorMessage = "Invalid password format")]
        [Display(Name = "New Password")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

    }
}
