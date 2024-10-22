using Payment.Proccessor.Models;
using Payment.Proccessor.Services.Abstractions;
using LiqPay.SDK;
using LiqPay.SDK.Dto;
using LiqPay.SDK.Dto.Enums;

namespace Payment.Proccessor.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentConnectionService _paymentConnectionService;
        private readonly IDecodingService _decodingService;



        public PaymentService(
            IPaymentConnectionService paymentConnectionService,
            IDecodingService decodingService)
        {
            _paymentConnectionService = paymentConnectionService;
            _decodingService = decodingService;
        }

        public async Task<BaseResponse> SetStatusAsync(string data, string signature)
        {
            try
            {
                var jsonModelResponse = await _decodingService.GetJsomFromData(data, signature);


                if(jsonModelResponse is LiqPayResponse liq)
                {
                    var status = liq.Status.ToString();
                    var orderId = liq.OrderId.ToString();
                    return await _paymentConnectionService.SetOrderStatusAsync(orderId, status);
                }

                return new BaseResponse()
                {
                    ErrorMessage = "somthing go wrong may be it`s signature"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new DataResponse<string>
                {
                    Data = null,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<DataResponse<string>> GetPaymentForm(PaymentData jsonData)
        {
            try
            {
                var liqPayClient = new LiqPayClient(Keys.MyPublicKey, Keys.MyPrivateKey);
                var request = new LiqPayRequest()
                {
                    Action = LiqPayRequestAction.Pay,
                    Version = 3,
                    Amount = jsonData.Amount,
                    Currency = jsonData.Currency,
                    Description = jsonData.Description,
                    OrderId = jsonData.OrderId,
                    Sandbox = "1",
                    ServerUrl = "https://28d6-62-122-70-171.ngrok-free.app/api/v1/Payment/GetOrderStatus",
                    ResultUrl = "http://www.fruitshop.com:3000/succusfullpay",                     
                };

                var form = liqPayClient.CNBForm(request);
                return await Task.FromResult(new DataResponse<string>()
                { 
                    Data = form
                });
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return await Task.FromResult(new DataResponse<string>() 
                {
                    Data = null,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
