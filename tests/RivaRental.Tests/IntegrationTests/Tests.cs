using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace RivaRental.Tests.IntegrationTests;

public class Tests
{
    [Fact]
    public async Task A()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var b = 123;
            });
            builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                var cfg = configurationBuilder.Build();
                var value = cfg.GetValue<int>("Test123");
                var a = 1;
            });
        });

        var client = application.CreateClient();

        var response = await client.PostAsync("/rest/api/rent", new StringContent("""
                                                                                  {
                                                                                  "BoatType": "IseoSuper",
                                                                                  "StartTime": "2023-10-01T12:00:00Z",
                                                                                  "StartWalkingHours": 10
                                                                                  }
                                                                                  """, MediaTypeHeaderValue.Parse("application/json")));

        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    }
}