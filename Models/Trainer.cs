using System.ComponentModel.DataAnnotations;

namespace GymSite.Models
{
    public class Trainer
    {
        [Key]
        public int ID {get;set;}
        public string LastName {get;set;}
        public string FirstName {get;set;}
        
    }
}