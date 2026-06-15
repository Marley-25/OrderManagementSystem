using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Dtos;
using NotificationService.Services.Impl;
using NotificationService.Services;

namespace NotificationService.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost] 
        public async Task<ActionResult> SendNotification([FromQuery] NotificationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _notificationService.SendNotificationAsync(dto);
            return Ok(new { message = "Notification sent successfully" });
        }
    }
}
