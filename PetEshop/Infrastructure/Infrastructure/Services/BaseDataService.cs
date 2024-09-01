using Infrastructure.Enums;
using Infrastructure.Exceptions;
using Infrastructure.Models;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services;
public abstract class BaseDataService<T>
    where T : DbContext
{
    private readonly IDbContextWrapper<T> _dbContextWrapper;
    private readonly ILogger<BaseDataService<T>> _logger;

    protected BaseDataService(
        IDbContextWrapper<T> dbContextWrapper,
        ILogger<BaseDataService<T>> logger)
    {
        _dbContextWrapper = dbContextWrapper;
        _logger = logger;
    }

    protected Task ExecuteSafeAsync(
        Func<Task> action,
        CancellationToken cancellationToken = default) =>

        ExecuteSafeAsync(token => action(), cancellationToken);

    protected Task<TResult> ExecuteSafeAsync<TResult>(
        Func<Task<TResult>> action,
        CancellationToken cancellationToken = default)
        where TResult : BaseResponse, new()
        =>
        ExecuteSafeAsync(token => action(), cancellationToken);

    private async Task ExecuteSafeAsync(
        Func<CancellationToken,
        Task> action,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await _dbContextWrapper.BeginTransactionAsync(cancellationToken);

        try
        {
            await action(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (BusinessException bx)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(bx, $"transaction is rollbacked");             
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, $"transaction is rollbacked");
        }

    }

    private async Task<TResult> ExecuteSafeAsync<TResult>(
        Func<CancellationToken,
        Task<TResult>> action,
        CancellationToken cancellationToken = default)
        where TResult : BaseResponse, new()
    {
        await using var transaction = await _dbContextWrapper.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await action(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return result;
        }
        catch (BusinessException bx)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(bx, $"transaction is rollbacked");

            return new TResult()
            {
                ErrorMessage = bx.Message,                
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, $"transaction is rollbacked");

            return new TResult() 
            { 
                 ErrorMessage = ex.Message,                
            };
        }        
    }
}