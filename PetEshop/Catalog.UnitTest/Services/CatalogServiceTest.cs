using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Abstractions;
using Infrastructure.Enums;
using Infrastructure.Exceptions;

namespace Catalog.UnitTest.Services
{
    public class CatalogServiceTest
    {
        private readonly ICatalogService _serviceCatalog;

        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly Mock<IItemRepository> _itemRepository;
        private readonly Mock<ITypeRepository> _typeRepository;

        public CatalogServiceTest()
        {
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();
            _itemRepository = new Mock<IItemRepository>();
            _typeRepository = new Mock<ITypeRepository>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();

            _dbContextWrapper
                .Setup(s => s.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(dbContextTransaction.Object);

            _serviceCatalog = new CatalogService(
                _dbContextWrapper.Object,
                _logger.Object,
                _itemRepository.Object,
                _typeRepository.Object,
                _mapper.Object);
        }

        [Fact]
        public async Task GetByPageAsync_Succusfull()
        {
            //arrage
            int pageSizeTest = 2;
            int pageIndexTest = 1;
            int totalCountTest = 3;
            int typeFilter = 2;

            var paginationItemReponceSeccusfull = new PaginatedItems<ItemEntity>()
            {
                TotalCount = totalCountTest,
                Data = new List<ItemEntity>()
                {
                    new ItemEntity()
                    {
                        Name = "NameTest",
                        Description = "DescriptionTest",
                        Price = 1000,
                        AvailableStock = 100,
                        TypeId = 2,
                        PictureFileName = "1.pngTest"
                    },
                    new ItemEntity()
                    {
                        Name = "NameTest",
                        Description = "DescriptionTest",
                        Price = 1000,
                        AvailableStock = 100,
                        TypeId = 2,
                        PictureFileName = "1.pngTest"
                    },
                    new ItemEntity()
                    {
                        Name = "NameTest",
                        Description = "DescriptionTest",
                        Price = 1000,
                        AvailableStock = 100,
                        TypeId = 2,
                        PictureFileName = "1.pngTest"
                    }
                }
            };

            var itemRequestSuccesfull = new PaginatedItemsRequest()
            {
                FilterTypeId = typeFilter,
                PageIndex = pageIndexTest,
                PageSize = pageSizeTest,
            };

            var countData = paginationItemReponceSeccusfull.Data.Count();

            ItemDto _itemDtoSuccesfull = new ItemDto()
            {
                Name = "Test",
                AvailableStock = 100,
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

            var catalogItemSuccesfull = new ItemEntity()
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Price = 1000,
                AvailableStock = 100,
                TypeId = 2,
                PictureFileName = "1.pngTest"
            };

            _itemRepository.Setup(s => s.GetItemsByPageAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
            .ReturnsAsync(paginationItemReponceSeccusfull);

            _mapper.Setup(s => s.Map<ItemDto>(
                    It.IsAny<ItemEntity>()))
                .Returns(_itemDtoSuccesfull);

            //act
            var reesponce = await _serviceCatalog.GetItemByPageAsync(itemRequestSuccesfull);

            //assert
            reesponce.Should().NotBeNull();
            reesponce.Count.Should().Be(totalCountTest);
            reesponce.Data.Should().NotBeNull();
            reesponce.Data.Count(s => s.Name == "Test").Should().Be(countData);
            reesponce.PageIndex.Should().Be(pageIndexTest);
            reesponce.PageSize.Should().Be(pageSizeTest);
        }

        [Fact]
        public async Task GetByPageAsync_Failed()
        {
            //arrage
            var pag = new PaginatedItemsRequest()
            {
                PageIndex = 2000,
                PageSize = 1000,
                FilterTypeId = 1
            };
            var pageIndexTest = 2000;
            var pageSizeTest = 1000;
            int typeFilter = 1;


            _itemRepository.Setup(s => s.GetItemsByPageAsync(
                It.Is<int>(i => i == pageIndexTest),
                It.Is<int>(i => i == pageSizeTest),
                It.Is<int>(i => i == typeFilter)))
            .ThrowsAsync(new BusinessException("test"));

            //act
            var responce = await _serviceCatalog.GetItemByPageAsync(pag);

            //assert
            responce.Should().NotBeNull();
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

            _itemRepository
                .Setup(s => s.GetItemsByTypeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(list);

            //act
            var reponce = await _serviceCatalog.GetItemByIdAsync(id);

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

            _itemRepository
                .Setup(s => s.GetItemsByTypeIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new BusinessException(messageText));

            //_mapper.Setup(s => s.Map<ItemDto>(It.IsAny<ItemEntity>())).Returns(dto!);

            //act
            var responce = await _serviceCatalog.GetItemByIdAsync(id);

            //asert
            responce.Should().NotBeNull();
            responce.RespCode.Should().NotBeNull();
            responce.ErrorMessage.Should().NotBeNull();
            responce.RespCode.Should().Be(ResponceCode.Failed);
            responce.ErrorMessage.Should().Be("Test1");
        }
    }
}
