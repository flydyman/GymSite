using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSite.Models
{
    public class Training
    {
        [Key]
        public int ID {get;set;}
        public int ID_Trainer {get;set;}
        public int ID_Price {get;set;}
        public int ID_Creator {get;set;}
        [DataType(DataType.DateTime)]
        public DateTime StartTime {get;set;}
        //[DataType(DataType.DateTime)]
        //public DateTime EndTime {get;set;}
    }
}