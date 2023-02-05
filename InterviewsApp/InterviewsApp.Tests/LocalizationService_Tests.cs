using AutoFixture;
using AutoMapper;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Services;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using InterviewsApp.Tests.Customizations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Tests
{
    public class LocalizationService_Tests
    {
        Mock<IRepository<LocalizationEntity>> repMock = new Mock<IRepository<LocalizationEntity>>();
        Mock<IRepository<UserEntity>> userMock = new Mock<IRepository<UserEntity>>();
        Mock<IMapper> mapperMoq = new Mock<IMapper>();
        IFixture fixture = new Fixture().Customize(new RecursionBehaviourCustomization());

        [Fact]
        public async void GetByLanguage_Test()
        {
            var lang = "RU";
            var locals = fixture.Build<LocalizationEntity>().With(l => l.Language, lang).CreateMany(3);
            var localDto = fixture.Build<LocalizationDto>().With(ld => ld.Language, lang).Create();
            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<LocalizationEntity, bool>>>())).ReturnsAsync(locals);
            mapperMoq.Setup(mapper => mapper.Map<LocalizationDto>(It.IsAny<LocalizationEntity>())).Returns(localDto);

            var sut = new LocalizationService(repMock.Object, userMock.Object, mapperMoq.Object);

            var res = await sut.GetByLanguage(lang);


            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            Assert.True(res.ResponseData.Count() > 0, "Запрос был успешен, но не вернул результатов");
            repMock.Verify(rep => rep.Get(It.IsAny<Expression<Func<LocalizationEntity, bool>>>()), Times.Once);
        }

        [Fact]
        public async void GetByUser_TestFailureNoUser()
        {
            var lang = "RU";
            var locals = fixture.Build<LocalizationEntity>().With(l => l.Language, lang).CreateMany(3);
            var localDto = fixture.Build<LocalizationDto>().With(ld => ld.Language, lang).Create();
            var userId = fixture.Create<Guid>();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<LocalizationEntity, bool>>>())).ReturnsAsync(locals);
            userMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync((UserEntity)null);
            mapperMoq.Setup(mapper => mapper.Map<LocalizationDto>(It.IsAny<LocalizationEntity>())).Returns(localDto);

            var sut = new LocalizationService(repMock.Object, userMock.Object, mapperMoq.Object);

            var res = await sut.GetByUserId(userId);


            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Equal("Loc.Message.NoSuchUser", res.ErrorMessage);
            repMock.Verify(rep => rep.Get(It.IsAny<Expression<Func<LocalizationEntity, bool>>>()), Times.Never);
            userMock.Verify(rep => rep.GetByIdOrDefault(It.IsAny<Guid>()), Times.Once);
        }
        [Fact]
        public async void GetByUser_TestSuccess()
        {
            var lang = "RU";
            var locals = fixture.Build<LocalizationEntity>().With(l => l.Language, lang).CreateMany(3);
            var localDto = fixture.Build<LocalizationDto>().With(ld => ld.Language, lang).Create();
            var userId = fixture.Create<Guid>();
            var user = fixture.Create<UserEntity>();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<LocalizationEntity, bool>>>())).ReturnsAsync(locals);
            userMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(user);
            mapperMoq.Setup(mapper => mapper.Map<LocalizationDto>(It.IsAny<LocalizationEntity>())).Returns(localDto);

            var sut = new LocalizationService(repMock.Object, userMock.Object, mapperMoq.Object);

            var res = await sut.GetByUserId(userId);


            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            Assert.True(res.ResponseData.Count() > 0, "Запрос был успешен, но не вернул результатов");
            repMock.Verify(rep => rep.Get(It.IsAny<Expression<Func<LocalizationEntity, bool>>>()), Times.Once);
            userMock.Verify(rep => rep.GetByIdOrDefault(It.IsAny<Guid>()), Times.Once);
        }
        [Fact]
        public async void SetLocalizationForUser_TestFailureNoUser()
        {
            var lang = "RU";
            var locals = fixture.Build<LocalizationEntity>().With(l => l.Language, lang).CreateMany(3);
            var localDto = fixture.Build<LocalizationDto>().With(ld => ld.Language, lang).Create();
            var userId = fixture.Create<Guid>();
            var user = fixture.Create<UserEntity>();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<LocalizationEntity, bool>>>())).ReturnsAsync(locals);
            userMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync((UserEntity) null);
            mapperMoq.Setup(mapper => mapper.Map<LocalizationDto>(It.IsAny<LocalizationEntity>())).Returns(localDto);

            var sut = new LocalizationService(repMock.Object, userMock.Object, mapperMoq.Object);

            var res = await sut.SetLocalizationForUser(userId, lang);


            Assert.False(res.Ok, "Ожидалось что запрос НЕ БУДЕТ успешен, но он почему-то выполнился");
            Assert.Equal("Loc.Message.NoSuchUser", res.ErrorMessage);
            userMock.Verify(rep => rep.GetByIdOrDefault(It.IsAny<Guid>()), Times.Once);
            userMock.Verify(rep => rep.Update(It.IsAny<UserEntity>()), Times.Never);
        }

        [Fact]
        public async void SetLocalizationForUser_TestSuccess()
        {
            var lang = "RU";
            var locals = fixture.Build<LocalizationEntity>().With(l => l.Language, lang).CreateMany(3);
            var localDto = fixture.Build<LocalizationDto>().With(ld => ld.Language, lang).Create();
            var userId = fixture.Create<Guid>();
            var user = fixture.Create<UserEntity>();

            repMock.Setup(rep => rep.Get(It.IsAny<Expression<Func<LocalizationEntity, bool>>>())).ReturnsAsync(locals);
            userMock.Setup(rep => rep.GetByIdOrDefault(It.IsAny<Guid>())).ReturnsAsync(user);
            mapperMoq.Setup(mapper => mapper.Map<LocalizationDto>(It.IsAny<LocalizationEntity>())).Returns(localDto);

            var sut = new LocalizationService(repMock.Object, userMock.Object, mapperMoq.Object);

            var res = await sut.SetLocalizationForUser(userId, lang);


            Assert.True(res.Ok, $"Ожидалось, что запрос будет успешен, но это не так. Ошибка: {res.ErrorMessage}");
            userMock.Verify(rep => rep.GetByIdOrDefault(It.IsAny<Guid>()), Times.Once);
            userMock.Verify(rep => rep.Update(It.IsAny<UserEntity>()), Times.Once);
        }
    }
}
