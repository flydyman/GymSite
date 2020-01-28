using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSite.Models
{
    public class Staff
    {
        [Key]
        public int ID {get;set;}
        public string LastName {get;set;}
        public string FirstName {get;set;}
        public sbyte IsAdmin {get;set;}
        public string Login {get;set;}
        public string Password {get;set;}
    }
}