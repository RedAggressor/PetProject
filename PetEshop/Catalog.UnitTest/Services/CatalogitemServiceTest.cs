using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Abstractions;
using Infrastructure.Enums;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Catalog.UnitTest.Services
{
    public class CatalogitemServiceTest
    {
        private readonly IItemService _catalogItemService;

        private readonly Mock<IItemRepository> _catalogItemRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogItemService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly ItemEntity _testItem = new ItemEntity()
        {
            Name = "NameTest",
            Description = "DescriptionTest",
            Price = 1000,
            AvailableStock = 100,
            TypeId = 1,
            PictureFileName = "1.pngTest"
        };

        public CatalogitemServiceTest()
        {
            _catalogItemRepository = new Mock<IItemRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogItemService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogItemService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Seccusfull()
        {
            //arrage
            var testResult = 1;

            _catalogItemRepository
                .Setup(s => s.Add(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<decimal>(),
                    It.IsAny<int>(),                    
                    It.IsAny<int>(),
                    It.IsAny<string>()))
                .ReturnsAsync(testResult);

            //act
            var result = await _catalogItemService
                .Add(
                _testItem.Name,
                _testItem.Description,
                _testItem.Price,
                _testItem.AvailableStock,                
                _testItem.TypeId,
                _testItem.PictureFileName
                );

            //asert
            result.Should().NotBeNull();
            result.Id.Should().Be(testResult);
            result.RespCode.Should().BeNull();
            result.ErrorMessage.Should().BeNull();            
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            //arrage          
            _catalogItemRepository.Setup(s => s.Add(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<decimal>(),
                    It.IsAny<int>(),                    
                    It.IsAny<int>(),
                    It.IsAny<string>()))
                .ThrowsAsync(new Exception("Test exceptions"));

            //act
            var result = await _catalogItemService
                .Add(
                _testItem.Name,
                _testItem.Description,
                _testItem.Price,
                _testItem.AvailableStock,               
                _testItem.TypeId,
                _testItem.PictureFileName
                );

            //asert
            result.Should().NotBeNull();            
            result.RespCode.Should().NotBeNull();
            result.ErrorMessage.Should().NotBeNull();            
        }

        [Fact]
        public async Task GetCatalogItemsByIdAsync_Succesfull()
        {
            //arrage
            int id = 1;

            var catalogItemDtoSuccesfull = new ItemDto()
            {
                Name = "Test",
                AvailableStock = 5,
                Description = "Test",
                Price = 10,
                PictureUrl = "Test",
                Id = 1
            };

            var catalogItemSuccesfull = new ItemEntity()
            {
                Name = "Test",
                AvailableStock = 5,
                Description = "Test",
                Price = 10,                
                TypeId = 2,
                Id = 1
            };

            _catalogItemRepository
                .Setup(s => s.GetCatalogItemsByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(catalogItemSuccesfull);

            _mapper.Setup(s => s.Map<ItemDto>(
                    It.Is<ItemEntity>(i => i.Equals(catalogItemSuccesfull))))
                .Returns(catalogItemDtoSuccesfull);

            //act
            var responce = await _catalogItemService.GetCatalogItemsByIdAsync(id);

            //asert
            responce.Should().NotBeNull();
            responce.Equals(catalogItemDtoSuccesfull);
        }

        [Fact]
        public async Task GetCatalogItemsByIdAsync_Failedl()
        {
            //arrage
            int id = 0;

            _catalogItemRepository
                .Setup(s => s.GetCatalogItemsByIdAsync(It.IsAny<int>()))
                .Returns((Func<ItemDto>)null!);

            //act
            var responce = await _catalogItemService.GetCatalogItemsByIdAsync(id);

            //asert
            responce.Should().BeNull();
        }

        [Fact]
        public async Task Delete_Seccusfull()
        {
            //arrage
            int id = 1;

            var answer = "text";

            _catalogItemRepository
                .Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(answer);

            //act
            var reponce = await _catalogItemService.DeleteAsync(id);

            //asert
            reponce.Should().NotBeNull();
            reponce.Equals(answer);
        }

        [Fact]
        public async Task Delete_Failed()
        {
            //arrage
            int id = 0;

            string? answer = "";

            _catalogItemRepository
                .Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(answer);

            //act
            var reponce = await _catalogItemService.DeleteAsync(id);

            //asert
            reponce.Should().NotBeNull();
            reponce.Equals(answer);
        }

        [Fact]
        public async Task Update_Succesfull()
        {
            //arrage
            var catalogItemDtoSuccesfull = new ItemDto()
            {
                Name = "Test",
                AvailableStock = 5,
                Description = "Test",
                Price = 10,
                PictureUrl = "Test",
                Id = 1
            };

            var catalogItemSuccesfull = new ItemEntity()
            {
                Name = "Test",
                AvailableStock = 5,
                Description = "Test",
                Price = 10,
                TypeId = 2,
                Id = 1
            };

            _mapper
                .Setup(s => 
                    s.Map<ItemEntity>(It.Is<ItemDto>(i => 
                        i.Equals(catalogItemDtoSuccesfull))))
                .Returns(catalogItemSuccesfull);

            _catalogItemRepository
                .Setup(s => s.Update(It.IsAny<ItemEntity>()))
                .ReturnsAsync(catalogItemSuccesfull);


            _mapper
                .Setup(s => 
                    s.Map<ItemDto>(It.Is<ItemEntity>(i => 
                        i.Equals(catalogItemSuccesfull))))
                .Returns(catalogItemDtoSuccesfull);

            //act
            var reponce = await _catalogItemService
                .UpdateAsync(catalogItemDtoSuccesfull);

            //asert
            reponce.Should().NotBeNull();
            reponce.Description.Should().Be("Test");
        }

        [Fact]
        public async Task Update_Failed()
        {
            //arrage
            string messageTest = "Test";
            var catalog = new ItemDto();

            _catalogItemRepository
                .Setup(s => s.Update(It.IsAny<ItemEntity>()))
                .ThrowsAsync(new Exception(messageTest));

            //act
            var responce = await _catalogItemService.UpdateAsync(catalog);

            //asert
            
            responce.Should().NotBeNull();
            responce.RespCode.Should().NotBeNull();
            responce.ErrorMessage.Should().NotBeNull();
            responce.Name.Should().BeNull();
            responce.PictureUrl.Should().BeNull();
            responce.RespCode.Should().Be(ResponceCode.Failed);
            responce.ErrorMessage.Should().Be("Test");
        }

        [Fact]
        public async Task GetCatalogItemByTypeAsync_Succesfull()
        {
            //arrage
            var id = 1;
            var list = new List<ItemEntity>()
            {
                new ItemEntity()
                {
                    Name ="Test",
                },
                new ItemEntity()
                {
                    Name ="Test",
                    AvailableStock = 5,
                    Description = "Test",
                    Price = 10,
                    TypeId = 2,
                    PictureFileName = "Test"
                },
                new ItemEntity()
                {
                    Name ="Test",
                    AvailableStock = 5,
                    Description = "Test",
                    Price = 10,
                    TypeId = 2,
                    PictureFileName = "Test"
                },
            };            

            var dto = new ItemDto()
            {
                Name = "Test",
                AvailableStock = 5,
                Description = "Test",
                Price = 10,                
                Type = new TypeDto() { Type = "test" },

            };

            _mapper.Setup(s => s.Map<ItemDto>(It.IsAny<ItemEntity>())).Returns(dto);

            _catalogItemRepository
                .Setup(s => s.GetCatalogItemsByTypeAsync(It.IsAny<int>()))
                .ReturnsAsync(list);

            //act
            var reponce = await _catalogItemService.GetCatalogItemByTypeAsync(id);

            //asert
            reponce.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCatalogItemByTypeAsync_Failed()
        {
            //arrage
            int id = 7667777;

            ItemDto? dto = null;
            var messageText = "Test1";



            _catalogItemRepository
                .Setup(s => s.GetCatalogItemsByTypeAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception(messageText));

            _mapper.Setup(s => s.Map<ItemDto>(It.IsAny<ItemEntity>())).Returns(dto!);

            //act
            var responce = await _catalogItemService.GetCatalogItemByTypeAsync(id);

            //asert
            responce.Should().NotBeNull();
            responce.RespCode.Should().NotBeNull();
            responce.ErrorMessage.Should().NotBeNull();            
            responce.RespCode.Should().Be(ResponceCode.Failed);
            responce.ErrorMessage.Should().Be("Test1");
        }
    }
}
