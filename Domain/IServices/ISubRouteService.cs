using Database.DTO;
using Database.DTO.SubRouteDTO;

namespace Domain.IServices
{
    public interface ISubRouteService
    {
        public Task<ResponseModel> AddSubRoute(SubRouteDTO newSubRoute); 
        public Task<ResponseModel> GetAllSubRoute();
    }
}
