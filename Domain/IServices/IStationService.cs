using Database.DTO;
using Database.DTO.StationDTO;

namespace Domain.IServices
{
    public interface IStationService
    {
        public Task<ResponseModel> GetAllStation();
        public Task<ResponseModel> GetStationById(int id);
        public Task<ResponseModel> AddNewStation(StationDTO newStation);
        public Task<ResponseModel> UpdateStation(UpdateStationDTO stationDTO);
    }
}
