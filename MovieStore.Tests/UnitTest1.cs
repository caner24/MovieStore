using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using MovieStore.Core.Data;
using MovieStore.Data.Abstract;
using MovieStore.Entity;
using MovieStore.Entity.Dto;
using System.Linq.Expressions;

namespace MovieStore.Tests
{
    public class UnitTest1
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;
        public UnitTest1()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();

        }
        [Fact]
        public async Task Create_Cast()
        {
            // Arrange
            var createCastRequest = new CreateCastDto { Email = "cnr24clp@gmail.com" };
            var identityResult = IdentityResult.Success;

            var baseUser = new BaseUser { Email = createCastRequest.Email }; 

            _mapper.Setup(m => m.Map<BaseUser>(It.IsAny<CreateCastDto>())).Returns(baseUser);
            _unitOfWork.Setup(m => m.CastDal.AddCast(It.IsAny<BaseUser>(), "12456-Admin")).Returns(Task.FromResult(identityResult));

            // Act
            var result = _unitOfWork.Object.CastDal.AddCast(baseUser, "12456-Admin").Result;
            var cast = await _unitOfWork.Object.CastDal.AddAsync(new Cast { BaseUser = baseUser });

            // Assert
            Assert.Equal(identityResult, result);

        }

        [Fact]
        public async Task Get_Cast_By_Email()
        {

        }

        [Fact]
        public async Task Get_All_Cast()
        {

        }
        [Fact]
        public void Delete_Cast()
        {

        }

        [Fact]
        public async Task Add_Movie_By_CastEmail()
        {

        }
    }
}