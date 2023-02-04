using AutoFixture;
using AutoMapper;
using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Services;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using InterviewsApp.Tests.Customizations;
using Moq;
using System.Linq.Expressions;

namespace InterviewsApp.Tests
{
    public class PositionService_Tests
    {
        Mock<IRepository<PositionEntity>> repMock = new Mock<IRepository<PositionEntity>>();
        Mock<IRepository<CompanyEntity>> companyMock = new Mock<IRepository<CompanyEntity>>();
        Mock<IRepository<UserEntity>> userMock = new Mock<IRepository<UserEntity>>();
        Mock<IMapper> mapperMoq = new Mock<IMapper>();
        IFixture fixture = new Fixture().Customize(new RecursionBehaviourCustomization());

        [Fact]
        public async void PositionTest_CreatePositionSuccess()
        {
            var position = fixture.Create<CreatePositionDto>();
            var user = fixture.Create<UserEntity>();
            var company = fixture.Create<CompanyEntity>();
            userMock.Setup(user => user.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(user);
            companyMock.Setup(company => company.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(company);
            mapperMoq.Setup(mapper => mapper.Map<PositionEntity>(It.IsAny<CreatePositionDto>())).Returns(new PositionEntity());

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.CreatePosition(position);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            repMock.Verify(rep => rep.Create(It.IsAny<PositionEntity>()), Times.Once);
        }
        [Fact]
        public async void PositionTest_CreatePositionFailureNoUser()
        {
            var position = fixture.Create<CreatePositionDto>();
            var company = fixture.Create<CompanyEntity>();
            userMock.Setup(user => user.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync((UserEntity) null);
            companyMock.Setup(company => company.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(company);
            mapperMoq.Setup(mapper => mapper.Map<PositionEntity>(It.IsAny<CreatePositionDto>())).Returns(new PositionEntity());

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.CreatePosition(position);

            Assert.False(res.Ok, $"Ожидалось, что запрос НЕ БУДЕТ успешен, но это не так.");
            Assert.Contains("Loc.Message.NoSuchUser", res.ErrorMessage);
            repMock.Verify(rep => rep.Create(It.IsAny<PositionEntity>()), Times.Never);
        }
        [Fact]
        public async void PositionTest_CreatePositionFailureNoCompany()
        {
            var position = fixture.Create<CreatePositionDto>();
            var user = fixture.Create<UserEntity>();
            userMock.Setup(user => user.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(user);
            companyMock.Setup(company => company.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync((CompanyEntity) null);
            mapperMoq.Setup(mapper => mapper.Map<PositionEntity>(It.IsAny<CreatePositionDto>())).Returns(new PositionEntity());

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.CreatePosition(position);

            Assert.False(res.Ok, $"Ожидалось, что запрос НЕ БУДЕТ успешен, но это не так.");
            Assert.Contains("Loc.Message.NoSuchCompany", res.ErrorMessage);
            repMock.Verify(rep => rep.Create(It.IsAny<PositionEntity>()), Times.Never);
        }
        [Fact]
        public async void PositionTest_UpdateMoneySuccess()
        {
            var position = fixture.Create<UpdatePositionDto>();
            var positionEntities = fixture.CreateMany<PositionEntity>(1);

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positionEntities);

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.UpdateMoney(position);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так.");
            repMock.Verify(rep => rep.Update(It.IsAny<PositionEntity>()), Times.Once);
        }
        [Fact]
        public async void PositionTest_UpdateMoneyFailureNoPosition()
        {
            var position = fixture.Create<UpdatePositionDto>();
            var positionEntities = fixture.CreateMany<PositionEntity>();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(new List<PositionEntity>());

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.UpdateMoney(position);

            Assert.False(res.Ok, $"Ожидалось, что запрос НЕ БУДЕТ успешен, но это не так.");
            repMock.Verify(rep => rep.Update(It.IsAny<PositionEntity>()), Times.Never);
        }
        [Fact]
        public async void PositionTest_UpdateSetDeniedSuccess()
        {
            var positionId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            var positionEntities = fixture.CreateMany<PositionEntity>(1);

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positionEntities);

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.UpdateSetDenied(positionId, userId);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так.");
            repMock.Verify(rep => rep.Update(It.IsAny<PositionEntity>()), Times.Once);
        }
        [Fact]
        public async void PositionTest_UpdateSetDeniedFailureNoPosition()
        {
            var positionId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(new List<PositionEntity>());

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.UpdateSetDenied(positionId, userId);

            Assert.False(res.Ok, $"Ожидалось, что запрос НЕ БУДЕТ успешен, но это не так.");
            repMock.Verify(rep => rep.Update(It.IsAny<PositionEntity>()), Times.Never);
        }
        [Fact]
        public async void PositionTest_UpdateSetOfferedSuccess()
        {
            var positionId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            var positionEntities = fixture.CreateMany<PositionEntity>(1);

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positionEntities);

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.UpdateSetOffered(positionId, userId);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так.");
            repMock.Verify(rep => rep.Update(It.IsAny<PositionEntity>()), Times.Once);
        }
        [Fact]
        public async void PositionTest_UpdateSetOfferedFailureNoPosition()
        {
            var positionId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(new List<PositionEntity>());

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.UpdateSetOffered(positionId, userId);

            Assert.False(res.Ok, $"Ожидалось, что запрос НЕ БУДЕТ успешен, но это не так.");
            repMock.Verify(rep => rep.Update(It.IsAny<PositionEntity>()), Times.Never);
        }
        [Fact]
        public async void PositionTest_UpdateCitySuccess()
        {
            var position = fixture.Create<UpdatePositionDto>();
            var positionEntities = fixture.CreateMany<PositionEntity>(1);

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positionEntities);

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.UpdateCity(position);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так.");
            repMock.Verify(rep => rep.Update(It.IsAny<PositionEntity>()), Times.Once);
        }
        [Fact]
        public async void PositionTest_UpdateCityFailureNoPosition()
        {
            var position = fixture.Create<UpdatePositionDto>();
            var positionEntities = fixture.CreateMany<PositionEntity>();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(new List<PositionEntity>());

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.UpdateCity(position);

            Assert.False(res.Ok, $"Ожидалось, что запрос НЕ БУДЕТ успешен, но это не так.");
            repMock.Verify(rep => rep.Update(It.IsAny<PositionEntity>()), Times.Never);
        }
        [Fact]
        public async void PositionTest_UpdateCommentSuccess()
        {
            var position = fixture.Create<UpdateCommentDto>();
            var positionEntities = fixture.CreateMany<PositionEntity>(1);

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positionEntities);

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.UpdateComment(position);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так.");
            repMock.Verify(rep => rep.Update(It.IsAny<PositionEntity>()), Times.Once);
        }
        [Fact]
        public async void PositionTest_UpdateCommentFailureNoPosition()
        {
            var position = fixture.Create<UpdateCommentDto>();
            var positionEntities = fixture.CreateMany<PositionEntity>();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(new List<PositionEntity>());

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.UpdateComment(position);

            Assert.False(res.Ok, $"Ожидалось, что запрос НЕ БУДЕТ успешен, но это не так.");
            repMock.Verify(rep => rep.Update(It.IsAny<PositionEntity>()), Times.Never);
        }
        [Fact]
        public async void PositionTest_DeleteSuccess()
        {
            var positionId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            var positionEntities = fixture.CreateMany<PositionEntity>(1);

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positionEntities);

            var sut = new PositionService(repMock.Object, userMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.Delete(positionId, userId);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так.");
            repMock.Verify(rep => rep.Delete(It.IsAny<PositionEntity>()), Times.Once);
        }
    }
}
