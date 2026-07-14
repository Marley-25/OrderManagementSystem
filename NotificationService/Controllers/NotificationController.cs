using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Dtos;


namespace NotificationService.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateNotificationAsync([FromBody] NotificationDto dto)
        {

            Console.WriteLine("Orderid " + dto.OrderId + " Message:" + dto.Message);
            return Ok();
        }
    }
}
        
