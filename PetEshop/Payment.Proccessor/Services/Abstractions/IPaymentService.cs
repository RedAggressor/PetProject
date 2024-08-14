using Payment.Proccessor.Models;

namespace Payment.Proccessor.Services.Abstractions
{
    public interface IPaymentService
    {
        Task<DataResponse> GetDataSignature(JsonRequest jsonModel);
    }
}
