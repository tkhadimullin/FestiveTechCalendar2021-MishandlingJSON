using Microsoft.EntityFrameworkCore;
using MishandlingJson.Models;
using Newtonsoft.Json; // for value converter

namespace MishandlingJson
{
    internal class SHDbContext:DbContext
    {
        public DbSet<WishlistEntry> WishlistEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNSTR")).LogTo(Console.WriteLine);
        }

        // Adding for value converter
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<WishlistEntry>()
            //            .Property(e => e.ItemData)
            //            .HasConversion((itemData) => JsonConvert.SerializeObject(itemData), str => JsonConvert.DeserializeObject<ItemData>(str));
        }

        // adding static function to the DB Context
        [DbFunction("JSON_VALUE", IsBuiltIn = true, IsNullable = false)]
        public static string JsonValue(string expression, string path) => throw new NotImplementedException();

        // now, if we want to handle arrays we need to sweat a bit
        // adding static function to the DB Context
        [DbFunction("OPENJSON", IsBuiltIn = true, IsNullable = false)]
        public static string OpenJson(string expression, string path) => throw new NotImplementedException();
    }
}
