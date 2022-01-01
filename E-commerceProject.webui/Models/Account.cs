

using System;

namespace E_commerceProject.webui.Models
{
    public class Account
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool Sex { get; set; }
        public string Type { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}