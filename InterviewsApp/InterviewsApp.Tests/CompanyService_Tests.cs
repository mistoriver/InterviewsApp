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
    public class CompanyService_Tests
    {
        Mock<IRepository<CompanyEntity>> repMock = new Mock<IRepository<CompanyEntity>>();
        Mock<IRepository<PositionEntity>> posMock = new Mock<IRepository<PositionEntity>>();
        Mock<IMapper> mapperMoq = new Mock<IMapper>();
        IFixture fixture = new Fixture().Customize(new RecursionBehaviourCustomization());

        [Fact]
        public async void CompanyTest_CreateCompanySuccess()
        {
            var company = fixture.Create<CreateCompanyDto>();
            repMock.Setup(repo => repo.Create(It.IsAny<CompanyEntity>())).ReturnsAsync(new Guid());
            mapperMoq.Setup(mapper => mapper.Map<CompanyEntity>(It.IsAny<CreateCompanyDto>())).Returns(new CompanyEntity());

            var sut = new CompanyService(repMock.Object, posMock.Object, mapperMoq.Object);
            var res = await sut.CreateCompany(company);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            repMock.Verify(userRep => userRep.Create(It.IsAny<CompanyEntity>()), Times.Once());
        }

        [Fact]
        public async void CompanyTest_GetCompanyUserRatingSuccess()
        {
            var companyId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            var positions = fixture.CreateMany<PositionEntity>(2);
            posMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);

            var sut = new CompanyService(repMock.Object, posMock.Object, mapperMoq.Object);
            var res = await sut.GetUserCompanyRate(companyId, userId);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
        }

        [Fact]
        public async void CompanyTest_GetCompanyUserRatingFailure()
        {
            var companyId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            var positions = fixture.CreateMany<PositionEntity>(0);
            posMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);

            var sut = new CompanyService(repMock.Object, posMock.Object, mapperMoq.Object);
            var res = await sut.GetUserCompanyRate(companyId, userId);

            Assert.False(res.Ok, $"Ожидалось, что запрос НЕ БУДЕТ успешен, но это не так.");
        }

        [Fact]
        public async void CompanyTest_RateCompanySuccessUp()
        {
            var companyId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            short newRate = 10;
            var positions = fixture.Build<PositionEntity>().With( pos => pos.CompanyRate, 50).CreateMany(1);
            var company = fixture.Build<CompanyEntity>().With(c => c.Rating, 50).With(c => c.Id, companyId).Create();

            repMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(company);
            posMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);

            var sut = new CompanyService(repMock.Object, posMock.Object, mapperMoq.Object);
            var res = await sut.RateCompany(companyId, userId, newRate);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            repMock.Verify(userRep => userRep.Update(It.IsAny<CompanyEntity>()), Times.Once());
            Assert.True(res.ResponseData < 50);
        }

        [Fact]
        public async void CompanyTest_RateCompanySuccessDown()
        {
            var companyId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            short newRate = 60;
            var positions = fixture.Build<PositionEntity>().With(pos => pos.CompanyRate, 50).CreateMany(1);
            var company = fixture.Build<CompanyEntity>().With(c => c.Rating, 50).With(c => c.Id, companyId).Create();

            repMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(company);
            posMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);

            var sut = new CompanyService(repMock.Object, posMock.Object, mapperMoq.Object);
            var res = await sut.RateCompany(companyId, userId, newRate);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            repMock.Verify(userRep => userRep.Update(It.IsAny<CompanyEntity>()), Times.Once());
            Assert.True(res.ResponseData > 50);
        }

        [Fact]
        public async void CompanyTest_RateCompanyMore100()
        {
            var companyId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            short newRate = 101;
            var positions = fixture.Build<PositionEntity>().With(pos => pos.CompanyRate, 50).CreateMany(1);
            var company = fixture.Build<CompanyEntity>().With(c => c.Rating, 50).With(c => c.Id, companyId).Create();

            repMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(company);
            posMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);

            var sut = new CompanyService(repMock.Object, posMock.Object, mapperMoq.Object);
            var res = await sut.RateCompany(companyId, userId, newRate);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            repMock.Verify(userRep => userRep.Update(It.IsAny<CompanyEntity>()), Times.Once());
            Assert.True(res.ResponseData == 50);
        }
        [Fact]
        public async void CompanyTest_RateCompanyLess0()
        {
            var companyId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            short newRate = -1;
            var positions = fixture.Build<PositionEntity>().With(pos => pos.CompanyRate, 50).CreateMany(1);
            var company = fixture.Build<CompanyEntity>().With(c => c.Rating, 50).With(c => c.Id, companyId).Create();

            repMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(company);
            posMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);

            var sut = new CompanyService(repMock.Object, posMock.Object, mapperMoq.Object);
            var res = await sut.RateCompany(companyId, userId, newRate);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            repMock.Verify(userRep => userRep.Update(It.IsAny<CompanyEntity>()), Times.Once());
            Assert.True(res.ResponseData == 50);
        }

        [Fact]
        public async void CompanyTest_RateCompanyFailureNotFound()
        {
            var companyId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            short newRate = 50;

            repMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync((CompanyEntity) null);

            var sut = new CompanyService(repMock.Object, posMock.Object, mapperMoq.Object);
            var res = await sut.RateCompany(companyId, userId, newRate);

            Assert.False(res.Ok, $"Ожидалось, что запрос НЕ БУДЕТ успешен, но это не так.");
            repMock.Verify(userRep => userRep.Update(It.IsAny<CompanyEntity>()), Times.Never());
        }

        [Fact]
        public async void CompanyTest_RateCompanyFailureNoPositionForUser()
        {
            var companyId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            short newRate = 50;
            var company = fixture.Build<CompanyEntity>().With(c => c.Rating, 50).With(c => c.Id, companyId).Create();

            repMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(company);

            var sut = new CompanyService(repMock.Object, posMock.Object, mapperMoq.Object);
            var res = await sut.RateCompany(companyId, userId, newRate);

            Assert.False(res.Ok, $"Ожидалось, что запрос НЕ БУДЕТ успешен, но это не так.");
            repMock.Verify(userRep => userRep.Update(It.IsAny<CompanyEntity>()), Times.Never());
        }
    }
}
