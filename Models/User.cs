using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Models
{
	public class User
	{
		public User()
		{
		}
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,9}$",ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required field")]
        [DataType(DataType.Password)]
        [MinLength(7, ErrorMessage = "Password must be at least 7 characters long")]
        [RegularExpression(@"^(?=.*[!@#$%^&*(),.?""':{}|<>])[^\s]*$", ErrorMessage = "Password must contain at least one special character and no spaces")]
        public string PasswordHash { get; set; }

    public DateTime CreatedDate { get; set; }

    }
}

