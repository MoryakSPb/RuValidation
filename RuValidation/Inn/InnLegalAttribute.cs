namespace RuValidation;

public class InnLegalAttribute(bool checkHash = true) : InnBaseAttribute(checkHash)
{
    protected internal override ulong MaxValue => IntegerExtensions.POWER10_11;
    protected internal override ulong MinValue => IntegerExtensions.POWER10_09;
    protected internal override int Length => 10;
    protected override string InvalidCharacter => Inn_Legal_InvalidCharacter;
    protected override string InvalidHash => Inn_Legal_InvalidHash;
    protected override string InvalidLength => Inn_Legal_InvalidLength;
    protected override string InvalidValue => Inn_Legal_InvalidValue;

    protected override bool CalcHash(in ulong value)
    {
        ulong hash = value % 10UL;

        ulong rem = value / 10UL;
        ulong actual = 0;
        for (int j = 8; j >= 0; j--)
        {
            (rem, ulong digit) = Math.DivRem(rem, 10);
            actual += digit * InnHashSalt[j + 2];
        }

        actual = actual % 11 % 10;

        return actual == hash;
    }

    protected override bool? CalcHash(in string value)
    {
        Span<byte> digits = stackalloc byte[10];
        int i = 0;
        foreach (char symbol in value)
            switch (symbol)
            {
                case >= '0' and <= '9':
                    digits[i] = (byte)(symbol - 0x30);
                    i++;
                    continue;
                default: return null;
            }

        int hash = 0;
        for (int j = 0; j < 9; j++) hash += digits[j] * InnHashSalt[j + 2];
        hash = hash % 11 % 10;

        return hash == digits[^1];
    }

    protected override bool PassRestrictions(in ulong _) => true;

    protected override bool PassRestrictions(in string _) => true;
}