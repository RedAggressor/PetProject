using Microsoft.AspNetCore.Mvc;
using Payment.Proccessor.Models;
using Payment.Proccessor.Services.Abstractions;
using System.Net;

namespace Payment.Proccessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService) 
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GenereitPaymentCode(JsonRequest jsonRequest)
        {            
            var data = await _paymentService.GetDataSignature(jsonRequest);           
            return Ok(data);
        }
    }
}
