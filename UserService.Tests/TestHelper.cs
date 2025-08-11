using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Services;
using Microsoft.Extensions.Configuration;

namespace UserService.Tests
{
    public static class TestHelper
    {
        public static AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("UserServiceTestDb")
                .Options;

            return new AppDbContext(options);
        }

        public static JwtService GetJwtService()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"Jwt:Key", "vfsdlpgjdgfd,gl;fdjgdfogfmdyjgdfspgjdf[odasutidhgjsdfotijdf0g"},
                {"Jwt:Issuer", "UserService"},
                {"Jwt:Audience", "UserServiceAudience"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            return new JwtService(configuration);
        }
    }
}
