using AutoMapper;
using Database.DTO;
using Database.DTO.Shared;
using Database.DTO.SubRouteDTO;
using Database.DTO.UserDTO;
using Database.Models;
using DocumentFormat.OpenXml.Drawing.Charts;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class SubRouteService : ISubRouteService
    {
        private readonly RailwayTicketDbContext _railwayTicketDbContext;
        private readonly IMapper _mapper;
        public SubRouteService(RailwayTicketDbContext railwayTicketDbContext,IMapper mapper)
        {
            _railwayTicketDbContext = railwayTicketDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseModel> AddSubRoute(SubRouteDTO newSubRoute)
        {
            try
            {
                var subRoute = _mapper.Map<SubRoute>(newSubRoute);
                subRoute.SubRouteId = Guid.NewGuid();
                subRoute.CreatedAt = DateTime.UtcNow;
                subRoute.ActiveFlag = true;

                 await _railwayTicketDbContext.SubRoutes.AddAsync(subRoute);
                 await _railwayTicketDbContext.SaveChangesAsync();

                return new ResponseModel(MessageModel.AddSuccess, true, subRoute);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseModel> GetAllSubRoute()
        {
            var subRouteList =await (from mr in _railwayTicketDbContext.MainRoutes
                                join tt in _railwayTicketDbContext.TrainTypes on mr.TrainTypeId equals tt.Id
                                join cr in _railwayTicketDbContext.Carriages on tt.Id equals cr.TrainTypeId
                                select new GetAllSubRouteDTO
                                {
                                    RouteName = mr.RouteName,
                                    TrainType = tt.Name,
                                    RouteType = mr.RouteType,
                                    CarriageName = cr.TypeMm
                                }).ToListAsync();
            return new ResponseModel(MessageModel.Success, true, subRouteList);
        }
    }
}
