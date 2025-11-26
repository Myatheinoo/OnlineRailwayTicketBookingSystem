using Database.DTO;
using Database.DTO.MainRouteDTO;

namespace Domain.IServices
{
    public interface IMainRouteService
    {
        public Task<ResponseModel> GetAllMainRoutes();
        public Task<ResponseModel> AddMainRoute(MainRouteDTO newMainRoute);
        public Task<ResponseModel> UpdateMainRouteById(UpdateMainRouteDTO routeData);
    }
}
