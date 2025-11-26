using AutoMapper;
using Database.DTO;
using Database.DTO.Shared;
using Database.DTO.StationDTO;
using Database.Models;
using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class StationService : IStationService
    {
        private readonly RailwayTicketDbContext _railwayTicketDbContext;
        private readonly IMapper _mapper;
        public StationService(RailwayTicketDbContext railwayTicketDbContext,IMapper mapper)
        {
            _railwayTicketDbContext = railwayTicketDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseModel> GetAllStation()
        {
            var stationList = new List<GetAllStationDTO>();

            var stations = await _railwayTicketDbContext.Stations.Where(x => x.ActiveFlag).ToListAsync();
            foreach (var station in stations)
            {
                var township = await _railwayTicketDbContext.Townships.FirstOrDefaultAsync(x => x.Id == station.TownshipId);
                var region = await _railwayTicketDbContext.Regions.FirstOrDefaultAsync(x => x.Id == station.RegionId);

                var stationData = new GetAllStationDTO
                {
                    StationName = station.Name,
                    StationNo = station.StationNo,
                    Township = township!.NameMm,
                    Region = region!.NameMm,
                    Lat = station.Lat,
                    Long = station.Long,
                };
                stationList.Add(stationData);
            }

            return new ResponseModel(MessageModel.Success,true,stationList);
        }
        public async Task<ResponseModel> GetStationById(int id)
        {
            var station = await _railwayTicketDbContext.Stations.FirstOrDefaultAsync(x => x.Id == id);
            if(station is not null)
            {
                var region = await _railwayTicketDbContext.Regions.FirstOrDefaultAsync(x => x.Id == station.RegionId);
                var township = await _railwayTicketDbContext.Townships.FirstOrDefaultAsync(x => x.Id == station.TownshipId);

                var stationData = new GetAllStationDTO
                {
                    StationName = station.Name,
                    StationNo = station.StationNo,
                    Region = region!.NameMm,
                    Township = township!.NameMm,
                    Lat = station.Lat,
                    Long = station.Long
                };
                return new ResponseModel(MessageModel.Success,true, stationData);
            }
            return new ResponseModel("Data not found.", false);
        }
        public async Task<ResponseModel> AddNewStation(StationDTO newStation)
        {
            try
            {
                var stationNo = await _railwayTicketDbContext.Stations.FirstOrDefaultAsync(x => x.StationNo == newStation.StationNo);
                if (stationNo is null)
                {
                    var station = _mapper.Map<Station>(newStation);
                    station.StationId = Guid.NewGuid();
                    station.CreatedAt = DateTime.UtcNow;
                    station.ActiveFlag = true;

                    await _railwayTicketDbContext.Stations.AddAsync(station);
                    await _railwayTicketDbContext.SaveChangesAsync();

                    return new ResponseModel(MessageModel.AddSuccess, true, station);
                }
                return new ResponseModel(MessageModel.UnSuccess, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ResponseModel> UpdateStation(UpdateStationDTO stationDTO)
        {
            var station = await _railwayTicketDbContext.Stations.FirstOrDefaultAsync(x => x.Id == stationDTO.Id);
            if(station is not null)
            {
                _mapper.Map(stationDTO,station);
                station.UpdatedAt = DateTime.UtcNow;

                await _railwayTicketDbContext.SaveChangesAsync();
                return new ResponseModel(MessageModel.UpdateSuccess,true,station);
            }
            return new ResponseModel("Data Not found.",false);
        }
    }
}
