using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTest.Services
{
    public class CatalogTypeServiceTest
    {
        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<ILogger<CatalogTypeService>> _logger;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<IMapper> _mapper;

        private readonly CatalogTypeService _catalogTypeService;

        public CatalogTypeServiceTest()
        {
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _logger = new Mock<ILogger<CatalogTypeService>>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper
                .Setup(s => s.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(dbContextTransaction.Object);

            _catalogTypeService = new CatalogTypeService(
                _catalogTypeRepository.Object,
                _mapper.Object,
                _dbContextWrapper.Object,
                _logger.Object);
        }

        [Fact]
        public async Task Add_Seccusfull()
        {
            //arrage
            var inTest = "test";
            var outTest = 1;

            _catalogTypeRepository
                .Setup(s => 
                    s.AddTypeAsync(It.IsAny<string>()))
                .ReturnsAsync(outTest);

            //act
            var responce = await _catalogTypeService.AddType(inTest);

            //assert
            responce.Should().NotBeNull();
            responce.Id.Should().Be(outTest);
            responce.Id.Should().NotBeNull();
            responce.ErrorMessage.Should().BeNull();
            responce.RespCode.Should().BeNull();
        }

        [Fact]
        public async Task Add_Failed()
        {
            //arrage
            string? inTest = null;
            int? outTest = null;

            _catalogTypeRepository
                .Setup(s => 
                    s.AddTypeAsync(It.IsAny<string>()))
                .ReturnsAsync(outTest);

            //act
            var responce = await _catalogTypeService.AddType(inTest);

            //assert
            responce.Should().NotBeNull();
            responce.Id.Should().BeNull();
            responce.ErrorMessage.Should().NotBeNull();
            responce.RespCode.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_succesfull()
        {
            //arrage
            var inTest = 1;
            var outTest = "test";

            _catalogTypeRepository
                .Setup(s=>
                    s.DeleteType(It.IsAny<int>()))
                .ReturnsAsync(outTest);

            //act
            var responce = await _catalogTypeService.DeleteType(inTest);

            //assert
            responce.Should().NotBeNull();
            responce.Status.Should().Be(outTest);
            responce.Status.Should().NotBeNull();
            responce.ErrorMessage.Should().BeNull();
            responce.RespCode.Should().BeNull();
        }

        [Fact]
        public async Task Delete_failed()
        {
            //arrage
            int? inTest = null;
            string? outTest = "test";

            _catalogTypeRepository
                .Setup(s => 
                    s.DeleteType(It.IsAny<int>()))
                .ReturnsAsync(outTest);

            //act
            var responce = await _catalogTypeService.DeleteType(inTest);

            //assert
            responce.Should().NotBeNull();
            responce.Status.Should().BeNull();
            responce.ErrorMessage.Should().NotBeNull();
            responce.RespCode.Should().NotBeNull();
        }

        [Fact]
        public async Task Update_Succesfull()
        {
            //arrage
            var dto = new CatalogTypeDto()
            {
                Type = "test"
            };
            var entity = new CatalogType()
            {
                Type= "test"
            };

            _mapper.Setup(s => s.Map<CatalogType>(It.Is<CatalogTypeDto>(i => i.Equals(dto)))).Returns(entity);

            _catalogTypeRepository
                .Setup(s => 
                    s.Update(It.Is<CatalogType>(i => 
                        i.Equals(entity))))
                .ReturnsAsync(entity);
                        
            _mapper
                .Setup(s => 
                    s.Map<CatalogTypeDto>(It.Is<CatalogType>(i => 
                        i.Equals(entity))))
                .Returns(dto);

            //act
            var responce = await _catalogTypeService.UpdateType(dto);

            //assert
            responce.Should().NotBeNull();
            responce.ErrorMessage.Should().BeNull();
            responce.RespCode.Should().BeNull();
            responce.UpdataModel.Should().NotBeNull();
            responce.UpdataModel.Should().BeSameAs(dto);
        }

        [Fact]
        public async Task Update_Failed()
        {
            //arrage
            CatalogTypeDto? dto = null;
            CatalogType? entity = null;

            _mapper
                .Setup(s => 
                    s.Map<CatalogType>(It.Is<CatalogTypeDto>(i => 
                        i.Equals(dto))))
                .Returns(entity);

            _catalogTypeRepository
                .Setup(s => 
                    s.Update(It.Is<CatalogType>(i => 
                        i.Equals(entity))))
                .ReturnsAsync(entity);

            _mapper
                .Setup(s => 
                    s.Map<CatalogTypeDto>(It.Is<CatalogType>(i => 
                        i.Equals(entity))))
                .Returns(dto);

            //act
            var responce = await _catalogTypeService.UpdateType(dto);

            //assert
            responce.Should().NotBeNull();
            responce.ErrorMessage.Should().NotBeNull();
            responce.RespCode.Should().NotBeNull();
            responce.UpdataModel.Should().BeNull();
            responce.UpdataModel.Should().BeSameAs(dto);
        }
    }
}
