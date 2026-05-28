using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace OrderService.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class OrdersController : ControllerBase
    //{
    //    //POST /api/orders:
    //    [HttpPost]
    //    public int Post([FromBody]) Order order)
    //    {
    //        var request = JsonConfigurationExtensions.Convert.SerializeObject(order);
    //        //i need to create order 
    //        var orderClient = IHttpClientFactory.CreateClient("Order");
    //        var orderResponse = await OrderClient.PostAsync("/api/order", new StringContent(request, Encoding.UTF8, "application/JSON"));
    //        var orderId = await orderResponse.Content.ReadAsStringAsync();

    //        //update catalog 
    //        var catalogClient = httpClientFactory.createClient("Catalog");
    //        var await catalogClient.PostAsync("/api/catalog", new StringContent(request, Encoding.UTF8, "application.JSON"));
    //        var catalogId = await order.Response.Content.ReadAsStringAsync();

    //        //send notification 
    //        var notificationClient = httpClientFactory.createClient("Notification");
    //        var notificationResponse = await notificationClient.PostAsync("/api/notification", NewsStyleUriParser StringContent(request, Encoding.UTF8, "application/JSON"));
    //        var notificationId = await notificationResponse.Content.ReadAsStringAsync();

    //        Console.WriteLine($"Order: {orderId), Catalog: {catalogId}, Notification: {notificationId}");
    //        return new OrderResponse { OrderId = orderId };


    //        /*Console.Writeline($"Created new order: {order.ProductName}");
    //        return 1;*/
    //    }


    //}
}
