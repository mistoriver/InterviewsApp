using AutoFixture;
using AutoMapper;
using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Services;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using InterviewsApp.Tests.Customizations;
using Moq;
using System.Linq.Expressions;

namespace InterviewsApp.Tests
{

    public class InterviewService_Tests
    {
        Mock<IRepository<InterviewEntity>> repMock = new Mock<IRepository<InterviewEntity>>();
        Mock<IRepository<PositionEntity>> positionMock = new Mock<IRepository<PositionEntity>>();
        Mock<IRepository<CompanyEntity>> companyMock = new Mock<IRepository<CompanyEntity>>();
        Mock<IMapper> mapperMoq = new Mock<IMapper>();
        IFixture fixture = new Fixture().Customize(new RecursionBehaviourCustomization());


        [Fact]
        public async void InterviewTest_GetFailureNoArgument()
        {
            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.Get();
            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Contains("необходима информация о пользователе", res.ErrorMessage);
        }
        [Fact]
        public async void InterviewTest_GetFailureOnlyId()
        {
            var interviewId = fixture.Create<Guid>();
            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.Get(interviewId);
            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Contains("необходима информация о пользователе", res.ErrorMessage);
        }
        [Fact]
        public async void InterviewTest_GetFailureNotFound()
        {
            var interviewId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<InterviewEntity, bool>>>())).ReturnsAsync(new List<InterviewEntity>());

            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.Get(interviewId, userId);
            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Contains("Loc.Message.NoSuchInterview", res.ErrorMessage);
        }
        [Fact]
        public async void InterviewTest_GetSuccess()
        {
            var interviewId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            var interviews = fixture.Build<InterviewEntity>().With(i => i.Id, interviewId).CreateMany(3);
            var positions = fixture.CreateMany<PositionEntity>(3);
            var company = fixture.Create<CompanyEntity>();
            var intDto = fixture.Build<InterviewDto>().With(i => i.Id, interviewId).Create();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<InterviewEntity, bool>>>())).ReturnsAsync(interviews);
            positionMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);
            positionMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(positions.First());
            mapperMoq.Setup(map => map.Map<InterviewDto>(It.IsAny<InterviewEntity>())).Returns(intDto);
            companyMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(company);
            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.Get(interviewId, userId);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
        }
        [Fact]
        public async void InterviewTest_GetByUserIdSuccess_OnlyFuture()
        {
            var interviewId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            var interviews = fixture.Build<InterviewEntity>().With(i => i.Id, interviewId).CreateMany(3);
            var positions = fixture.CreateMany<PositionEntity>(3);
            var company = fixture.Create<CompanyEntity>();
            var intDto = fixture.Build<InterviewDto>().With(i => i.Id, interviewId).Create();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<InterviewEntity, bool>>>())).ReturnsAsync(interviews);
            positionMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);
            positionMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(positions.First());
            mapperMoq.Setup(map => map.Map<InterviewDto>(It.IsAny<InterviewEntity>())).Returns(intDto);
            companyMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(company);
            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.GetByUserId(userId, true);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
        }
        [Fact]
        public async void InterviewTest_GetByUserIdSuccess()
        {
            var interviewId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            var interviews = fixture.Build<InterviewEntity>().With(i => i.Id, interviewId).CreateMany(3);
            var positions = fixture.CreateMany<PositionEntity>(3);
            var company = fixture.Create<CompanyEntity>();
            var intDto = fixture.Build<InterviewDto>().With(i => i.Id, interviewId).Create();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<InterviewEntity, bool>>>())).ReturnsAsync(interviews);
            positionMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);
            positionMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(positions.First());
            mapperMoq.Setup(map => map.Map<InterviewDto>(It.IsAny<InterviewEntity>())).Returns(intDto);
            companyMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(company);
            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.GetByUserId(userId);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
        }
        [Fact]
        public async void InterviewTest_GetByPositionIdSuccess()
        {
            var interviewId = fixture.Create<Guid>();
            var userId = fixture.Create<Guid>();
            var positionId = fixture.Create<Guid>();
            var interviews = fixture.Build<InterviewEntity>().With(i => i.PositionId, positionId).CreateMany(3);
            var positions = fixture.Build<PositionEntity>().With(p => p.Id, positionId).CreateMany(3);
            var company = fixture.Create<CompanyEntity>();
            var intDto = fixture.Build<InterviewDto>().With(i => i.Id, interviewId).With(i => i.PositionId, positionId).Create();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<InterviewEntity, bool>>>())).ReturnsAsync(interviews);
            positionMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);
            positionMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(positions.First());
            mapperMoq.Setup(map => map.Map<InterviewDto>(It.IsAny<InterviewEntity>())).Returns(intDto);
            companyMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(company);
            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.GetByPosition(positionId, userId);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
        }
        [Fact]
        public async void InterviewTest_GetByPositionIdFailureNotFound()
        {
            var userId = fixture.Create<Guid>();
            var positionId = fixture.Create<Guid>();
            var interviews = fixture.CreateMany<InterviewEntity>(0);
            var positions = fixture.CreateMany<PositionEntity>(0);

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<InterviewEntity, bool>>>())).ReturnsAsync(interviews);
            positionMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);
            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);

            var res = await sut.GetByPosition(positionId, userId);

            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Contains("Loc.Message.NoSuchInterview", res.ErrorMessage);
            repMock.Verify(rep => rep.Create(It.IsAny<InterviewEntity>()), Times.Never);
        }
        [Fact]
        public async void InterviewTest_CreateInterviewSuccess()
        {
            var intDto = fixture.Create<CreateInterviewDto>();
            var positions = fixture.CreateMany<PositionEntity>(3);
            var intEntity = fixture.Create<InterviewEntity>();

            mapperMoq.Setup(map => map.Map<InterviewEntity>(It.IsAny<CreateInterviewDto>())).Returns(intEntity);

            positionMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(positions.First());

            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);
            var res = await sut.CreateInterview(intDto);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            repMock.Verify(rep => rep.Create(It.IsAny<InterviewEntity>()), Times.Once);
        }
        [Fact]
        public async void InterviewTest_CreateInterviewFailureNoPosition()
        {
            var intDto = fixture.Create<CreateInterviewDto>();

            positionMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync((PositionEntity) null);

            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);
            var res = await sut.CreateInterview(intDto);

            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Contains("Loc.Message.NoSuchPosition", res.ErrorMessage);
            repMock.Verify(rep => rep.Update(It.IsAny<InterviewEntity>()), Times.Never);
        }
        [Fact]
        public async void InterviewTest_UpdateCommentSuccess()
        {
            var userId = fixture.Create<Guid>();
            var interviewId = fixture.Create<Guid>();
            var commentDto = fixture.Build<UpdateCommentDto>().With(u => u.UserId, userId).With(u => u.Id, interviewId).Create();
            var interviews = fixture.CreateMany<InterviewEntity>(3);
            var positions = fixture.CreateMany<PositionEntity>(3);

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<InterviewEntity, bool>>>())).ReturnsAsync(interviews);
            positionMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);

            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);
            var res = await sut.UpdateComment(commentDto);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            repMock.Verify(rep => rep.Update(It.IsAny<InterviewEntity>()), Times.Once);
        }
        [Fact]
        public async void InterviewTest_UpdateCommentFailureNoInterview()
        {
            var commentDto = fixture.Create<UpdateCommentDto>();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<InterviewEntity, bool>>>())).ReturnsAsync(new List<InterviewEntity>());

            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);
            var res = await sut.UpdateComment(commentDto);

            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Contains("Loc.Message.NoSuchInterviewForEdit", res.ErrorMessage);
            repMock.Verify(rep => rep.Update(It.IsAny<InterviewEntity>()), Times.Never);
        }
        [Fact]
        public async void InterviewTest_UpdateDatetimeSuccess()
        {

            var userId = fixture.Create<Guid>();
            var interviewId = fixture.Create<Guid>();
            var updateDto = fixture.Build<UpdateInterviewDto>().With(u => u.UserId, userId).With(u => u.Id, interviewId).Create();
            var interviews = fixture.CreateMany<InterviewEntity>(3);
            var positions = fixture.CreateMany<PositionEntity>(3);

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<InterviewEntity, bool>>>())).ReturnsAsync(interviews);
            positionMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<PositionEntity, bool>>>())).ReturnsAsync(positions);

            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);
            var res = await sut.UpdateDatetime(updateDto);

            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            repMock.Verify(rep => rep.Update(It.IsAny<InterviewEntity>()), Times.Once);
        }
        [Fact]
        public async void InterviewTest_UpdateDatetimeFailureNoInterview()
        {
            var updateDto = fixture.Create<UpdateInterviewDto>();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<InterviewEntity, bool>>>())).ReturnsAsync(new List<InterviewEntity>());

            var sut = new InterviewService(repMock.Object, positionMock.Object, companyMock.Object, mapperMoq.Object);
            var res = await sut.UpdateDatetime(updateDto);

            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Contains("Loc.Message.NoSuchInterviewForEdit", res.ErrorMessage);
            repMock.Verify(rep => rep.Update(It.IsAny<InterviewEntity>()), Times.Never);
        }
    }
}