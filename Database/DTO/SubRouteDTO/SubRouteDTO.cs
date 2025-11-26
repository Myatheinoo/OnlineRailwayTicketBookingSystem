using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO.SubRouteDTO
{
    public class SubRouteDTO
    {
        public decimal UpperClassFee { get; set; }

        public decimal OrdinaryClassFee { get; set; }

        public decimal FirstClassFee { get; set; }

        public decimal SleeperClassFee { get; set; }

        public long MainRouteId { get; set; }

        public decimal LifeInsurance { get; set; }

        public string? Distance { get; set; }

        public string? Duration { get; set; }
    }

    public class GetAllSubRouteDTO
    {
        public string? RouteName { get; set; }
        public string? TrainType { get; set; }
        public string? RouteType { get; set; }
        public string? CarriageName { get; set; }
    }
}
