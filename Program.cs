// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Test Start!");
var summary = BenchmarkRunner.Run<TestStage>();

/// <summary>
/// 測試舞台
/// </summary>
[MemoryDiagnoser]
public class TestStage
{

    /// <summary>
    /// 測試效能方法A
    /// </summary>
    [Benchmark]
    public void NewEmptyList()
    {
        new List<PersonInformation>();
    }

    /// <summary>
    /// 測試效能方法B
    /// </summary>
    [Benchmark]
    public void NewEmptyEnumerable()
    {

        Enumerable.Empty<PersonInformation>();
    }

}


/// <summary>
/// 個人訊息類
/// </summary>
public class PersonInformation
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

}

