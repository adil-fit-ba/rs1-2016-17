﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CondorExtreme3_API.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CondorDBXEntities : DbContext
    {
        public CondorDBXEntities()
            : base("name=CondorDBXEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Actors> Actors { get; set; }
        public virtual DbSet<AgeRestrictions> AgeRestrictions { get; set; }
        public virtual DbSet<Banks> Banks { get; set; }
        public virtual DbSet<CinemaHalls> CinemaHalls { get; set; }
        public virtual DbSet<Cinemas> Cinemas { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<EmployeePayments> EmployeePayments { get; set; }
        public virtual DbSet<EmployeeRoles> EmployeeRoles { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Employments> Employments { get; set; }
        public virtual DbSet<EmploymentTypes> EmploymentTypes { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<MovieDirectors> MovieDirectors { get; set; }
        public virtual DbSet<MovieRatings> MovieRatings { get; set; }
        public virtual DbSet<MovieRoles> MovieRoles { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<PaymentMethods> PaymentMethods { get; set; }
        public virtual DbSet<Projections> Projections { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RVisitors> RVisitors { get; set; }
        public virtual DbSet<SalesOrderVirtualPoints> SalesOrderVirtualPoints { get; set; }
        public virtual DbSet<SeatColumns> SeatColumns { get; set; }
        public virtual DbSet<SeatRows> SeatRows { get; set; }
        public virtual DbSet<Seats> Seats { get; set; }
        public virtual DbSet<TechnologyTypes> TechnologyTypes { get; set; }
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<VirtualPointsPackets> VirtualPointsPackets { get; set; }
    
        public virtual ObjectResult<Employees_Result> EmployeesView()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Employees_Result>("EmployeesView");
        }
    }
}