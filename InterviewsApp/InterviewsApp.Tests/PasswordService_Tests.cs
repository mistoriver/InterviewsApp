using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using InterviewsApp.Core.Services;
using InterviewsApp.Tests.Customizations;

namespace InterviewsApp.Tests
{
    public class PasswordService_Tests
    {
        IFixture fixture = new Fixture().Customize(new RecursionBehaviourCustomization());
        [Fact]
        public void PasswordTest_HashPassword()
        {
            var testPassword = fixture.Create<string>();
            var sut = new PasswordService();

            var hashedPass = sut.HashPassword(testPassword);

            Assert.NotEqual(testPassword, hashedPass);
        }

        [Fact]
        public void PasswordTest_Verify_Success()
        {
            var testPassword = fixture.Create<string>();
            var sut = new PasswordService();

            var hashedPass = sut.HashPassword(testPassword);

            var verifiedResult = sut.VerifyPassword(testPassword, hashedPass);

            Assert.True(verifiedResult, "Захешированный пароль не соответствует ожидаемому - не прошёл встроенную верификацию");
        }

        [Fact]
        public void PasswordTest_Verify_FailureWrongPass()
        {
            var testPassword = fixture.Create<string>();
            var wrongPassword = fixture.Create<string>();
            var sut = new PasswordService();

            var hashedWrongPass = sut.HashPassword(wrongPassword);
            var verifiedResult = sut.VerifyPassword(testPassword, hashedWrongPass);

            Assert.False(verifiedResult, "Был верифицирован неверный пароль - такого быть не должно");
        }
        [Fact]
        public void PasswordTest_Verify_FailureNonHashedPass()
        {
            var testPassword = fixture.Create<string>();
            var sut = new PasswordService();

            var verifiedResult = sut.VerifyPassword(testPassword, testPassword);

            Assert.False(verifiedResult, "Был верифицирован незахешированный пароль - такого быть не должно");
        }
    }
}
