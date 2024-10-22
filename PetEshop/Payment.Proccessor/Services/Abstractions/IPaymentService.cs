using LiqPay.SDK.Dto;
using Payment.Proccessor.Models;

namespace Payment.Proccessor.Services.Abstractions
{
    public interface IPaymentService
    {
        Task<BaseResponse> SetStatusAsync(string data, string signature);
        Task<DataResponse<string>> GetPaymentForm(PaymentData jsonData);
    }
}
