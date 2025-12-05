using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MoviesOnline.Controllers;
using MoviesOnline.Data;
using MoviesOnline.Interfaces;
using MoviesOnline.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;
using static MoviesOnline.Controllers.AuthController;

namespace Tests
{
    public class AuthLogTest
    {
        private DataBaseContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var context = new DataBaseContext(options);
            context.Users.Add(new User
            {
                Id = 1,
                Name = "John",
                Email = "john@google.com",
                HashPassword = "hashedpass"
            });
            context.SaveChanges();

            return context;
        }
        [Fact]
        public async Task RegistrationTest_Should_GiveJWT_WhenCredintialsIsValid()
        {
            var db = GetInMemoryDb();

            var fakeVerifyHash = new Mock<IHashPassword>();
            var mockToken = new Mock<ITokenGenerator>();

            var testUser = new User { Name = "John", HashPassword = "password", Email = "john@mail.com" };
            db.Users.Add(testUser);
            await db.SaveChangesAsync();

            fakeVerifyHash
                .Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            mockToken
                .Setup(x => x.TokenGenerate(It.IsAny<User>()))
                .Returns("jwt-token");

            var controller = new AuthController(db, null, fakeVerifyHash.Object,mockToken.Object);

            var loginDto = new UserLogDTO
            {
                UserName = "John",
                PasswordHash = "password"
            };
            var jwt = await controller.Login(loginDto);

            Assert.NotNull(jwt);
            var okResult = Assert.IsType<OkObjectResult>(jwt);
            var token = Assert.IsType<string>(okResult.Value);
            Assert.Equal("jwt-token", token);




        }
        [Fact]
        public async Task RegistrationTest_Should_GiveJWT_WhenCredintialsIsInvalid()
        {
            var db = GetInMemoryDb();

            var fakeVerifyHash = new Mock<IHashPassword>();
            var mockToken = new Mock<ITokenGenerator>();

            fakeVerifyHash
                .Setup(x => x.VerifyPassword("password", "hashedPassword"))
                .Returns(true);

            mockToken
                .Setup(x => x.TokenGenerate(It.IsAny<User>()))
                .Returns("jwt-token");

            var controller = new AuthController(db, null, fakeVerifyHash.Object, mockToken.Object);

            var loginDto = new UserLogDTO
            {
                UserName = "UnknownUser",
                PasswordHash = "password"
            };

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => controller.Login(loginDto));




        }
    }
}
