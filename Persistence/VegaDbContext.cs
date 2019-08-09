using Microsoft.EntityFrameworkCore;
using Vegas.Models;

namespace Vegas.Persistence
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Make> Makes{get;set;}
        public DbSet<Feature> Features{get;set;}
        public DbSet<Vehicle> Vehicles{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<VehicleFeature>()
                .HasKey(vf => new {vf.VehicleId, vf.FeatureId });
            modelBuilder.Entity<VehicleFeature>()
                .HasOne(vf => vf.Vehicle)
                .WithMany(v => v.VehicleFeatures)
                .HasForeignKey(vf => vf.VehicleId);


            modelBuilder.Entity<VehicleFeature>()
                .HasOne(vf => vf.Feature)
                .WithMany(f => f.VehicleFeatures)
                .HasForeignKey(vf => vf.FeatureId);
        }
    }
}