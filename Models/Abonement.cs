using System;
using System.ComponentModel.DataAnnotations;

namespace GymSite.Models
{
    public class Abonement
    {
        [Key]
        public int ID {get;set;}
        public int ID_Client {get;set;}
        [DataType(DataType.Date)]
        public DateTime StartDate {get;set;}
        [DataType(DataType.Date)]
        public DateTime EndDate {get;set;}
        public int ID_Price {get;set;}
    }
}