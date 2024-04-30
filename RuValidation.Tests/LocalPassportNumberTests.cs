namespace RuValidation.Tests;

public class LocalPassportNumberTests
{
    [Fact]
    public void ValidStr10()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.True(attribute.IsValid("1234567890"));
    }

    [Fact]
    public void ValidStr11()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.True(attribute.IsValid("1234 567890"));
    }

    [Fact]
    public void ValidStr12()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.True(attribute.IsValid("12 34 567890"));
    }

    [Fact]
    public void ValidInt64()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.True(attribute.IsValid(1234567890L));
    }

    [Fact]
    public void ValidUInt64()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.True(attribute.IsValid(1234567890ul));
    }

    [Fact]
    public void InvalidStr10()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.False(attribute.IsValid("l234567890"));
    }

    [Fact]
    public void InvalidStr11()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.False(attribute.IsValid("123 4567890"));
    }

    [Fact]
    public void InvalidStr12()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.False(attribute.IsValid("1234  567890"));
    }

    [Fact]
    public void InvalidStr13()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.False(attribute.IsValid(" 12 34 5678901"));
    }

    [Fact]
    public void InvalidInt64()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.False(attribute.IsValid(-1));
    }

    [Fact]
    public void InvalidUInt64()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.False(attribute.IsValid(ulong.MaxValue));
    }

    [Fact]
    public void InvalidObject()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.False(attribute.IsValid(new()));
    }

    [Fact]
    public void ValidNull()
    {
        LocalPassportNumberAttribute attribute = new();
        Assert.True(attribute.IsValid(null));
    }
}