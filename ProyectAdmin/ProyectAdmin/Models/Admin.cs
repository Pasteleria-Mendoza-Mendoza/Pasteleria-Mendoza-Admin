﻿namespace ProyectAdmin.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string []Roles { get; set; }
    }
}
