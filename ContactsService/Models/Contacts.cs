using System;
using System.ComponentModel.DataAnnotations;

namespace ContactsService.Models
{
    public class Contact
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}