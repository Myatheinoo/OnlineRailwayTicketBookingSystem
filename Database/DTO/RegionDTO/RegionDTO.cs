using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO.RegionDTO
{
    public class RegionDTO
    {
        public string? Name { get; set; }
        public string? NameMM { get; set; }
    }
    public class UpdateRegionDTO : RegionDTO
    {
        public int Id { get; set; }
    }
    public class TownshipDTO
    {
        public string Name { get; set; } = null!;

        public string NameMm { get; set; } = null!;

        public long RegionId { get; set; }
    }
    public class UpdateTownshipDTO : TownshipDTO
    {
        public int Id { get; set; }
    }

}
