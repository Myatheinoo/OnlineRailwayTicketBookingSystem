
namespace Database.DTO.MainRouteDTO
{
    public class MainRouteDTO
    {
        public string RouteName { get; set; } = null!;

        public string? RouteType { get; set; }

        public string? Duration { get; set; }

        public int TrainTypeId { get; set; }
    }
    
    public class UpdateMainRouteDTO : MainRouteDTO
    {
        public int Id { get; set; }
    }

    public class MainRouteResponseDataDTO
    {
        public string? RouteName { get; set; }
        public string? TrainType { get; set; }
        public string? RouteType { get; set; }
        public string? Duration { get; set; }

    }
}
