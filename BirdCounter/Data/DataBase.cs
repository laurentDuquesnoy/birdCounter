using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BirdCounter.Models;


namespace BirdCounter.Data
{
    public class DataBase : DbContext
    {
        public DbSet<Bird> Birds { get; set; }
        public DbSet<BirdCount> BirdCounts { get; set; }
        public DbSet<Session> Sessions { get; set; }
        
        public DataBase(DbContextOptions<DataBase> options) : base(options)
        {
            
        }

        public void Seed()
        {
            Bird bird1 = new Bird()
            {
                Id = 1,
                Description =
                    "De kerkuil is met zijn hartvormige gezicht en gitzwarte ogen één van de meest opvallende uilensoorten. Zijn onderzijde lijkt wit en is licht gestippelde onderzijde.",
                Name = "Kerkuil",
                Image = "/img/Barn owl.jpg"
            };
            Bird bird2 = new Bird()
            {
                Id = 2,
                Description =
                    "De gierzwaluw is geen zangvogel en is in feite dus geen echte zwaluw, hij is meer verwant aan de kolibries uit Amerika. In de zomermaanden laat hij zich opmerken door zijn snelle vliegwerk en typische geluid",
                Name = "Gierzwaluw",
                Image = "/img/Swift.jpg",
            };
            Bird bird3 = new Bird()
            {
                Id = 3,
                Description =
                    "De slechtvalk is onze grootste inheemse valk en wordt vaak bestempeld als ‘de snelste vogel ter wereld’",
                Name = "Slechtvalk",
                Image = "/img/Peregrine Falcon.jpg"
            };

            Birds.Add(bird1);
            Birds.Add(bird2);
            Birds.Add(bird3);

            Session s = new Session()
            {
                Id = 1,
                StartTime = new DateTime(2017, 10, 1, 20, 15, 00),
                EndTime = new DateTime(2017, 10, 1, 22, 15, 00)
            };
            BirdCount count1 = new BirdCount()
            {
                BirdId = 1,
                Count = 20,
                SessionId = 1
            };
            BirdCount count2 = new BirdCount()
            {
                BirdId = 2,
                Count = 5,
                SessionId = 1
            };

            Sessions.Add(s);
            BirdCounts.Add(count1);
            BirdCounts.Add(count2);
            
            SaveChanges();
        }

        public List<Bird> getBirds()
        {
            return Birds.ToList();
        }

        public Session GetSession(int id)
        {
            Session returnSession = Sessions.FirstOrDefault(a => a.Id == id);
            return returnSession;
        }

        public BirdCount GetCountWithSessionId(int id)
        {
            BirdCount returnCount = BirdCounts.FirstOrDefault(a => a.SessionId == id);
            return returnCount;
        }

        public List<Session> getSessions()
        {
            List<Session> returnValue = this.Sessions.ToList();
            return returnValue;
        }

        public DetailObject CreateDetailObject(int sessionId)
        {
            DetailObject returnValue = new DetailObject();
            returnValue.session = GetSession(sessionId);
            returnValue.birds = new List<BirdSession>();
            List<BirdSession> sessions = new List<BirdSession>();

            foreach (BirdCount count in BirdCounts.Where(a => a.SessionId == sessionId).ToList())
            {
                BirdSession session = new BirdSession();
                session.count = count;
                session.bird = Birds.FirstOrDefault(a => a.Id == count.BirdId);
                sessions.Add(session);
            }
            returnValue.birds.AddRange(sessions);
            return returnValue;
        }
    }
}