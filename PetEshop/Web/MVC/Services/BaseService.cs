using Infrastructure.Exeptions;
using MVC.ViewModels;

namespace MVC.Services
{    public class BaseService
    {
        protected async Task<TResult> ExecutSafeAsync<TResult>(Func<Task<TResult>> func)
            where TResult : ErrorViewModel, new()
        {
            try
            {
                return await func();
            }
            catch (BusinessException be)
            {
                return new TResult()
                {
                    RequestId = be.Message
                };
            }
            catch (Exception ex)
            {
                return new TResult()
                {
                    RequestId = ex.Message
                };
            }
        }
    }
}
