using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RadarLite.Database.Models.Entities;
public class Location {
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
}
public static partial class EntityMappings {
    public static EntityTypeBuilder<Location> Map(this EntityTypeBuilder<Location> Entity)
    {
        {
            
            Entity.ToTable(nameof(Location));
            //Entity.MapTrackableEntity();

            Entity.HasKey(e => e.Id);
            Entity.Property(x => x.Name);


            Entity.Property(x => x.Id)
                  .ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()")
                  .IsRequired()
                  .IsConcurrencyToken();

            //Entity.HasMany(x => x.Forecasts)
            //    .WithOne(x => x.City)
            //    .HasForeignKey(x => x.CityId)
            //    .OnDelete(DeleteBehavior.Restrict);


            return Entity;
        }
    }
}

