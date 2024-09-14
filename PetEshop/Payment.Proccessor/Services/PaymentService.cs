using Payment.Proccessor.Models;
using Payment.Proccessor.Services.Abstractions;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Payment.Proccessor.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<DataResponse> GetDataSignature(JsonRequest jsonModel)
        {
            var dataResponse = new DataResponse();
            dataResponse.Data = await GetData(GetPaymentKey(jsonModel));
            var privateKey = new JsonModel().PrivateKey;
            dataResponse.Signature = GenerateSignature(privateKey, dataResponse.Data);
            return dataResponse;
        }

        private JsonModel GetPaymentKey(JsonRequest json)
        {
            return new JsonModel()
            {
                Action = json.Action,
                Amount = json.Amount,
                Currency = json.Currency,
                Description = json.Description,
                OrderId = json.OrderId,
                Version = json.Version,
            };

        }

        private async Task<string> GetData(JsonModel jsonModel)
        {
            string jsonString = JsonSerializer.Serialize(jsonModel);
            return await Task.FromResult(Base64Encode(jsonString));
        }

        private string Base64Encode(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(bytes);
        }

        private byte[] Sha1(string input)
        {
            byte[] tmpSource = Encoding.UTF8.GetBytes(input);
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                return sha1.ComputeHash(tmpSource);
            }

        }

        private string GenerateSignature(string privateKey, string data)
        {
            string signString = $"{privateKey}{data}{privateKey}";
            string signature = Convert.ToBase64String(Sha1(signString));
            return signature;
        }

    }
}
