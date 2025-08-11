using Xunit;
using Microsoft.AspNetCore.Mvc;
using UserService.Controllers;
using UserService.Models;


using Xunit;

namespace UserService.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task Register_ShouldReturnOk_WhenUserIsValid()
        {
            var controller = new AuthController(TestHelper.GetDbContext(), TestHelper.GetJwtService());

            var newUser = new User
            {
                Name = "TestUser",
                Password = "TestPass"
            };

            var result = await controller.Register(newUser);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
