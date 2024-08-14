using Infrastructure.Enums;

namespace Infrastructure.Models
{
    public class BaseResponse
    {
        public string? ErrorMessage { get; set; } = null;
        public virtual ResponceCode? RespCode => ErrorMessage is null ? ResponceCode.Seccusfull : ResponceCode.Failed;
       
    }
}
