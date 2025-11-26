using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO.CarriageDTO
{
    public class CarriageDTO
    {
        public string TypeMm { get; set; } = null!;

        public string TypeEn { get; set; } = null!;

        public long TrainTypeId { get; set; }

        public int Seat { get; set; }

        public bool IsBv { get; set; }

        public bool AcitveFlag { get; set; }
    }
    public class UpdateCarriageDTO : CarriageDTO
    {
        public int Id { get; set; }
    }

    public class CarriageResponseDataDTO
    {
        public string? TypeMm {  get; set; }
        public string? TypeEn { get; set; }
        public string? TraintypeName { get; set; }
        public int Seat { get; set; }
    }
}
