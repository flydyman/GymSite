using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymSite.Models;

namespace GymSite.Models.Views
{
    public class ClientInfo
    {
        // From Client
        public int ID {get;set;}
        public string LastName {get;set;}
        public string FirstName {get;set;}
        public Genders Gender {get;set;}
        [DataType(DataType.Date)]
        public DateTime DateOfBirth {get;set;}
        // From Abonement
        [DataType(DataType.Date)]
        public DateTime StartDate {get;set;}
        [DataType(DataType.Date)]
        public DateTime EndDate {get;set;}
        // From Price
        public int ID_Price {get;set;}
        public string Description {get;set;}
        public int Cost {get;set;}
        public bool HasAbonement {get;set;}
    }
}