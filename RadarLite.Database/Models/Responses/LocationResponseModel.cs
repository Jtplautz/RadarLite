using RadarLite.Database.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarLite.Database.Models.Responses;
public class LocationResponseModel {
    public bool Success { get; init; }

    public List<Location> Body { get; init; } = new();
}