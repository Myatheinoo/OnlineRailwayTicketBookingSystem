using Database.DTO;
using Database.DTO.CarriageDTO;

namespace Domain.IServices
{
    public interface ICarriageService
    {
        public Task<ResponseModel> GetAllCarriage();
        public ResponseModel AddCarriage(CarriageDTO newCarriage);
        public ResponseModel UpdateCarriageById(UpdateCarriageDTO inputData);
    }
}
