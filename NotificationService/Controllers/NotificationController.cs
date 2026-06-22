using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Dtos;
////using NotificationService.Services.Impl;
////using NotificationService.Services;

namespace NotificationService.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        [HttpPost] 
        public async Task<ActionResult> CreateNotificationAsync([FromBody] NotificationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Console.WriteLine($"Notification {dto.OrderId} processed {dto.Message}");
            return Ok(new
            {
                Status = "Notification processed successfully.",
                Message = dto.Message
            });
        }
    }
}
