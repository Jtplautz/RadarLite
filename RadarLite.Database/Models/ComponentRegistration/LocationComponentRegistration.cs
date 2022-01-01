using Microsoft.EntityFrameworkCore;
using RadarLite.Database.Models.Entities;

namespace RadarLite.Database.Models.ComponentRegistration;
public static partial class ComponentRegistration {
    public static void AddLocationComponent(this ModelBuilder builder)
    {
        builder.Entity<Location>().Map();
    }
}

