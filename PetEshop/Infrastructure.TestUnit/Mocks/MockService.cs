namespace Infrastructure.TestUnit.Mocks
{
    internal class MockService : BaseDataService<MockDbContext>
    {
        public MockService(
            IDbContextWrapper<MockDbContext> dbContextWrapper,
            ILogger<MockService> logger)
            : base(dbContextWrapper, logger)
        {
        }

        public async Task RunWithException()
        {
            await ExecuteSafeAsync(() => throw new Exception());
        }

        public async Task RunWithuotException()
        {
            await ExecuteSafeAsync(() => Task.CompletedTask);
        }
    }
}
