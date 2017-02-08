using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MaterialTemplate.Models;

namespace MaterialTemplate.Context
{
    public class FmrContext : DbContext
    {
        public FmrContext() : base("FmrContext")
        {
            Database.SetInitializer<FmrContext>(new CreateDatabaseIfNotExists<FmrContext>());
        }

        public DbSet<User> User { get; set; }
        public DbSet<Advert> Advert { get; set; }
        public DbSet<Feature> Feature { get; set; }
        public DbSet<AdvertFeature> AdvertFeature { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<Message> Message { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}