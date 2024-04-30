using System.Globalization;

namespace RuValidation.Tests;

public class OgrnTests
{
    private const ulong _OGRN_LEGAL_ULONG = 1047707030513UL;
    private const ulong _OGRN_LEGAL_ULONG_LEN_PLUS = 10477070305132UL;
    private const ulong _OGRN_LEGAL_ULONG_LEN_MINUS = 104770703051UL;
    private const ulong _OGRN_LEGAL_ULONG_INVALID_HASH = 1047707030514UL;

    private const ulong _OGRN_INDIVIDUAL_ULONG = 316861700133226UL;
    private const ulong _OGRN_INDIVIDUAL_ULONG_LEN_PLUS = 3168617001332261UL;
    private const ulong _OGRN_INDIVIDUAL_ULONG_LEN_MINUS = 31686170013322UL;
    private const ulong _OGRN_INDIVIDUAL_ULONG_INVALID_HASH = 316861700133223UL;

    [Fact]
    public void BenchData()
    {
        OgrnAttribute attribute = new();
        Assert.True(attribute.IsValid("1047707030513"));
    }

    [Theory]
    [InlineData(_OGRN_LEGAL_ULONG, _OGRN_LEGAL_ULONG_LEN_PLUS, _OGRN_LEGAL_ULONG_LEN_MINUS,
        _OGRN_LEGAL_ULONG_INVALID_HASH, false)]
    [InlineData(_OGRN_INDIVIDUAL_ULONG, _OGRN_INDIVIDUAL_ULONG_LEN_PLUS, _OGRN_INDIVIDUAL_ULONG_LEN_MINUS,
        _OGRN_INDIVIDUAL_ULONG_INVALID_HASH, true)]
    public void Valid(
        in ulong valid,
        in ulong lenPlus,
        in ulong lenMinus,
        in ulong invalidHash,
        in bool isIndividual)
    {
        OgrnAttribute attribute = new();
        OgrnAttribute attributeWithoutHash = new(false);
        OgrnBaseAttribute baseAttribute = isIndividual ? new OgrnIndividualAttribute() : new OgrnLegalAttribute();
        OgrnBaseAttribute baseAttributeWithoutHash =
            isIndividual ? new OgrnIndividualAttribute(false) : new OgrnLegalAttribute(false);

        Assert.True(attribute.IsValid(null));
        Assert.True(attributeWithoutHash.IsValid(null));
        Assert.True(baseAttribute.IsValid(null));
        Assert.True(baseAttributeWithoutHash.IsValid(null));

        Assert.True(attribute.IsValid(valid));
        Assert.True(attribute.IsValid(valid.ToString("D", CultureInfo.InvariantCulture)));
        Assert.True(attributeWithoutHash.IsValid(valid));
        Assert.True(attributeWithoutHash.IsValid(valid.ToString("D", CultureInfo.InvariantCulture)));
        Assert.True(baseAttribute.IsValid(valid));
        Assert.True(baseAttribute.IsValid(valid.ToString("D", CultureInfo.InvariantCulture)));
        Assert.True(baseAttributeWithoutHash.IsValid(valid));
        Assert.True(baseAttributeWithoutHash.IsValid(valid.ToString("D", CultureInfo.InvariantCulture)));

        Assert.False(attribute.IsValid(lenPlus));
        Assert.False(attribute.IsValid(lenPlus.ToString("D", CultureInfo.InvariantCulture)));
        Assert.False(attributeWithoutHash.IsValid(lenPlus));
        Assert.False(
            attributeWithoutHash.IsValid(lenPlus.ToString("D", CultureInfo.InvariantCulture)));
        Assert.False(baseAttribute.IsValid(lenPlus));
        Assert.False(baseAttribute.IsValid(lenPlus.ToString("D", CultureInfo.InvariantCulture)));
        Assert.False(baseAttributeWithoutHash.IsValid(lenPlus));
        Assert.False(
            baseAttributeWithoutHash.IsValid(lenPlus.ToString("D", CultureInfo.InvariantCulture)));

        Assert.False(attribute.IsValid(lenMinus));
        Assert.False(attribute.IsValid(lenMinus.ToString("D", CultureInfo.InvariantCulture)));
        Assert.False(attributeWithoutHash.IsValid(lenMinus));
        Assert.False(
            attributeWithoutHash.IsValid(lenMinus.ToString("D", CultureInfo.InvariantCulture)));
        Assert.False(baseAttribute.IsValid(lenMinus));
        Assert.False(baseAttribute.IsValid(lenMinus.ToString("D", CultureInfo.InvariantCulture)));
        Assert.False(baseAttributeWithoutHash.IsValid(lenMinus));
        Assert.False(
            baseAttributeWithoutHash.IsValid(
                lenMinus.ToString("D", CultureInfo.InvariantCulture)));

        Assert.False(attribute.IsValid(invalidHash));
        Assert.False(attribute.IsValid(invalidHash.ToString("D", CultureInfo.InvariantCulture)));
        Assert.True(attributeWithoutHash.IsValid(invalidHash));
        Assert.True(
            attributeWithoutHash.IsValid(invalidHash.ToString("D", CultureInfo.InvariantCulture)));
        Assert.False(baseAttribute.IsValid(invalidHash));
        Assert.False(
            baseAttribute.IsValid(invalidHash.ToString("D", CultureInfo.InvariantCulture)));
        Assert.True(baseAttributeWithoutHash.IsValid(invalidHash));
        Assert.True(
            baseAttributeWithoutHash.IsValid(
                invalidHash.ToString("D", CultureInfo.InvariantCulture)));

        Assert.False(attribute.IsValid(-(long)valid));
        Assert.False(baseAttribute.IsValid(-(long)valid));
    }

    [Fact]
    public void Base()
    {
        OgrnAttribute attribute = new();
        OgrnAttribute attributeWithoutHash = new(false);
        OgrnBaseAttribute baseIndividualAttribute = new OgrnIndividualAttribute();
        //OgrnBaseAttribute baseIndividualAttributeWithoutHash = new OgrnIndividualAttribute(false);
        OgrnBaseAttribute baseLegalAttribute = new OgrnLegalAttribute();
        //OgrnBaseAttribute baseLegalAttributeWithoutHash = new OgrnLegalAttribute(false);

        Assert.False(attribute.IsValid(new()));
        Assert.False(baseIndividualAttribute.IsValid(new()));
        Assert.False(baseIndividualAttribute.IsValid("10477070303UL"));
        Assert.False(baseIndividualAttribute.IsValid(""));
        Assert.False(baseIndividualAttribute.IsValid("12345678901234_"));
        Assert.False(baseLegalAttribute.IsValid(new()));
        Assert.False(baseLegalAttribute.IsValid("316861700133226UL"));
        Assert.False(baseLegalAttribute.IsValid(""));
        Assert.False(attribute.IsValid("12345678901234_"));
        Assert.False(attributeWithoutHash.IsValid("12345678901234_"));
        Assert.False(attribute.IsValid("123456789012_"));
        Assert.False(attributeWithoutHash.IsValid("123456789012_"));
    }
}