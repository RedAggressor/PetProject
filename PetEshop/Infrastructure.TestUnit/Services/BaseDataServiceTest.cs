using Infrastructure.TestUnit.Mocks;

namespace Infrastructure.TestUnit.Services
{
    internal class BaseDataServiceTest
    {
        private readonly Mock<IDbContextTransaction> _dbContextTransaction;
        private readonly Mock<ILogger<MockService>> _logger;
        private readonly MockService _mockService;

        public BaseDataServiceTest() 
        {
            var dbContextWripper = new Mock<IDbContextWrapper<MockDbContext>>();
            _dbContextTransaction = new Mock<IDbContextTransaction>();
            _logger = new Mock<ILogger<MockService>>();

            dbContextWripper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(_dbContextTransaction.Object);

            _mockService = new MockService(dbContextWripper.Object, _logger.Object);
        }

        [Fact]
        public async Task ExecuteSafe_Succesfull()
        {
            // arrage

            //act
            await _mockService.RunWithuotException();

            //assert
            _dbContextTransaction.Verify(t => t.CommitAsync(CancellationToken.None), Times.Once);
            _dbContextTransaction.Verify(t => t.RollbackAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task ExecuteSafe_Failde()
        {
            // arrage

            //act
            await _mockService.RunWithException();

            //assert
            _dbContextTransaction.Verify(t => t.CommitAsync(CancellationToken.None), Times.Never);
            _dbContextTransaction.Verify(t=>t.RollbackAsync(CancellationToken.None), Times.Once);

            _logger.Verify(x=> x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString()!
                    .Contains("$transaction is rollbacked")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
              Times.Once);
        }
    }
}
