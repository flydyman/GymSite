using System;
using System.Collections.Generic;

namespace GymSite.Models.Views
{
    public class FullTrainingView
    {
        // Training
        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        public string TrainerName { get; set; }
        public List<Client> Clients { get; set; }
        public Price Group { get; set; }
        
    }
}