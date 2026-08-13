using MediaBoomService.Domain.Users.ValueObjects;

namespace MediaBoomService.Domain.UnitTests;

public class UsernameTests
{
    [Theory]
    [InlineData("abc")]
    [InlineData("John")]
    [InlineData("john.doe")]
    [InlineData("john-doe")]
    [InlineData("john_doe")]
    [InlineData("a.b-c_d")]
    public void Create_WhenUsernameIsValid_ShouldSucceed(string value)
    {
        var result = Username.Create(value);

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Value.Value);
    }

    [Fact]
    public void Create_WhenUsernameHasMinimumAllowedLength_ShouldSucceed()
    {
        var result = Username.Create(new string('a', 3));

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Create_WhenUsernameHasMaximumAllowedLength_ShouldSucceed()
    {
        var result = Username.Create(new string('a', 32));

        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("ab")]
    [InlineData(".john")]
    [InlineData("-john")]
    [InlineData("_john")]
    [InlineData("john.")]
    [InlineData("john-")]
    [InlineData("john_")]
    [InlineData("john..doe")]
    [InlineData("john--doe")]
    [InlineData("john__doe")]
    [InlineData("john.-doe")]
    [InlineData("john doe")]
    [InlineData("john@doe")]
    [InlineData("john123")]
    [InlineData("иван")]
    public void Create_WhenUsernameHasInvalidFormat_ShouldFail(string value)
    {
        var result = Username.Create(value);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Create_WhenUsernameIsLongerThanMaximumAllowedLength_ShouldFail()
    {
        var result = Username.Create(new string('a', 33));

        Assert.True(result.IsFailure);
    }
}
