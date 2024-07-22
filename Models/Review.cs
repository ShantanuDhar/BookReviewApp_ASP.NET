using System;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Models
{
	public class Review
	{
		public Review()
		{
		}
        public int ReviewID { get; set; }

        [Required]
        public int BookID { get; set; }

        public Book Book { get; set; }

        [Required]
        public int UserID { get; set; }

        public User User { get; set; }

        [Required]
        public string ReviewText { get; set; }

        [Required]
        public int Rating { get; set; }

        public DateTime CreatedDate { get; set; }

        public string BookTitle { get; set; }
    }
}

