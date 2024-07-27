using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using MovieStore.Data.Abstract;
using MovieStore.Entity.Dto;
using MovieStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieStore.Tests
{
    public class Customer
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;
        public Customer()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();

        }

        [Fact]
        public async Task Buy_Movie_And_After_Delete_Movie_Get_Movie_From_Temporal_Table_That_Bought_Movies()
        {
            // Arrange
            var createCastRequest = new CreateCustomerDto { Email = "cnr24clp@gmail.com" };
            var identityResult = IdentityResult.Success;

            var baseUser = new BaseUser { Email = createCastRequest.Email };

            _mapper.Setup(m => m.Map<BaseUser>(It.IsAny<CreateCustomerDto>())).Returns(baseUser);
            _unitOfWork.Setup(m => m.CustomerDal.AddCustomer(It.IsAny<BaseUser>(), "12456-Admin")).Returns(Task.FromResult(identityResult));

            // Act
            var result = _unitOfWork.Object.CustomerDal.AddCustomer(baseUser, "12456-Admin").Result;
            var cast = await _unitOfWork.Object.CustomerDal.AddAsync(new Entity.Customer { BaseUser = baseUser });

            // Assert
            Assert.Equal(identityResult, result);

        }

    }
}
