using AutoMapper;
using Database.DTO;
using Database.DTO.CarriageDTO;
using Database.DTO.Shared;
using Database.Models;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class CarriageService : ICarriageService
    {
        private readonly IMapper _mapper;
        private readonly RailwayTicketDbContext _railwayTicketDbContext;

        public CarriageService(RailwayTicketDbContext railwayTicketDbContext, IMapper mapper)
        {
            _railwayTicketDbContext = railwayTicketDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseModel> GetAllCarriage()
        {
            var carriageList = new List<CarriageResponseDataDTO>();

            var carriages =await _railwayTicketDbContext.Carriages.Where(x => x.AcitveFlag).ToListAsync();

            foreach (var carriage in carriages)
            {
                var trainType = await _railwayTicketDbContext.TrainTypes.FirstOrDefaultAsync(x => x.Id == carriage.TrainTypeId);
                var carriageData = new CarriageResponseDataDTO
                {
                    TypeMm = carriage.TypeMm,
                    TypeEn = carriage.TypeEn,
                    TraintypeName = trainType!.Name,
                    Seat = carriage.Seat
                };

                carriageList.Add(carriageData);
            }
            return new ResponseModel(MessageModel.Success, true, carriageList);
        }

        public ResponseModel AddCarriage(CarriageDTO newCarriage)
        {
            try
            {
                var data = _mapper.Map<Carriage>(newCarriage);
                data.CarriageId = Guid.NewGuid();
                data.CreatedAt = DateTime.UtcNow;
                data.AcitveFlag = true;

                _railwayTicketDbContext.Carriages.Add(data);
                _railwayTicketDbContext.SaveChanges();

                return new ResponseModel(MessageModel.AddSuccess, true, data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ResponseModel UpdateCarriageById(UpdateCarriageDTO inputData)
        {
            var existingData = _railwayTicketDbContext.Carriages.FirstOrDefault(x => x.Id == inputData.Id && x.AcitveFlag);
            if (existingData != null)
            {
                _mapper.Map(inputData, existingData);
                existingData.UpdatedAt = DateTime.UtcNow;

                _railwayTicketDbContext.SaveChanges();
                return new ResponseModel(MessageModel.UpdateSuccess, true, inputData);
            }
            return new ResponseModel("Data not found.", false);
        }
    }
}
