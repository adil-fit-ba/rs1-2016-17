namespace CondorExtreme3.Migrations.Configuration1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CondorExtreme3.DAL;
    using CondorExtreme3.ModelsUser;
    using CondorExtreme3.ModelsLocalDB;
    using System.Collections.Generic;
    
    internal sealed class Configuration : DbMigrationsConfiguration<CondorDBContextChild>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
