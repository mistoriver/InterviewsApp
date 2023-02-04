using AutoFixture;
using AutoMapper;
using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Core.Services;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using InterviewsApp.Tests.Customizations;
using Moq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace InterviewsApp.Tests
{
    public class UserService_Tests
    {
        Mock<IRepository<UserEntity>> repMock = new Mock<IRepository<UserEntity>>();
        Mock<IPasswordService> passMoq = new Mock<IPasswordService>();
        Mock<IAuthService> authMoq = new Mock<IAuthService>();
        Mock<IMapper> mapperMoq = new Mock<IMapper>();
        IFixture fixture = new Fixture().Customize(new RecursionBehaviourCustomization());

        [Fact]
        public async Task UserTest_LoginTest_Success()
        {
            //Arrange
            var loginUserDto = fixture.Build<LoginUserDto>().With(user => user.Login, "mistoriver").Create();
            var users = fixture.Build<UserEntity>().With(user => user.Login, "mistoriver").CreateMany(3);

            repMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<UserEntity, bool>>>())).ReturnsAsync(users);
            passMoq.Setup(pass => pass.VerifyPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true).Verifiable();

            var sut = new UserService(repMock.Object,passMoq.Object,authMoq.Object,mapperMoq.Object);

            //Act
            var res = await sut.Login(loginUserDto);

            //Assert
            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            passMoq.Verify(passService => passService.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task UserTest_LoginTest_WrongLogin_RU()
        {
            //Arrange
            var loginUserDto = fixture.Build<LoginUserDto>().With(user => user.Login, "mistoriver").Create();

            var sut = new UserService(repMock.Object, passMoq.Object, authMoq.Object, mapperMoq.Object);

            //Act
            var res = await sut.Login(loginUserDto);

            //Assert
            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Contains("Loc.Message.WrongLogPass", res.ErrorMessage);
            passMoq.Verify(passService => passService.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }
        [Fact]
        public async Task UserTest_LoginTest_WrongPass_RU()
        {
            //Arrange

            var loginUserDto = fixture.Build<LoginUserDto>().With(user => user.Login, "mistoriver").Create();
            var users = fixture.Build<UserEntity>().With(user => user.Login, "mistoriver").CreateMany(1);

            repMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<UserEntity, bool>>>())).ReturnsAsync(users);
            passMoq.Setup(pass => pass.VerifyPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false).Verifiable();

            var sut = new UserService(repMock.Object, passMoq.Object, authMoq.Object, mapperMoq.Object);

            //Act
            var res = await sut.Login(loginUserDto);

            //Assert
            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Contains("Loc.Message.WrongLogPass", res.ErrorMessage);
            passMoq.Verify();
        }
        [Fact]
        public async Task UserTest_CreateUserTest_Success()
        {
            //Arrange
            var createUserDto = fixture.Build<CreateUserDto>().With(user => user.Login, "mistoriver").Create();

            mapperMoq.Setup(map => map.Map<UserEntity>(It.IsAny<CreateUserDto>())).Returns(new UserEntity());

            var sut = new UserService(repMock.Object, passMoq.Object, authMoq.Object, mapperMoq.Object);

            //Act
            var res = await sut.CreateUser(createUserDto);

            //Assert
            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            passMoq.Verify(passRep => passRep.HashPassword(It.IsAny<string>()), Times.Once());
            repMock.Verify(userRep => userRep.Create(It.IsAny<UserEntity>()), Times.Once());
        }
        [Fact]
        public async Task UserTest_CreateUserTest_NotUnique_RU()
        {
            //Arrange

            var createUserDto = fixture.Build<CreateUserDto>().With(user => user.Login, "mistoriver").Create();
            var users = fixture.Build<UserEntity>().With(user => user.Login, "mistoriver").CreateMany(1);

            mapperMoq.Setup(map => map.Map<UserEntity>(It.IsAny<CreateUserDto>())).Returns(new UserEntity());
            repMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<UserEntity, bool>>>())).ReturnsAsync(users);

            var sut = new UserService(repMock.Object, passMoq.Object, authMoq.Object, mapperMoq.Object);

            //Act
            var res = await sut.CreateUser(createUserDto);

            //Assert
            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Contains("Loc.Message.UserNotUnique", res.ErrorMessage);
            passMoq.Verify(passService => passService.HashPassword(It.IsAny<string>()), Times.Never());
            repMock.Verify(userRep => userRep.Create(It.IsAny<UserEntity>()), Times.Never());
        }
    }
}