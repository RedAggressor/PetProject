using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Repositories.Abstractions;
using Infrastructure.Enums;
using Infrastructure.Exceptions;

namespace Catalog.UnitTest.Services
{
    public class ItemServiceTest
    {
        private readonly IItemService _itemService;

        private readonly Mock<IItemRepository> _itemRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<ItemService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly ItemEntity _itemEntitySeccessfull = new ItemEntity()
        {
            Name = "NameTest",
            Description = "DescriptionTest",
            Price = 1000,
            AvailableStock = 100,
            TypeId = 1,
            PictureFileName = "1.pngTest"
        };

        private readonly ItemDto _itemDtoSuccesfull = new ItemDto()
        {
            Name = "Test",
            AvailableStock = 5,
            Description = "Test",
            Price = 10,
            PictureUrl = "Test",
            Id = 1,
            Type = new TypeDto()
            {
                Id = 2,
                Type = "test"
            }
        };

        public ItemServiceTest()
        {
            _itemRepository = new Mock<IItemRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<ItemService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper
                .Setup(s => s.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(dbContextTransaction.Object);

            _itemService = new ItemService(
                _dbContextWrapper.Object,
                _logger.Object,
                _itemRepository.Object,
                _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Seccusfull()
        {
            //arrage
            var testResult = 1;
            var testItem = new CreateItemRequest()
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Price = 1000,
                AvailableStock = 100,
                TypeId = 1,
                PictureFileName = "1.pngTest"
            };

            _itemRepository
                .Setup(s => s.AddItemAsync(
                    It.IsAny<ItemEntity>()))
                .ReturnsAsync(testResult);

            //act
            var result = await _itemService
                .AddItemAsync(testItem);

            //asert
            result.Should().NotBeNull();
            result.Id.Should().Be(testResult);
            result.RespCode.Should().Be(ResponceCode.Seccusfull);
            result.ErrorMessage.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            //arrage
            var testItem = new CreateItemRequest();            

            _itemRepository.Setup(s => s.AddItemAsync(                    
                    It.IsAny<ItemEntity>()))
                .ThrowsAsync(new Exception("Test exceptions"));

            //act
            var result = await _itemService
                .AddItemAsync(testItem);

            //asert
            result.Should().NotBeNull();
            result.Id.Should().Be(default(int));
            result.RespCode.Should().Be(ResponceCode.Failed);
            result.ErrorMessage.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCatalogItemsByIdAsync_Succesfull()
        {
            //arrage
            int id = 1;

            _itemRepository
                .Setup(s => s.GetItemByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_itemEntitySeccessfull);

            _mapper.Setup(s => s.Map<ItemDto>(
                    It.Is<ItemEntity>(i => i.Equals(_itemEntitySeccessfull))))
                .Returns(_itemDtoSuccesfull);

            //act
            var response = await _itemService.GetItemByIdAsync(id);

            //asert
            response.Should().NotBeNull();
            response.Equals(_itemDtoSuccesfull);
            response.ErrorMessage.Should().BeNullOrEmpty();
            response.RespCode.Should().Be(ResponceCode.Seccusfull);
            response.ItemDto.Should().NotBeNull();
            response.ItemDto.Should().BeSameAs(_itemDtoSuccesfull);
        }

        [Fact]
        public async Task GetCatalogItemsByIdAsync_Failedl()
        {
            //arrage
            int id = 756765776;

            _itemRepository
                .Setup(s => s.GetItemByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new BusinessException("test"));

            //act
            var response = await _itemService.GetItemByIdAsync(id);

            //asert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().NotBeNullOrEmpty();
            response.RespCode.Should().Be(ResponceCode.Failed);
            response.ItemDto.Should().BeNull();
        }

        [Fact]
        public async Task Delete_Seccusfull()
        {
            //arrage
            int id = 1;

            var answer = "text";

            _itemRepository
                .Setup(s => s.DeleteItemAsync(It.IsAny<int>()))
                .ReturnsAsync(answer);

            //act
            var reponse = await _itemService.DeleteItemAsync(id);

            //asert
            reponse.Should().NotBeNull();
            reponse.Status.Should().Be(answer);
            reponse.ErrorMessage.Should().BeNullOrEmpty();
            reponse.RespCode.Should().Be(ResponceCode.Seccusfull);
        }

        [Fact]
        public async Task Delete_Failed()
        {
            //arrage
            int id = 343534787;

            _itemRepository
                .Setup(s => s.DeleteItemAsync(It.IsAny<int>()))
                .ThrowsAsync(new BusinessException("test"));

            //act
            var response = await _itemService.DeleteItemAsync(id);

            //asert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().NotBeNullOrEmpty();
            response.RespCode.Should().Be(ResponceCode.Failed);
            response.Status.Should().BeNullOrEmpty();            
        }

        [Fact]
        public async Task Update_Succesfull()
        {
            //arrage            

            _mapper
                .Setup(s => 
                    s.Map<ItemEntity>(It.Is<ItemDto>(i => 
                        i.Equals(_itemDtoSuccesfull))))
                .Returns(_itemEntitySeccessfull);

            _itemRepository
                .Setup(s => s.UpdateItemAsync(It.IsAny<ItemEntity>()))
                .ReturnsAsync(_itemEntitySeccessfull);

            _mapper
                .Setup(s => 
                    s.Map<ItemDto>(It.Is<ItemEntity>(i => 
                        i.Equals(_itemEntitySeccessfull))))
                .Returns(_itemDtoSuccesfull);

            //act
            var response = await _itemService
                .UpdateItemAsync(_itemDtoSuccesfull);

            //asert
            response.Should().NotBeNull();
            response.UpdataModel.Should().BeSameAs(_itemDtoSuccesfull);
            response.RespCode.Should().Be(ResponceCode.Seccusfull);
            response.ErrorMessage.Should().BeNullOrEmpty();            
        }

        [Fact]
        public async Task Update_Failed()
        {
            //arrage
            string messageTest = "Test";
            var catalog = new ItemDto();

            _itemRepository
                .Setup(s => s.UpdateItemAsync(It.IsAny<ItemEntity>()))
                .ThrowsAsync(new BusinessException(messageTest));

            //act
            var responce = await _itemService.UpdateItemAsync(catalog);

            //asert            
            responce.Should().NotBeNull();
            responce.RespCode.Should().NotBeNull();
            responce.ErrorMessage.Should().NotBeNull();           
            responce.RespCode.Should().Be(ResponceCode.Failed);            
            responce.UpdataModel.Should().BeNull();
        }        
    }
}
