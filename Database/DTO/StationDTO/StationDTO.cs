namespace Database.DTO.StationDTO
{
    public class StationDTO
    {
        public string Name { get; set; } = null!;

        public long RegionId { get; set; }

        public long TownshipId { get; set; }

        public string StationNo { get; set; } = null!;

        public decimal? Lat { get; set; }

        public decimal? Long { get; set; }
    }

    public class UpdateStationDTO : StationDTO
    {
        public long Id { get; set; }
    }
    public class GetAllStationDTO
    {
        public string? StationName { get; set; }
        public string? StationNo { get; set; }
        public string? Township {  get; set; }
        public string? Region {  get; set; }
        public decimal? Lat { get; set; }

        public decimal? Long { get; set; }
    }
}
