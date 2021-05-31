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
            
            SaveChanges();
        }

        public List<Bird> getBirds()
        {
            return Birds.ToList();
        }
    }
}