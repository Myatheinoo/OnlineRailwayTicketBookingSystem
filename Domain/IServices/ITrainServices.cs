using Database.DTO;
using Database.DTO.TrainTypeDTO;

namespace Domain.IServices
{
    public interface ITrainServices
    {
        public ResponseModel GetAllTrainTypes();
        public ResponseModel GetTrainTypesById(int id);
        public ResponseModel AddTrainType(string name);
        public ResponseModel UpdateTrainType(TrainsTypeDTO trainType);
        public ResponseModel AddTrain(TrainsDTO newTrain);
        public ResponseModel GetAllTrains();
        public ResponseModel GetTrainById(int trainId);
        public ResponseModel UpdateTrain(UpdateTrainDTO trainsDTO);
    }
}
