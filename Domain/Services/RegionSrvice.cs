using AutoMapper;
using Database.DTO;
using Database.DTO.RegionDTO;
using Database.DTO.Shared;
using Database.Models;
using Domain.IServices;

namespace Domain.Services
{
    public class RegionSrvice : IRegionService
    {
        private readonly RailwayTicketDbContext _railwayTicketDbContext;
        private readonly IMapper _mapper;
        public RegionSrvice(RailwayTicketDbContext railwayTicketDbContext, IMapper mapper)
        {
            _railwayTicketDbContext = railwayTicketDbContext;
            _mapper = mapper;
        }

        public ResponseModel GetAllRegions()
        {
            var regions = _railwayTicketDbContext.Regions.ToList();
            return new ResponseModel(MessageModel.Success, true, regions);
        }

        public ResponseModel AddRegion(RegionDTO newRegion)
        {
            try
            {
                var regionData = _railwayTicketDbContext.Regions.FirstOrDefault(x => x.Name == newRegion.Name);
                if (regionData == null)
                {
                    var data = _mapper.Map<Region>(newRegion);
                    data.CreatedAt = DateTime.UtcNow;

                    _railwayTicketDbContext.Regions.Add(data);
                    _railwayTicketDbContext.SaveChanges();

                    return new ResponseModel(MessageModel.AddSuccess, true, data);
                }
                return new ResponseModel("Data Already Exist", false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public ResponseModel UpdateRegion(UpdateRegionDTO updateData)
        {
            var existingRegion = _railwayTicketDbContext.Regions.FirstOrDefault(x => x.Id == updateData.Id);
            if (existingRegion != null)
            {
                _mapper.Map(updateData, existingRegion);
                existingRegion.UpdatedAt = DateTime.UtcNow;
                _railwayTicketDbContext.SaveChanges();
                return new ResponseModel(MessageModel.UpdateSuccess, true, existingRegion);
            }
            return new ResponseModel(MessageModel.UnSuccess, false);
        }
        public ResponseModel GetAllTownships()
        {
            var townships = _railwayTicketDbContext.Townships.ToList();
            return new ResponseModel(MessageModel.Success, true, townships);
        }
        public ResponseModel AddTownship(TownshipDTO newTownship)
        {
            var townshipData = _railwayTicketDbContext.Townships.Where(x => x.Name == newTownship.Name).FirstOrDefault();
            var regionData = _railwayTicketDbContext.Regions.FirstOrDefault(x => x.Id == newTownship.RegionId);
            if (townshipData == null && regionData != null)
            {
                var data = _mapper.Map<Township>(newTownship);
                data.CreatedAt = DateTime.UtcNow;
                _railwayTicketDbContext.Townships.Add(data);
                _railwayTicketDbContext.SaveChanges();

                return new ResponseModel(MessageModel.AddSuccess, true, data);
            }
            return new ResponseModel(MessageModel.UnSuccess, false);
        }
        public ResponseModel UpdateTownship(UpdateTownshipDTO townshipData)
        {
            var existingTownship = _railwayTicketDbContext.Townships.FirstOrDefault(x => x.Id == townshipData.Id);
            if (existingTownship != null)
            {
                _mapper.Map(townshipData, existingTownship);
                existingTownship.UpdatedAt = DateTime.UtcNow;
                _railwayTicketDbContext.SaveChanges();
                return new ResponseModel(MessageModel.UpdateSuccess, true, existingTownship);
            }
            return new ResponseModel(MessageModel.UnSuccess, false);
        }
    }
}
