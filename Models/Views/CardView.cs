using System;
using System.Collections.Generic;

namespace GymSite.Models.Views
{
    public class CardView
    {
        // From Client
        public int ID_Client {get;set;}
        public string FullName {get;set;}
        public int Age {get;set;} 
        // From Abonement
        public int ID {get;set;}
        public DateTime StartDate {get;set;}
        public DateTime EndDate {get;set;}
        public int ID_Price {get;set;}
    }
}