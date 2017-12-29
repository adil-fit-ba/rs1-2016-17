namespace CondorExtreme3.Migrations.Configuration2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CondorExtreme3.DAL;
    using CondorExtreme3.ModelsUser;
    using CondorExtreme3.ModelsLocalDB;
    using System.Collections.Generic;
    internal sealed class Configuration : DbMigrationsConfiguration<CondorDBUsers>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            MigrationsDirectory = "Migrations\\Configuration2";
            
        }
        protected override void Seed(CondorDBUsers context)
        {
            var Countries = new List<ModelsUser.Country>()
            {
                new ModelsUser.Country { Name = "Bosnia and Herzegovina", Guid = Guid.NewGuid().ToString(), IsDeleted = false},
                 new ModelsUser.Country { Name = "Croatia" , Guid = Guid.NewGuid().ToString(), IsDeleted = false },
                  new ModelsUser.Country { Name = "Serbia" , Guid = Guid.NewGuid().ToString(), IsDeleted = false }
            };

            Countries.ForEach(s => context.Country.AddOrUpdate(s));
            context.SaveChanges();


            var Cities = new List<ModelsUser.City>()
            {
                new ModelsUser.City { CountryID = 1, Name = "Sarajevo", IsDeleted = false, Guid = Guid.NewGuid().ToString() },
                new ModelsUser.City { CountryID = 1, Name = "Mostar", IsDeleted = false, Guid = Guid.NewGuid().ToString() },
                new ModelsUser.City { CountryID = 1, Name = "Tuzla", IsDeleted = false, Guid = Guid.NewGuid().ToString() },
                new ModelsUser.City { CountryID = 1, Name = "Banja Luka", IsDeleted = false, Guid = Guid.NewGuid().ToString() }
            };

            Cities.ForEach(s => context.Cities.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
