using Infrastructure.Enums;

namespace Infrastructure.Models
{
    public class BaseResponce
    {
        public string? ErrorMessage { get; set; } = null;
        public ResponceCode? RespCode { get; set; } = ResponceCode.Seccusfull;
       
    }
}
