using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailServices)
        {
            _emailService = emailServices;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail(string to, string subject, string message)
        {
            await _emailService.SendEmailAsync(to, subject, message);
            return Ok("Email sent successfully!");
        }
    }
}
