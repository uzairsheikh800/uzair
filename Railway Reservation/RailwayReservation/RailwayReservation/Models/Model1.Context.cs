//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RailwayReservation.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RailwayEntities : DbContext
    {
        public RailwayEntities()
            : base("name=RailwayEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<stamaster> stamaster { get; set; }
        public virtual DbSet<TrainDetail> TrainDetail { get; set; }
        public virtual DbSet<TrainSchedule> TrainSchedule { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ConfirmSeat> ConfirmSeat { get; set; }
    }
}
