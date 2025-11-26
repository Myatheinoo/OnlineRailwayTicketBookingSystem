using AutoMapper;
using Database.DTO;
using Database.DTO.Shared;
using Database.DTO.TrainTypeDTO;
using Database.Models;
using Domain.IServices;

namespace Domain.Services
{
    public class TrainServices : ITrainServices
    {
        private readonly RailwayTicketDbContext _railwayTicketDbContext;
        private readonly IMapper _mapper;
        public TrainServices(RailwayTicketDbContext railwayTicketDbContext, IMapper mapper)
        {
            _railwayTicketDbContext = railwayTicketDbContext;
            _mapper = mapper;
        }

        public ResponseModel GetAllTrainTypes()
        {
            var trainTypes = _railwayTicketDbContext.TrainTypes.Where(x => x.ActiveFlag).ToList();
            return new ResponseModel(MessageModel.Success,true,trainTypes);
        }

        public ResponseModel GetTrainTypesById(int id)
        {
            var trainTypes = _railwayTicketDbContext.TrainTypes.Where(x => x.Id == id && x.ActiveFlag).FirstOrDefault();
            if(trainTypes is null)
            {
                return new ResponseModel(MessageModel.UnSuccess,false);
            }
            return new ResponseModel(MessageModel.Success, true, trainTypes);
        }

        public ResponseModel AddTrainType(string name)
        {
            try
            {
                var existData = _railwayTicketDbContext.TrainTypes.Where(x => x.Name == name && x.ActiveFlag).FirstOrDefault();
                if(existData != null)
                {
                    return new ResponseModel("Train type already exist.", false, existData);
                }
                if (string.IsNullOrEmpty(name) || name == " ")
                {
                    throw new ArgumentNullException("name");
                }
                var trainType = new TrainType
                {
                    Name = name,
                    TrainTypeId = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    ActiveFlag = true,
                };
                _railwayTicketDbContext.TrainTypes.Add(trainType);
                _railwayTicketDbContext.SaveChanges();
                return new ResponseModel(MessageModel.AddSuccess, true, name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ResponseModel UpdateTrainType(TrainsTypeDTO trainType)
        {
            var trainTypeData = _railwayTicketDbContext.TrainTypes.Where(x => x.Id == trainType.Id && x.ActiveFlag).FirstOrDefault();
            if (trainTypeData is null)
            {
                return new ResponseModel(MessageModel.UnSuccess, false);
            }
            if (string.IsNullOrEmpty(trainType.Name))
            {
                throw new ArgumentNullException("name");
            }
            trainTypeData.Name = trainType.Name;
            trainTypeData.UpdatedAt = DateTime.UtcNow;
            _railwayTicketDbContext.TrainTypes.Update(trainTypeData);
            _railwayTicketDbContext.SaveChanges();

            return new ResponseModel(MessageModel.UpdateSuccess, true, trainType.Name);
        }
        public ResponseModel AddTrain(TrainsDTO newTrain)
        {
            try
            {
                var existingData = _railwayTicketDbContext.Trains.Where(x => x.TrainNo == newTrain.TrainNo).FirstOrDefault();
                var trainType = _railwayTicketDbContext.TrainTypes.Where(x => x.Id == newTrain.TrainTypeId).FirstOrDefault();
                if (existingData is null && trainType != null)
                {
                    var data = _mapper.Map<Train>(newTrain);
                    data.TrainId = Guid.NewGuid();
                    data.CreatedAt = DateTime.UtcNow;
                    data.ActiveFlag = true;
                    //var data = new Train
                    //{
                    //    TrainId = Guid.NewGuid(),
                    //    TrainName = newTrain.Name,
                    //    TrainNo = newTrain.TrainNo,
                    //    TrainTypeId = newTrain.TrainTypeId,
                    //    StartedUsingDate = newTrain.StartUsingDate,
                    //    Power = newTrain.Power,
                    //    ManufactureBy = newTrain.ManufactureBy,
                    //    CreatedAt = DateTime.UtcNow,
                    //    ActiveFlag = true
                    //};
                    _railwayTicketDbContext.Trains.Add(data);
                    _railwayTicketDbContext.SaveChanges();
                    return new ResponseModel(MessageModel.AddSuccess, true, data);
                }
                return new ResponseModel(MessageModel.UnSuccess, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ResponseModel GetAllTrains()
        {
            try
            {
                var trainData = _railwayTicketDbContext.Trains.Where(x => x.ActiveFlag == true).ToList();
               var trainResponsedata = new List<TrainResponseDataDTO>();
                if (trainData.Count > 0)
                {
                    foreach(var train in trainData)
                    {
                        var traintypes = _railwayTicketDbContext.TrainTypes.Where(x => x.Id == train.TrainTypeId).FirstOrDefault();
                        var trains = new TrainResponseDataDTO
                        {
                            TrainName = train.TrainName,
                            TrainNo = train.TrainNo,
                            TrainType = traintypes!.Name,
                            ManufacutredBy = train.ManufactureBy
                        };
                        trainResponsedata.Add(trains);
                    }
                    return new ResponseModel(MessageModel.Success, true, trainResponsedata);
                }
                return new ResponseModel("No Data Found.", false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ResponseModel GetTrainById(int trainId)
        {
            try
            {
                var trainData = _railwayTicketDbContext.Trains.Where(x => x.ActiveFlag == true && x.Id == trainId).FirstOrDefault();
                if (trainData != null)
                {
                    return new ResponseModel(MessageModel.Success, true, trainData);
                }
                return new ResponseModel("No Data Found.", false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel UpdateTrain(UpdateTrainDTO trainsDTO)
        {
            var trainData = _railwayTicketDbContext.Trains.FirstOrDefault(x => x.Id == trainsDTO.Id && x.ActiveFlag == true);
            if (trainData != null)
            {
                _mapper.Map(trainsDTO, trainData);
                trainData.UpdatedAt = DateTime.UtcNow;
                _railwayTicketDbContext.SaveChanges();
                return new ResponseModel(MessageModel.UpdateSuccess, true, trainData);
            }
            return new ResponseModel(MessageModel.UnSuccess, false);    
        }
    }
}
