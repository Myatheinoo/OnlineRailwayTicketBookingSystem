using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO.TrainTypeDTO
{
    public class TrainsDTO
    {
        public string? TrainName { get; set; }
        public string? TrainNo { get; set; }
        public int? TrainTypeId { get; set; }
        public DateTime? StartedUsingDate { get; set; }
        public string? Power {  get; set; }
        public string? ManufactureBy { get; set; }
    }
    public class UpdateTrainDTO : TrainsDTO
    {
        public int Id { get; set; }
    }
    public class TrainsTypeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class TrainResponseDataDTO
    {
        public string? TrainName { get; set; }
        public string? TrainNo { get; set; }
        public string? TrainType {  get; set; }
        public string? ManufacutredBy { get; set; }
    }
}
