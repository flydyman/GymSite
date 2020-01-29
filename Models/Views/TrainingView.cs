using System;

namespace GymSite.Models.Views
{
    public class TrainingView
    {
        public int ID {get;set;}
        public int ID_Training {get; set;}
        public int ID_Trainer {get;set;}
        public string TrainerName {get;set;}
        public string GroupTypeName {get;set;}
        public int ClientsCount {get;set;}
        // For assigning clients
        public bool IsRight {get;set;}
    }
}