namespace RuValidation.Tests;

public class KppTests
{
    private const int _INT_VALUE = 123456789;
    private const string _STRING_VALUE = "123456789";
    private readonly KppAttribute _attribute = new();

    [Theory]
    [InlineData(_INT_VALUE, _INT_VALUE * 10 + 1, _INT_VALUE / 10, -_INT_VALUE / 10)]
    [InlineData(_STRING_VALUE, _STRING_VALUE + "0", "12345678", " 12345-6789")]
    public void Validate(in object valid, in object lenPlus, in object lenMinus, in object invalidCharacters)
    {
        Assert.True(_attribute.IsValid(valid));
        Assert.False(_attribute.IsValid(lenPlus));
        if (lenMinus is string)
            Assert.False(_attribute.IsValid(lenMinus));
        else
            Assert.True(_attribute.IsValid(lenMinus));
        Assert.False(_attribute.IsValid(invalidCharacters));
    }

    [Fact]
    public void Null() => Assert.True(_attribute.IsValid(null));

    [Fact]
    public void Obj() => Assert.False(_attribute.IsValid(new()));
}