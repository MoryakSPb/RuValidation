#pragma warning disable CA1822

namespace RuValidation.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public partial class LocalPassportNumberBenchmark
{
    private const string _NUMBER_12 = "12 34 567890";
    private static readonly object _numberLong = 1234567890L;
    private static readonly object _numberUlong = 1234567890UL;
    private static readonly LocalPassportNumberAttribute _attribute = new();


    [Benchmark(Baseline = true)]
    public void Attribute()
    {
        _attribute.IsValid(_NUMBER_12);
    }

    [Benchmark]
    public void AttributeUInt64()
    {
        _attribute.IsValid(_numberUlong);
    }


    [Benchmark]
    public void AttributeInt64()
    {
        _attribute.IsValid(_numberLong);
    }

#if NET8_0_OR_GREATER
    private static readonly Regex _regex = MyRegex();

    [Benchmark]
    public void RegexSourceGenerator()
    {
        _ = _regex.IsMatch(_NUMBER_12);
    }

    [GeneratedRegex(@"^\d{2} ?\d{2} ?\d{6}$")]
    private static partial Regex MyRegex();
#else
    [Benchmark]
    public void RegexSourceGenerator()
    {
        throw new NotSupportedException();
    }
#endif
}