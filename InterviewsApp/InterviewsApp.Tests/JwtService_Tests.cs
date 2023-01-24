using AutoFixture;
using InterviewsApp.Core.Models;
using InterviewsApp.Core.Services;
using InterviewsApp.Tests.Customizations;
using Microsoft.Extensions.Options;
using Moq;

namespace InterviewsApp.Tests
{
    public class JwtService_Tests
    {
        Mock<IOptions<AuthSettings>> authOptionsMoq = new Mock<IOptions<AuthSettings>>();
        IFixture fixture = new Fixture().Customize(new RecursionBehaviourCustomization());

        [Fact]
        public void JwtTest_GenerateSuccess()
        {
            var login = fixture.Create<string>();
            var authSettings = fixture.Create<AuthSettings>();
            authOptionsMoq.Setup(moq => moq.Value).Returns(authSettings);
            var sut = new JwtService(authOptionsMoq.Object);
            var resToken = sut.Generate(login);

            Assert.True(login != resToken, "Ожидалось, что JWT-токен, построенный по логину, будет отличаться от самого логина, но это не так");
        }
    }
}
