using GymSite;
using GymSite.Models;

namespace GymSite.Relations
{
    public class MyDBContext
    {
        public MyDBSet<Client> Clients {get;set;}
        public MyDBSet<Price> Prices {get;set;}
        public MyDBSet<Staff> Staffs {get;set;}
        public MyDBSet<Abonement> Abonements {get;set;}
        public MyDBSet<Trainer> Trainers {get;set;}
        public MyDBSet<TrainGroup> TrainGroups {get;set;}
        public MyDBSet<Training> Trainings {get;set;}

        public MyDBContext(MyDBUse db)
        {
            Clients = new MyDBSet<Client>(db);
            Prices = new MyDBSet<Price>(db);
            Staffs = new MyDBSet<Staff>(db);
            Abonements = new MyDBSet<Abonement>(db);
            Trainers = new MyDBSet<Trainer>(db);
            TrainGroups = new MyDBSet<TrainGroup>(db);
            Trainings = new MyDBSet<Training>(db);
        }
    }
}