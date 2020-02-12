﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HranaContext context)
        {
            //Remove EnsureCreated when data needs to be preserved - EnsureCreated create new empty db
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Hrana.Any())
            {
                return;   // DB has been seeded
            }

            var hrana = new Hrana[]
            {
                new Hrana{Naziv="Pekarski kropmir", Stalna=true},
                new Hrana{Naziv="Pileci file sa zara", Stalna = true},
                new Hrana{Naziv="Lignje", Stalna=false}
            };
            foreach(Hrana h in hrana)
            {
                context.Hrana.Add(h);
            }
            context.SaveChanges();

            var meni = new Meni[]
            {
                new Meni{Datum= DateTime.Now },
                new Meni{Datum= DateTime.Now.AddDays(1) }
            };
            foreach(Meni m in meni)
            {
                context.Add(m);
            }

            

            var bookbs = new Book[]
            {
                new Book{Name="Prva knjiga"},
                new Book{Name="Druga knjiga"},
                
            };
            foreach (Book b in bookbs)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();

            var users = new User[]
            {
                new User{FirstName="Prvi", LastName = "Korisnik"},
                new User{FirstName="Drugi", LastName = "Korisnik"},

            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var enrollments = new SavedBook[]
            {
                new SavedBook{UserId=1,BookId=1}
            };
            foreach (SavedBook e in enrollments)
            {
                context.SavedBooks.Add(e);
            }
            context.SaveChanges();
        }
    }
}
