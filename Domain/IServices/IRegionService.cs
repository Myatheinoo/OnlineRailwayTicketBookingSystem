using Database.DTO;
using Database.DTO.RegionDTO;

namespace Domain.IServices
{
    public interface IRegionService
    {
        public ResponseModel GetAllRegions();
        public ResponseModel AddRegion(RegionDTO regionDTO);
        public ResponseModel UpdateRegion(UpdateRegionDTO updateData);
        public ResponseModel AddTownship(TownshipDTO newTownship);
        public ResponseModel GetAllTownships();
        public ResponseModel UpdateTownship(UpdateTownshipDTO townshipData);
    }
}
