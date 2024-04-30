using System.Runtime.CompilerServices;

namespace RuValidation;

public class SnilsAttribute(bool checkHash = true) : ValidationAttribute
{
    public bool CheckHash { get; } = checkHash;

    protected override ValidationResult? IsValid(object? value, ValidationContext _)
    {
        switch (value)
        {
            case null:
                return null;
            case string stringValue:
            {
                if (stringValue.Length != 14 && stringValue.Length != 11) return new(Snils_InvalidLength);
                Span<byte> digits = stackalloc byte[11];
                int i = 0;
                for (int j = 0; j < stringValue.Length; j++)
                {
                    char symbol = stringValue[j];
                    switch (symbol)
                    {
                        case >= '0' and <= '9':
                            digits[i] = (byte)(symbol - 0x30);
                            i++;
                            continue;
                        case '-' when j is 3:
                        case '-' when j is 7:
                        case ' ' when j is 11:
                            continue;
                        default: return new(Snils_InvalidCharacter);
                    }
                }

                return CheckHash ? CalcHash(digits) : ValidationResult.Success;
            }
            case IConvertible convertible:
            {
                ulong current;
                try
                {
                    current = Convert.ToUInt64(convertible);
                }
                catch (OverflowException)
                {
                    return new(Snils_InvalidValue);
                }

                if (current > 999_999_999_99u) return new(Snils_InvalidLength);
                if (!CheckHash) return ValidationResult.Success;
                int i = 10;
                Span<byte> digits = stackalloc byte[11];
                while (current > 10)
                {
                    (current, ulong remainder) = Math.DivRem(current, 10);
                    digits[i--] = (byte)remainder;
                }

                digits[i] = (byte)current;
                return CalcHash(digits);
            }
            default:
                return new(Snils_InvalidValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static ValidationResult? CalcHash(in ReadOnlySpan<byte> digits)
        {
            int hash = 0;
            for (int j = 0; j < 9; j++) hash += digits[j] * (9 - j);
            hash %= 101;
            int originalHash = digits[9] * 10 + digits[10];
            return hash != originalHash ? new(Snils_InvalidHash) : ValidationResult.Success;
        }
    }
}