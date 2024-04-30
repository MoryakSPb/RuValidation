namespace RuValidation.Benchmark;

#pragma warning disable CA1822 // Пометьте члены как статические

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net60)]
public partial class InnBenchmark
{
    private const string _NUMBER = "7707329152";
    private static readonly object _numberLong = 7707329152L;
    private static readonly object _numberUlong = 7707329152UL;
    private static readonly InnLegalAttribute _attributeWithoutHash = new(false);
    private static readonly InnLegalAttribute _attributeWithHash = new();

    [Benchmark(Baseline = true)]
    public void Attribute()
    {
        _attributeWithoutHash.IsValid(_NUMBER);
    }

    [Benchmark]
    public void AttributeUInt64()
    {
        _attributeWithoutHash.IsValid(_numberUlong);
    }


    [Benchmark]
    public void AttributeInt64()
    {
        _attributeWithoutHash.IsValid(_numberLong);
    }

    [Benchmark]
    public void AttributeHash()
    {
        _attributeWithHash.IsValid(_NUMBER);
    }

    [Benchmark]
    public void AttributeUInt64Hash()
    {
        _attributeWithHash.IsValid(_numberUlong);
    }


    [Benchmark]
    public void AttributeInt64Hash()
    {
        _attributeWithHash.IsValid(_numberLong);
    }

#if NET8_0_OR_GREATER
    private static readonly Regex _regex = MyRegex();

    [Benchmark]
    public void RegexSourceGenerator()
    {
        _ = _regex.IsMatch(@"^(\d{10}|\d{12})$");
    }

    [GeneratedRegex(@"^\d{10}$")]
    private static partial Regex MyRegex();
#else
    [Benchmark]
    public void RegexSourceGenerator()
    {
        throw new NotSupportedException();
    }
#endif
}