using System;
using System.ComponentModel.DataAnnotations;

namespace BookReviewApp.Models
{
	public class Book
	{
		public Book()
		{
		}
        public int BookID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Genre { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

