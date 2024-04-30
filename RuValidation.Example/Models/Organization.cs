namespace RuValidation.Example.Models;

public record Organization
{
    [InnLegal]
    public ulong Inn { get; init; }

    [Kpp]
    public ulong Kpp { get; init; }

    [OgrnLegal]
    public ulong Ogrn { get; init; }
}