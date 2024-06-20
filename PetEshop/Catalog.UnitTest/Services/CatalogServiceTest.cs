﻿using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.enums;
using Catalog.Host.Models.Response;

namespace Catalog.UnitTest.Services
{
    public class CatalogServiceTest
    {
        private readonly ICatalogService _serviceCatalog;

        private readonly Mock<ICatalogItemRepository> _catalogRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;

        public CatalogServiceTest()
        {
            _catalogRepository = new Mock<ICatalogItemRepository>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _serviceCatalog = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetByPageAsync_Succusfull()
        {
            //arrage
            int pageSizeTest = 1;
            int pageIndexTest = 5;
            int totalCountTest = 12;
            int brandFilter = 1;
            int typeFilter = 1;
            var filter = new Dictionary<CatalogTypeFilter, int>()
            {                
                [CatalogTypeFilter.Type] = 1 
            };

            var paginationItemReponceSeccusfull = new PaginatedItems<CatalogItem>()
            {
                TotalCount = totalCountTest,
                Data = new List<CatalogItem>()
                {
                    new CatalogItem()
                    {
                         Name ="Test",
                    },
                    new CatalogItem()
                    {
                         Name ="Test",                         
                    },
                    new CatalogItem()
                    {
                         Name ="Test",                         
                    },
                }
            };

            var countData = paginationItemReponceSeccusfull.Data.Count();

            var catalogItemDtoSuccesfull = new CatalogItemDto()
            {
                Name = "Test",                              
            };

            var catalogItemSuccesfull = new CatalogItem()
            {
                Name = "Test",
            };            

            _catalogRepository.Setup(s => s.GetByPageAsync(
                It.Is<int>(i => i == pageIndexTest),
                It.Is<int>(i => i == pageSizeTest),
                It.Is<int>(i=> i == brandFilter),
                It.Is<int>(i=>i == typeFilter)
                ))
            .ReturnsAsync(paginationItemReponceSeccusfull);

            _mapper.Setup(s => s.Map<CatalogItemDto>(
                    It.Is<CatalogItem>(i => i.Name == "Test")))
                .Returns(catalogItemDtoSuccesfull); // check mapper failed

            //act
            var reesponce = await _serviceCatalog.GetByPageAsync(pageSizeTest, pageIndexTest, filter);

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
            var pageIndexTest = 2000;
            var pageSizeTest = 1000;
            int? typeFilter = null;
            int? brandFilter = null;

            _catalogRepository.Setup(s => s.GetByPageAsync(
                It.Is<int>(i => i == pageIndexTest),
                It.Is<int>(i => i == pageSizeTest),
                It.Is<int?>(i=>i == typeFilter),
                It.Is<int>(i=>i== brandFilter)))
            .Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

            //act
            var responce = await _serviceCatalog.GetByPageAsync(pageSizeTest, pageIndexTest, null);

            //assert
            responce.Should().NotBeNull();
        }
    }
}