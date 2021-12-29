using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        base.OnModelCreating(modelBuilder);
    }
}

