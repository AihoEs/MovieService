using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;
using MoviesOnline.Controllers;
using MoviesOnline.Data;
using MoviesOnline.Interfaces;
using MoviesOnline.Models;
using MoviesOnline.Services.CookieServices;
using MoviesOnline.Services.UserServices;
using MoviesOnline.ValidationRules;
using System;
using System.Collections.Generic;
using System.Text;
using static MoviesOnline.Controllers.AuthController;



namespace Tests
{
    public class AuthRegTest
    {
        private DataBaseContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            return new DataBaseContext(options);
        }
        [Fact]

        public async Task RegistrationTest_Should_CreateUser()
        {
            var db = GetInMemoryDb();

            var fakeHash = new Mock<IHashPassword>();
            var fakeToken = new Mock<ITokenGenerator>();
            var fakeID = new Mock<IIDGenerate>();
            var FakeController = new AuthController(db, fakeID.Object, fakeHash.Object, fakeToken.Object);
            var userDto = new UserRegDTO
            {
                UserName = "John",
                Email = "john@gmail.com",
                PasswordHash = "password"
            };

            fakeHash
                .Setup(x => x.HashPassword(It.IsAny<string>()))
                .Returns("hashPassword");

            fakeToken
                .Setup(x => x.TokenGenerate(It.IsAny<User>()))
                .Returns("fakeJwt");

            fakeID
                .Setup(x => x.IdGenerate())
                .Returns(6464646);

            var result = await FakeController.Register(userDto);

            var userInDb = await db.Users.FirstOrDefaultAsync(x => x.Name == "John");
            Assert.NotNull(userInDb);
            Assert.Equal("hashPassword", userInDb.HashPassword);
            Assert.Equal("john@gmail.com", userInDb.Email);
            Assert.Equal(6464646, userInDb.Id);


        }
        [Fact]
        public async Task RegistrationTest_With_FluentValidationError()
        {

            var validator = new AuthRegValidation();
            var invalidUser = new UserRegDTO { UserName = "", PasswordHash = "123" };

            Assert.Throws<FluentValidation.ValidationException>(() =>
                validator.ValidateAndThrow(invalidUser));


        }

    }
        

    
}
