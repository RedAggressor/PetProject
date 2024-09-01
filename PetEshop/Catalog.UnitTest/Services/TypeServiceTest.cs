using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Abstractions;
using Infrastructure.Enums;
using Infrastructure.Exceptions;

namespace Catalog.UnitTest.Services
{
    public class TypeServiceTest
    {
        private readonly Mock<ITypeRepository> _catalogTypeRepository;
        private readonly Mock<ILogger<TypeService>> _logger;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<IMapper> _mapper;

        private readonly TypeService _catalogTypeService;

        public TypeServiceTest()
        {
            _catalogTypeRepository = new Mock<ITypeRepository>();
            _logger = new Mock<ILogger<TypeService>>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper
                .Setup(s => s.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(dbContextTransaction.Object);

            _catalogTypeService = new TypeService(
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
            var response = await _catalogTypeService.AddType(inTest);

            //assert
            response.Should().NotBeNull();
            response.Id.Should().Be(outTest);            
            response.ErrorMessage.Should().BeNullOrEmpty();
            response.RespCode.Should().NotBeNull();
            response.RespCode.Should().Be(ResponceCode.Seccusfull);
        }

        [Fact]
        public async Task Add_Failed()
        {
            //arrage
            string inTest = null!;            

            _catalogTypeRepository
                .Setup(s =>
                    s.AddTypeAsync(It.IsAny<string>()))
                .ThrowsAsync(new BusinessException("test"));

            //act
            var response = await _catalogTypeService.AddType(inTest);

            //assert
            response.Should().NotBeNull();            
            response.ErrorMessage.Should().NotBeNull();
            response.RespCode.Should().NotBeNull();
            response.RespCode.Should().Be(ResponceCode.Failed);
            response.Id.Should().Be(default(int));
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
            responce.RespCode.Should().NotBeNull();
            responce.RespCode.Should().Be(ResponceCode.Seccusfull);
        }

        [Fact]
        public async Task Delete_failed()
        {
            //arrage
            int inTest = 7868767;

            _catalogTypeRepository
                .Setup(s => 
                    s.DeleteType(It.IsAny<int>()))
                .ThrowsAsync(new BusinessException("test"));

            //act
            var response = await _catalogTypeService.DeleteType(inTest);

            //assert
            response.Should().NotBeNull();
            response.Status.Should().BeNull();
            response.ErrorMessage.Should().NotBeNull();
            response.RespCode.Should().NotBeNull();
            response.RespCode.Should().Be(ResponceCode.Failed);
        }

        [Fact]
        public async Task Update_Succesfull()
        {
            //arrage
            var dto = new TypeDto()
            {
                Type = "test"
            };

            var entity = new TypeEntity()
            {
                Type= "test"
            };

            _mapper.Setup(s => s.Map<TypeEntity>(It.Is<TypeDto>(i => i.Equals(dto)))).Returns(entity);

            _catalogTypeRepository
                .Setup(s => 
                    s.Update(It.Is<TypeEntity>(i => 
                        i.Equals(entity))))
                .ReturnsAsync(entity);
                        
            _mapper
                .Setup(s => 
                    s.Map<TypeDto>(It.Is<TypeEntity>(i => 
                        i.Equals(entity))))
                .Returns(dto);

            //act
            var responce = await _catalogTypeService.UpdateType(dto);

            //assert
            responce.Should().NotBeNull();
            responce.ErrorMessage.Should().BeNull();
            responce.RespCode.Should().NotBeNull();
            responce.RespCode.Should().Be(ResponceCode.Seccusfull);
            responce.UpdataModel.Should().NotBeNull();
            responce.UpdataModel.Should().BeSameAs(dto);
        }

        [Fact]
        public async Task Update_Failed()
        {
            //arrage
            var dto = new TypeDto()
            {
                Type = "test"
            };

            var entity = new TypeEntity()
            {
                Type = "test"
            };

            _mapper
                .Setup(s => 
                    s.Map<TypeEntity>(It.Is<TypeDto>(i => 
                        i.Equals(dto))))
                .Returns(entity);

            _catalogTypeRepository
                .Setup(s => 
                    s.Update(It.Is<TypeEntity>(i => 
                        i.Equals(entity))))
                .ThrowsAsync(new BusinessException("test error"));

            _mapper
                .Setup(s => 
                    s.Map<TypeDto>(It.Is<TypeEntity>(i => 
                        i.Equals(entity))))
                .Returns(dto);

            //act
            var responce = await _catalogTypeService.UpdateType(dto);

            //assert
            responce.Should().NotBeNull();
            responce.ErrorMessage.Should().NotBeNull();
            responce.RespCode.Should().NotBeNull();
            responce.UpdataModel.Should().BeNull();
            responce.RespCode.Should().Be(ResponceCode.Failed);
        }
    }
}
