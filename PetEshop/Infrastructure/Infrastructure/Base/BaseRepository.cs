using Infrastructure.Exeptions;

namespace Infrastructure.Repositories
{
    public class BaseRepository
    {
        private readonly ILogger<BaseRepository> _logger;
        public BaseRepository(ILogger<BaseRepository> logger) 
        { 
            _logger = logger;
        }

        protected async Task<TResult> ExecutSafeAsync<TResult>(Func<Task<TResult>> func)
        {
            try 
            {
                return await func();
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BusinessException(ex.Message);
            }
        }
    }
}
