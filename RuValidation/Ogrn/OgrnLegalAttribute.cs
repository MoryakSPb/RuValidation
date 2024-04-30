namespace RuValidation;

public class OgrnLegalAttribute(bool checkHash = true) : OgrnBaseAttribute(checkHash)
{
    protected override int Length => 13;
    protected override ulong MaxValue => IntegerExtensions.POWER10_13;

    protected override ulong MinValue => IntegerExtensions.POWER10_12;
    protected override string InvalidCharacter => OgrnLegal_InvalidCharacter;
    protected override string InvalidHash => OgrnLegal_InvalidHash;
    protected override string InvalidLength => OgrnLegal_InvalidLength;
    protected override string InvalidValue => OgrnLegal_InvalidValue;
}