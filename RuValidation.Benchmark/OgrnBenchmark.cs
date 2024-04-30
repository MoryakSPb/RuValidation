namespace RuValidation.Benchmark;

#pragma warning disable CA1822 // Пометьте члены как статические

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net60)]
//[SimpleJob(RuntimeMoniker.NativeAot80)]
[MemoryDiagnoser]
public partial class OgrnBenchmark
{
    private const string _NUMBER = "1047707030513";
    private static readonly object _numberLong = 1047707030513L;
    private static readonly object _numberUlong = 1047707030513UL;
    private static readonly OgrnAttribute _attributeWithoutHash = new(false);
    private static readonly OgrnAttribute _attributeWithHash = new();

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
    public void AttributeWithHash()
    {
        _attributeWithHash.IsValid(_NUMBER);
    }

    [Benchmark]
    public void AttributeUInt64WithHash()
    {
        _attributeWithHash.IsValid(_numberUlong);
    }


    [Benchmark]
    public void AttributeInt64WithHash()
    {
        _attributeWithHash.IsValid(_numberLong);
    }

    [Benchmark]
    public void RegexInterpreted()
    {
#pragma warning disable SYSLIB1045 // Преобразовать в "GeneratedRegexAttribute".
        bool _ = Regex.IsMatch(_NUMBER, @"^(\d{13}|\d{15})$");
#pragma warning restore SYSLIB1045 // Преобразовать в "GeneratedRegexAttribute".
    }

#if NET8_0_OR_GREATER
    private static readonly Regex _regex = MyRegex();

    [Benchmark]
    public void RegexSourceGenerator()
    {
        _ = _regex.IsMatch(_NUMBER);
    }

    [GeneratedRegex(@"^(\d{13}|\d{15})$")]
    private static partial Regex MyRegex();
#else
    [Benchmark]
    public void RegexSourceGenerator()
    {
        throw new NotSupportedException();
    }
#endif
}