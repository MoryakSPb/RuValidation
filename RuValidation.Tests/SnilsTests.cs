namespace RuValidation.Tests;

public class SnilsTests
{
    [Fact]
    public void ValidWithDelimiters()
    {
        SnilsAttribute attribute = new(false);
        Assert.True(attribute.IsValid("123-456-789 00"));
    }

    [Fact]
    public void ValidHashWithDelimiters()
    {
        SnilsAttribute attribute = new();
        Assert.True(attribute.IsValid("123-456-789 64"));
    }

    [Fact]
    public void InvalidHashWithDelimiters()
    {
        SnilsAttribute attribute = new();
        Assert.False(attribute.IsValid("123-456-789 00"));
    }

    [Fact]
    public void InvalidDelimiterWithDelimiters()
    {
        SnilsAttribute attribute = new(false);
        Assert.False(attribute.IsValid("123_456-789 00"));
    }

    [Fact]
    public void InvalidNoDelimiterWithDelimiters()
    {
        SnilsAttribute attribute = new(false);
        Assert.False(attribute.IsValid("123-456789 00"));
    }


    [Fact]
    public void InvalidLen10WithDelimiters()
    {
        SnilsAttribute attribute = new(false);
        Assert.False(attribute.IsValid("123-45-789 00"));
    }

    [Fact]
    public void InvalidLen12WithDelimiters()
    {
        SnilsAttribute attribute = new(false);
        Assert.False(attribute.IsValid("123-4456-789 00"));
    }

    [Fact]
    public void ValidWithoutDelimiters()
    {
        SnilsAttribute attribute = new(false);
        Assert.True(attribute.IsValid("12345678900"));
    }

    [Fact]
    public void ValidLen10WithoutDelimiters()
    {
        SnilsAttribute attribute = new(false);
        Assert.False(attribute.IsValid("1234567890"));
    }

    [Fact]
    public void ValidInt64WithoutDelimiters()
    {
        SnilsAttribute attribute = new(false);
        Assert.True(attribute.IsValid(12345678900));
    }

    [Fact]
    public void ValidHashInt64WithoutDelimiters()
    {
        SnilsAttribute attribute = new();
        Assert.True(attribute.IsValid(12345678964));
    }

    [Fact]
    public void ValidInt64ZeroWithoutDelimiters()
    {
        SnilsAttribute attribute = new(false);
        Assert.True(attribute.IsValid(0));
    }

    [Fact]
    public void InvalidInt64MaxValue()
    {
        SnilsAttribute attribute = new(false);
        Assert.False(attribute.IsValid(long.MaxValue));
    }
#if NET8_0_OR_GREATER
    [Fact]
    public void InvalidUInt128MaxValue()
    {
        SnilsAttribute attribute = new(false);
        Assert.False(attribute.IsValid(UInt128.MaxValue));
    }
#endif

    [Fact]
    public void InvalidNegativeInt64WithoutDelimiters()
    {
        SnilsAttribute attribute = new(false);
        Assert.False(attribute.IsValid(-12345678900));
    }

    [Fact]
    public void ValidUInt64WithoutDelimiters()
    {
        SnilsAttribute attribute = new(false);
        Assert.True(attribute.IsValid(12345678900ul));
    }

    [Fact]
    public void ValidNull()
    {
        SnilsAttribute attribute = new(false);
        Assert.True(attribute.IsValid(null));
    }

    [Fact]
    public void InvalidObject()
    {
        SnilsAttribute attribute = new(false);
        Assert.False(attribute.IsValid(new()));
    }
}