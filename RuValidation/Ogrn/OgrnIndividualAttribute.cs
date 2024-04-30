namespace RuValidation;

public class OgrnIndividualAttribute(bool checkHash = true) : OgrnBaseAttribute(checkHash)
{
    protected override int Length => 15;

    protected override ulong MaxValue => IntegerExtensions.POWER10_15;

    protected override ulong MinValue => IntegerExtensions.POWER10_14;

    protected override string InvalidCharacter => OgrnIndividual_InvalidCharacter;
    protected override string InvalidHash => OgrnIndividual_InvalidHash;
    protected override string InvalidLength => OgrnIndividual_InvalidLength;
    protected override string InvalidValue => OgrnIndividual_InvalidValue;
}