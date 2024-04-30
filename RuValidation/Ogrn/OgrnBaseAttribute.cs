using System.Globalization;

namespace RuValidation;

public abstract class OgrnBaseAttribute(bool checkHash = true) : ValidationAttribute
{
    protected abstract int Length { get; }
    protected abstract ulong MaxValue { get; }
    protected abstract ulong MinValue { get; }
    private bool CheckHash { get; } = checkHash;
    protected abstract string InvalidCharacter { get; }
    protected abstract string InvalidHash { get; }
    protected abstract string InvalidLength { get; }
    protected abstract string InvalidValue { get; }

    internal new ValidationResult? IsValid(object? value) => IsValid(value, null!);

    protected override ValidationResult? IsValid(object? value, ValidationContext _)
    {
        switch (value)
        {
            case null:
                return null;
            case string stringValue when !CheckHash:
            {
                if (stringValue.Length != Length) return new(InvalidLength);
                foreach (char c in stringValue)
                    if (c is < '0' or > '9')
                        return new(InvalidCharacter);
                return ValidationResult.Success;
            }
            case IConvertible convertible:
            {
                ulong current;
                try
                {
                    if (value is string str)
                    {
                        if (str.Length == 0) return new(InvalidValue);
                        current = ulong.Parse(str, NumberStyles.None, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        current = Convert.ToUInt64(convertible);
                    }
                }
                catch (FormatException)
                {
                    return new(InvalidValue);
                }
                catch (OverflowException)
                {
                    return new(InvalidValue);
                }

                if (current < MinValue || current >= MaxValue) return new(InvalidLength);
                if (!CheckHash) return ValidationResult.Success;

                (ulong quotient, ulong remainder) = Math.DivRem(current, 10);
                ulong multiplierUlong = (ulong)Length - 2;
                ulong hash = quotient - quotient / multiplierUlong * multiplierUlong;
                return hash == remainder ? ValidationResult.Success : new(InvalidHash);
            }
            default:
                return new(InvalidValue);
        }
    }
}