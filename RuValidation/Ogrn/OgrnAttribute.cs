namespace RuValidation;

public class OgrnAttribute(bool checkHash = true) : ValidationAttribute
{
    private readonly OgrnBaseAttribute _individualAttribute = new OgrnIndividualAttribute(checkHash);
    private readonly OgrnBaseAttribute _legalAttribute = new OgrnLegalAttribute(checkHash);

    protected override ValidationResult? IsValid(object? value, ValidationContext _)
    {
        switch (value)
        {
            case null:
                return null;
            case string { Length: 13 }:
                return _legalAttribute.IsValid(value);
            case string { Length: 15 }:
                return _individualAttribute.IsValid(value);
            case string:
                goto default;
            case IConvertible:
                ulong ulongValue;
                try
                {
                    ulongValue = Convert.ToUInt64(value);
                }
                catch (OverflowException)
                {
                    goto default;
                }

                switch (ulongValue)
                {
                    case >= IntegerExtensions.POWER10_12 and < IntegerExtensions.POWER10_13:
                        return _legalAttribute.IsValid(value);
                    case >= IntegerExtensions.POWER10_14 and < IntegerExtensions.POWER10_15:
                        return _individualAttribute.IsValid(value);
                }

                goto default;
            default:
                return new(Ogrn_InvalidValue);
        }
    }
}