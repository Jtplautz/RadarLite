﻿using Microsoft.EntityFrameworkCore;
using RadarLite.Database.Models.ComponentRegistration;

namespace RadarLite.Database.Models;

public sealed class RadarLiteContext : DbContext
{
    public RadarLiteContext(DbContextOptions<RadarLiteContext> options)
        : base(options)
    { 
    }

    #region DBSets
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.AddLocationComponent();
        base.OnModelCreating(modelBuilder);
    }
}

