using Database.DTO.TrainTypeDTO;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrainServices _trainServices;

        public TrainController(ITrainServices trainServices)
        {
            _trainServices = trainServices;
        }

        [HttpGet("GetAllTrainsTypes")]
        public IActionResult GetAllTrainTypes()
        {
            var reslt = _trainServices.GetAllTrainTypes();
            if (reslt.IsSuccess)
            {
                return Ok(reslt);
            }
            return BadRequest(reslt);
        }

        [HttpPost("GetTrainTypesById")]
        public IActionResult GetTrainTypesById(int id)
        {
            var reslt = _trainServices.GetTrainTypesById(id);
            if (reslt.IsSuccess)
            {
                return Ok(reslt);
            }
            return BadRequest(reslt);
        }

        [HttpPost("AddTrainType")]
        public IActionResult AddTrainType(string name)
        {
            var result = _trainServices.AddTrainType(name);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdateTrainType")]
        public IActionResult UpdateTrainType(TrainsTypeDTO trainType)
        {
            var result = _trainServices.UpdateTrainType(trainType);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddTrain")]
        public IActionResult AddTrain(TrainsDTO newTrain)
        {
            var result = _trainServices.AddTrain(newTrain);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetAllTrains")]
        public IActionResult GetAllTrains()
        {
            var result = _trainServices.GetAllTrains();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("GetTrainById")]
        public IActionResult GetTrainById(int trainId)
        {
            var result = _trainServices.GetTrainById(trainId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdateTrainById")]
        public IActionResult UpdateTrain(UpdateTrainDTO trainsDTO)
        {
            var result = _trainServices.UpdateTrain(trainsDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
