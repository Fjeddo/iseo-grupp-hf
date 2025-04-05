
namespace RivaRental.Tests.Domain.Boats;

public class IseoSuperTests
{
    [Fact]
    public void This_should_fail()
    {
        var sut = 10;

        Assert.Equal(11, sut);
    }

    [Fact]
    public void This_should_go_green()
    {
        var sut = 10;

        Assert.NotEqual(11, sut);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void This_should_all_go_green(int val)
    {
        var sut = 10;

        Assert.True(sut > val);
    }
}