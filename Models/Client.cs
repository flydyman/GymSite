using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSite.Models
{
    public class Client
    {
        public enum Genders {
            [Display(Name = "Male")]
            M,
            [Display(Name = "Female")]
            F
        }

        [Key]
        public int ID {get;set;}
        public string LastName {get;set;}
        public string FirstName {get;set;}
        [Column(TypeName="CHAR(1)")]
        public Genders Gender {get;set;}
        [DataType(DataType.Date)]
        public DateTime DateOfBirth {get;set;}
    }
}