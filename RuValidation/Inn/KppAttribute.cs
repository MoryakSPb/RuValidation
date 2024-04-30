namespace RuValidation;

public class KppAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        switch (value)
        {
            case null:
                return null;
            case string { Length: 9 } stringValue:
                return stringValue.Any(x => x is < '0' or > '9') ? new(Kpp_InvalidCharacter) : ValidationResult.Success;
            case string:
                return new(Kpp_InvalidLength);
            case IConvertible convertible:
                ulong current;
                try
                {
                    current = Convert.ToUInt64(convertible);
                }
                catch (OverflowException)
                {
                    return new(Kpp_InvalidValue);
                }

                return current < IntegerExtensions.POWER10_09 ? ValidationResult.Success : new(Kpp_InvalidLength);
            default:
                return new(Kpp_InvalidValue);
        }
    }
}