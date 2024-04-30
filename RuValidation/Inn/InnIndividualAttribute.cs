namespace RuValidation;

public class InnIndividualAttribute(bool checkHash = true) : InnBaseAttribute(checkHash)
{
    protected internal override ulong MaxValue => IntegerExtensions.POWER10_13;
    protected internal override ulong MinValue => IntegerExtensions.POWER10_11;
    protected internal override int Length => 12;
    protected override string InvalidCharacter => Inn_Individual_InvalidCharacter;
    protected override string InvalidHash => Inn_Individual_InvalidHash;
    protected override string InvalidLength => Inn_Individual_InvalidLength;
    protected override string InvalidValue => Inn_Individual_InvalidValue;

    protected override bool CalcHash(in ulong value)
    {
        ulong hash = value % 100UL;

        ulong rem = value / 100;
        ulong hashLeft = 0;
        ulong hashRight = 0;
        for (int j = 9; j >= 0; j--)
        {
            (rem, ulong digit) = Math.DivRem(rem, 10);
            hashLeft += digit * InnHashSalt[j + 1];
            hashRight += digit * InnHashSalt[j];
        }

        hashLeft = hashLeft % 11 % 10;
        hashRight += hashLeft * InnHashSalt[^1];
        hashRight = hashRight % 11 % 10;

        return hashLeft * 10 + hashRight == hash;
    }

    protected override bool? CalcHash(in string value)
    {
        Span<byte> digits = stackalloc byte[12];
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

        int hashLeft = 0;
        int hashRight = 0;
        for (int j = 0; j < 10; j++)
        {
            hashLeft += digits[j] * InnHashSalt[j + 1];
            hashRight += digits[j] * InnHashSalt[j];
        }

        hashLeft = hashLeft % 11 % 10;
        hashRight += hashLeft * InnHashSalt[^1];
        hashRight = hashRight % 11 % 10;

        return hashLeft == digits[^2] && hashRight == digits[^1];
    }

    protected override bool PassRestrictions(in ulong _) => true;

    protected override bool PassRestrictions(in string _) => true;
}