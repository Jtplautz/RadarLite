using Microsoft.EntityFrameworkCore;
using RadarLite.Database.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarLite.Database.Models.ComponentRegistration;
public static partial class ComponentRegistration {
    public static void AddLocationComponent(this ModelBuilder builder)
    {
        builder.Entity<Location>().Map();
    }
}

