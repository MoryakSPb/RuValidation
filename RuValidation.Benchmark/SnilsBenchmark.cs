#pragma warning disable CA1822
#pragma warning disable SYSLIB1045

namespace RuValidation.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public partial class SnilsBenchmark
{
    private const string _SNILS = "166-027-585 72";
    private const string _SNILS_WITHOUT_DELIMITERS = "16602758572";
    private static readonly object _snilsLong = 16602758572;
    private static readonly object _snilsUlong = 16602758572ul;
    private static readonly SnilsAttribute _attribute = new(false);
    private static readonly SnilsAttribute _attributeWithHash = new();
    private static readonly Regex _regex = new(@"^\d{3}-\d{3}-\d{3} \d{2}$");

    [Benchmark]
    public void Attribute()
    {
        _attribute.IsValid(_SNILS);
    }

    [Benchmark(Baseline = true)]
    public void AttributeWithHash()
    {
        _attributeWithHash.IsValid(_SNILS);
    }

    [Benchmark]
    public void AttributeWithoutDelimiters()
    {
        _attribute.IsValid(_SNILS_WITHOUT_DELIMITERS);
    }

    [Benchmark]
    public void AttributeWithHashWithoutDelimiters()
    {
        _attributeWithHash.IsValid(_SNILS_WITHOUT_DELIMITERS);
    }

    [Benchmark]
    public void AttributeWithoutDelimitersInt64()
    {
        _attribute.IsValid(_snilsLong);
    }

    [Benchmark]
    public void AttributeWithHashWithoutDelimitersInt64()
    {
        _attributeWithHash.IsValid(_snilsLong);
    }

    [Benchmark]
    public void AttributeWithoutDelimitersUInt64()
    {
        _attribute.IsValid(_snilsUlong);
    }

    [Benchmark]
    public void AttributeWithHashWithoutDelimitersUInt64()
    {
        _attributeWithHash.IsValid(_snilsUlong);
    }

    [Benchmark]
    public void RegexInterpreted()
    {
        bool _ = Regex.IsMatch(_SNILS, @"^\d{3}-\d{3}-\d{3} \d{2}$");
    }

    [Benchmark]
    public void RegexInterpretedStatic()
    {
        bool _ = _regex.IsMatch(_SNILS);
    }
#if NET8_0_OR_GREATER
    [Benchmark]
    public void RegexSourceGenerator()
    {
        bool _ = MyRegex().IsMatch(_SNILS);
    }

    [GeneratedRegex(@"^\d{3}-\d{3}-\d{3} \d{2}$")]
    private static partial Regex MyRegex();
#else
    [Benchmark]
    public void RegexSourceGenerator()
    {
        throw new NotSupportedException();
    }
#endif
}