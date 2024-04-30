namespace RuValidation.Tests;

public class InnTests
{
    public const string INDIVIDUAL_INN_VALID_STR = "505021556592";
    public const ulong INDIVIDUAL_INN_VALID_UINT64 = 505021556592UL;
    public const string INDIVIDUAL_INN_WRONG_HASH_STR = "505021556593";
    public const ulong INDIVIDUAL_INN_WRONG_HASH_UINT64 = 505021556593UL;

    public const string LEGAL_INN_VALID_STR = "7707329152";
    public const ulong LEGAL_INN_VALID_UINT64 = 7707329152UL;
    public const string LEGAL_INN_WRONG_HASH_STR = "7707329153";
    public const ulong LEGAL_INN_WRONG_HASH_UINT64 = 7707329153UL;

    public const string LEGAL_FOREIGN_INN_VALID_STR = "9909524142";
    public const ulong LEGAL_FOREIGN_INN_VALID_UINT64 = 9909524142UL;

    public static readonly InnIndividualAttribute AttributeIndividual = new();
    public static readonly InnIndividualAttribute AttributeIndividualWithoutHash = new(false);

    public static readonly InnLegalAttribute AttributeLegal = new();
    public static readonly InnLegalAttribute AttributeLegalWithoutHash = new(false);

    public static readonly InnAttribute Attribute = new();
    public static readonly InnAttribute AttributeWithoutHash = new(false);

    public static readonly InnLegalForeignAttribute ForeignAttribute = new();
    public static readonly InnLegalLocalAttribute LocalAttribute = new();

    [Theory]
    [InlineData(INDIVIDUAL_INN_VALID_STR)]
    [InlineData(INDIVIDUAL_INN_VALID_UINT64)]
    public void ValidIndividual(in object value)
    {
        Assert.True(AttributeIndividual.IsValid(value));
        Assert.True(AttributeIndividualWithoutHash.IsValid(value));

        Assert.True(Attribute.IsValid(value));
        Assert.True(AttributeWithoutHash.IsValid(value));
    }

    [Theory]
    [InlineData(LEGAL_INN_VALID_STR)]
    [InlineData(LEGAL_INN_VALID_UINT64)]
    public void ValidLegal(in object value)
    {
        Assert.True(AttributeLegal.IsValid(value));
        Assert.True(AttributeLegalWithoutHash.IsValid(value));

        Assert.True(Attribute.IsValid(value));
        Assert.True(AttributeWithoutHash.IsValid(value));
    }

    [Theory]
    [InlineData(LEGAL_INN_WRONG_HASH_STR)]
    [InlineData(LEGAL_INN_WRONG_HASH_UINT64)]
    public void WrongHashLegal(in object value)
    {
        Assert.False(AttributeLegal.IsValid(value));
        Assert.True(AttributeLegalWithoutHash.IsValid(value));

        Assert.False(Attribute.IsValid(value));
        Assert.True(AttributeWithoutHash.IsValid(value));
    }

    [Theory]
    [InlineData(INDIVIDUAL_INN_WRONG_HASH_STR)]
    [InlineData(INDIVIDUAL_INN_WRONG_HASH_UINT64)]
    public void WrongHashIndividual(in object value)
    {
        Assert.False(AttributeIndividual.IsValid(value));
        Assert.True(AttributeIndividualWithoutHash.IsValid(value));

        Assert.False(Attribute.IsValid(value));
        Assert.True(AttributeWithoutHash.IsValid(value));
    }

    [Fact]
    public void InvalidCharacterIndividual()
    {
        string value = INDIVIDUAL_INN_VALID_STR.Replace('0', 'o');

        Assert.False(AttributeIndividual.IsValid(value));
        Assert.False(AttributeIndividualWithoutHash.IsValid(value));

        Assert.False(Attribute.IsValid(value));
        Assert.False(AttributeWithoutHash.IsValid(value));
    }

    [Fact]
    public void InvalidCharacterLegal()
    {
        string value = LEGAL_INN_VALID_STR.Replace('0', 'o');

        Assert.False(AttributeLegal.IsValid(value));
        Assert.False(AttributeLegalWithoutHash.IsValid(value));

        Assert.False(Attribute.IsValid(value));
        Assert.False(AttributeWithoutHash.IsValid(value));
    }

    [Theory]
    [InlineData(LEGAL_FOREIGN_INN_VALID_STR)]
    [InlineData(LEGAL_FOREIGN_INN_VALID_UINT64)]
    public void Foreign(in object value)
    {
        Assert.True(ForeignAttribute.IsValid(value));
        Assert.False(LocalAttribute.IsValid(value));
    }

    [Theory]
    [InlineData(LEGAL_INN_VALID_STR)]
    [InlineData(LEGAL_INN_VALID_UINT64)]
    public void Local(in object value)
    {
        Assert.False(ForeignAttribute.IsValid(value));
        Assert.True(LocalAttribute.IsValid(value));
    }

    [Fact]
    public void Null()
    {
        Assert.True(AttributeIndividual.IsValid(null));
        Assert.True(AttributeLegal.IsValid(null));
        Assert.True(Attribute.IsValid(null));
    }

    [Fact]
    public void Obj()
    {
        Assert.False(AttributeIndividual.IsValid(new()));
        Assert.False(AttributeLegal.IsValid(new()));
        Assert.False(Attribute.IsValid(new()));
    }

    [Fact]
    public void EmptyString()
    {
        Assert.False(AttributeIndividual.IsValid(string.Empty));
        Assert.False(AttributeLegal.IsValid(string.Empty));
        Assert.False(Attribute.IsValid(string.Empty));
    }

    [Fact]
    public void MinusOne()
    {
        Assert.False(AttributeIndividual.IsValid(-1));
        Assert.False(AttributeLegal.IsValid(-1));
        Assert.False(Attribute.IsValid(-1));
    }

    [Fact]
    public void MaxValue()
    {
        Assert.False(AttributeIndividual.IsValid(ulong.MaxValue));
        Assert.False(AttributeLegal.IsValid(ulong.MaxValue));
        Assert.False(Attribute.IsValid(ulong.MaxValue));
    }
}