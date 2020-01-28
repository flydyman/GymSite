using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSite.Models
{
    public class Price
    {
        [Key]
        public int ID {get;set;}
        [Column(TypeName="VARCHAR(100)")]
        public string Description {get;set;}
        public int Cost {get;set;}
    }
}