namespace RuValidation;

public class InnLegalLocalAttribute(bool checkHash = true) : InnLegalAttribute(checkHash)
{
    protected override string InvalidRange => Inn_Legal_Local_InvalidRange;
    protected override bool PassRestrictions(in ulong value) => value is < 9909000000 or > 9909999999;

    protected override bool PassRestrictions(in string value) => value[0] != '9'
                                                                 || value[1] != '9'
                                                                 || value[2] != '0'
                                                                 || value[3] != '9';
}