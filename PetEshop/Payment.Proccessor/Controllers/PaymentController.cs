using Payment.Proccessor.Models;
using Payment.Proccessor.Services.Abstractions;
using LiqPay.SDK.Dto;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Payment.Proccessor.Controllers
{
    [ApiController]
    //[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService) 
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPaymentLink(PaymentData jsonRequest)
        {            
            var data = await _paymentService.GetPaymentForm(jsonRequest);           
            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderStatus([FromForm] string data, [FromForm] string signature)
        {           
            //var data = await _paymentService.SetStatusAsync(orderId);
            return Ok( data);
        }
    }
}
