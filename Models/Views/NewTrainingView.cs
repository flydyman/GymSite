using System;
using System.Collections;
using System.Collections.Generic;
using GymSite.Models;

namespace GymSite.Models.Views
{
    public class NewTrainingView  : IEnumerable
    {
        public List<TrainingView> Trainings {get;set;}
        public DateTime CurrDate {get;set;}
        public Client client {get;set;}
        public DateTime AbonementLastDay { get; set; }

        public IEnumerator GetEnumerator()
        {
            return Trainings.GetEnumerator();
        }
    }
}