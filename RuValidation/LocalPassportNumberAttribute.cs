namespace RuValidation;

public class LocalPassportNumberAttribute(bool availableSpaces = true) : ValidationAttribute
{
    public bool AvailableSpaces { get; } = availableSpaces;

    protected override ValidationResult? IsValid(object? value, ValidationContext _)
    {
        switch (value)
        {
            case null:
                return null;
            case string stringValue:
                switch (stringValue.Length)
                {
                    case 10:
                        return stringValue.Any(x => x is < '0' or > '9')
                            ? new(LocalPassportNumber_InvalidCharacter)
                            : ValidationResult.Success;
                    case 11 when AvailableSpaces:
                        for (int i = 0; i < 11; i++)
                        {
                            char c = stringValue[i];
                            switch (c)
                            {
                                case ' ' when i is 4:
                                    continue;
                                case < '0' or > '9':
                                    return new(LocalPassportNumber_InvalidCharacter);
                            }
                        }

                        return ValidationResult.Success;
                    case 12 when AvailableSpaces:
                        for (int i = 0; i < 12; i++)
                        {
                            char c = stringValue[i];
                            switch (c)
                            {
                                case ' ' when i is 5 or 2:
                                    continue;
                                case < '0' or > '9':
                                    return new(LocalPassportNumber_InvalidCharacter);
                            }
                        }

                        return ValidationResult.Success;
                    default:
                        return new(LocalPassportNumber_InvalidLength);
                }
            case IConvertible convertible:
                ulong current;
                try
                {
                    current = Convert.ToUInt64(convertible);
                }
                catch (OverflowException)
                {
                    return new(LocalPassportNumber_InvalidValue);
                }

                return current <= 9999_999999ul ? ValidationResult.Success : new(LocalPassportNumber_InvalidLength);
            default:
                return new(LocalPassportNumber_InvalidValue);
        }
    }
}