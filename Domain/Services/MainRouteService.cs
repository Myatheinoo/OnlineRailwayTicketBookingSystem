using AutoMapper;
using Database.DTO;
using Database.DTO.MainRouteDTO;
using Database.DTO.Shared;
using Database.Models;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class MainRouteService : IMainRouteService
    {
        private readonly RailwayTicketDbContext _railwayTicketDbContext;
        private readonly IMapper _mapper;
        public MainRouteService(RailwayTicketDbContext railwayTicketDbContext,IMapper mapper)
        {
            _railwayTicketDbContext = railwayTicketDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseModel> GetAllMainRoutes()
        {
            var routeData = new List<MainRouteResponseDataDTO>();

            var routes = await _railwayTicketDbContext.MainRoutes.Where(x => x.ActiveFlag == true).ToListAsync();
            
           foreach (var route in routes)
            {
                var trainTypes = await _railwayTicketDbContext.TrainTypes.FirstOrDefaultAsync(x => x.Id == route.TrainTypeId);
                var routesdata = new MainRouteResponseDataDTO
                {
                    RouteName = route.RouteName,
                    RouteType = route.RouteType,
                    TrainType = trainTypes!.Name,
                    Duration = route.Duration,
                };
                routeData.Add(routesdata);

            }
            return new ResponseModel(MessageModel.Success,true,routeData);
        }
        public async Task<ResponseModel> AddMainRoute(MainRouteDTO newMainRoute)
        {
            try
            {
                var data = _mapper.Map<MainRoute>(newMainRoute);
                data.MainRouteId = Guid.NewGuid();
                data.CreatedAt = DateTime.UtcNow;
                data.ActiveFlag = true;

                await _railwayTicketDbContext.MainRoutes.AddAsync(data);
                await _railwayTicketDbContext.SaveChangesAsync();

                return new ResponseModel(MessageModel.AddSuccess, true, data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseModel> UpdateMainRouteById(UpdateMainRouteDTO routeData)
        {
            var existingData = await _railwayTicketDbContext.MainRoutes.FirstOrDefaultAsync(x => x.Id == routeData.Id && x.ActiveFlag == true);
            if (existingData != null)
            {
                _mapper.Map(routeData, existingData);
                existingData.UpdatedAt = DateTime.UtcNow;
                await _railwayTicketDbContext.SaveChangesAsync();
                return new ResponseModel(MessageModel.UpdateSuccess, true, existingData);
            }
            return new ResponseModel("Data not found.", false);
        }

    }
}
