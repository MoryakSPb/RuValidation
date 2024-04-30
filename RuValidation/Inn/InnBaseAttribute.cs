using System.Diagnostics.CodeAnalysis;

namespace RuValidation;

public abstract class InnBaseAttribute(in bool checkHash = true) : ValidationAttribute
{
    protected static readonly IReadOnlyList<byte> InnHashSalt = [3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8];
    private bool CheckHash { get; } = checkHash;
    protected internal abstract ulong MaxValue { get; }
    protected internal abstract ulong MinValue { get; }
    protected internal abstract int Length { get; }
    protected abstract string InvalidCharacter { get; }
    protected abstract string InvalidHash { get; }
    protected abstract string InvalidLength { get; }
    protected abstract string InvalidValue { get; }

    [ExcludeFromCodeCoverage]
    protected virtual string InvalidRange => string.Empty;

    internal new ValidationResult? IsValid(object? value) => IsValid(value, null!);

    protected override ValidationResult? IsValid(object? value, ValidationContext _)
    {
        switch (value)
        {
            case null:
                return null;
            case string stringValue when stringValue.Length == Length:
            {
                if (!CheckHash)
                    return stringValue.Any(x => x is < '0' or > '9')
                        ? new(InvalidCharacter)
                        : PassRestrictions(stringValue)
                            ? ValidationResult.Success
                            : new(InvalidRange);

                bool? result = CalcHash(stringValue);
                if (result is null) return new(InvalidCharacter);
                if (!PassRestrictions(stringValue)) return new(InvalidRange);
                return result.Value ? ValidationResult.Success : new(InvalidHash);
            }
            case string:
                return new(InvalidLength);
            case IConvertible convertible:
            {
                ulong current;
                try
                {
                    current = Convert.ToUInt64(convertible);
                }
                catch (OverflowException)
                {
                    return new(InvalidValue);
                }

                if (current < MinValue || current >= MaxValue) return new(InvalidLength);
                if (!PassRestrictions(current)) return new(InvalidRange);
                return CheckHash
                    ? CalcHash(current)
                        ? ValidationResult.Success
                        : new(InvalidHash)
                    : ValidationResult.Success;
            }
            default:
                return new(InvalidValue);
        }
    }

    protected abstract bool CalcHash(in ulong value);

    protected abstract bool? CalcHash(in string value);
    protected abstract bool PassRestrictions(in ulong value);
    protected abstract bool PassRestrictions(in string value);
}