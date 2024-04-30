namespace RuValidation;

public class InnAttribute(bool checkHash = true) : ValidationAttribute
{
    private readonly InnIndividualAttribute _individualAttribute = new(checkHash);
    private readonly InnLegalAttribute _legalAttribute = new(checkHash);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        switch (value)
        {
            case null:
                return null;
            case string strLegalValue when strLegalValue.Length == _legalAttribute.Length:
                return _legalAttribute.IsValid(strLegalValue);
            case string strIndividualValue when strIndividualValue.Length == _individualAttribute.Length:
                return _individualAttribute.IsValid(strIndividualValue);
            case string:
                return new("Неверная длинна ИНН");
            case IConvertible convertible:
                ulong current;
                try
                {
                    current = Convert.ToUInt64(convertible);
                }
                catch (OverflowException)
                {
                    goto default;
                }

                if (current >= _legalAttribute.MinValue && current < _legalAttribute.MaxValue)
                    return _legalAttribute.IsValid(current);
                if (current >= _individualAttribute.MinValue && current < _individualAttribute.MaxValue)
                    return _individualAttribute.IsValid(current);
                goto default;
            default:
                return new("Недопустимое значение ИНН");
        }
    }
}