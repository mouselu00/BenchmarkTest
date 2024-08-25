# BenchmarkTest

#DotNET #NetCore

套件 : [dotnet/BenchmarkDotNet: Powerful .NET library for benchmarking (github.com)](https://github.com/dotnet/BenchmarkDotNet) 
參考: [C#: BenchmarkDotnet —— 效能測試好簡單 | 伊果的沒人看筆記本 (igouist.github.io)](https://igouist.github.io/post/2021/06/benchmarkdotnet/)

## 測試效能注意事項
1.  開一個新的專案，該專案用於測試效能用，建議不要和實際專案發那個在一起增加專案體積

## 測試目標
案例： 測試空值的列表 `Enumerable.Empty` 對比 `new List<string>()` 那一個比較好

## 測試步驟

### 1. 建立測試專案
建立一個 `ConsoleApp` ，命名為 `Benchmarktest`

### 2. 引用 BenchmarkDotnet
到`NuGet` 查詢 “BenchmarkDotNet”

### 3. 撰寫測試效能的平台及測試方法
建立測試用的個人訊息類 `PersonInformation`
```csharp
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
```

建立測試效能的類 `TestStage` 及測試對比的方法 `NewEmptyList()` 和 `NewEmptyEnumerable()`, 並在測試方法的設定 `[Benchmark]` 屬性告知程式運行需要記錄效能
```csharp
/// <summary>
/// 測試舞台
/// </summary>
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
```

### 4.主程式運行中宣告測試運行
宣告運行測試的主要進入函數
```csharp
Console.WriteLine("Test Start!");
var summary = BenchmarkRunner.Run<TestStage>();
```

### 5.執行測試
設定執行為發布版本`release`,

### 6. 結果反饋
完成執行後，可以透過 `Prompt` 得知
- 執行的專案資訊
- 執行前後的設備效能及資源用量
- 執行過程的次數及過程資訊
- 完成執行後的結果
- 生成結果報告的檔案路徑
	- `csv`
	- `markdown`
	- `html`
- 執行測試的用時

#### 執行結果說明：
1. **平均值 (Mean)**：
    - 平均值是一組數據的算術平均數，它表示所有測量值的總和除以測量的總數。
    - 例如，如果您有一組數據 {2, 4, 6, 8, 10}，則平均值為 (2 + 4 + 6 + 8 + 10) / 5 = 6。
2. **標準誤差 (Standard Error)**：
    - 標準誤差是用來估計樣本平均值與整體母體平均值之間的差異。
    - 它通常用於報告樣本平均值的精確性。較小的標準誤差表示樣本平均值更接近整體母體平均值。
3. **標準差 (Standard Deviation)**：
    - 標準差衡量數據點相對於平均值的分散程度。它告訴我們數據的變異性有多大。
    - 計算標準差的步驟包括：
        - 將每個數據點與平均值的差的平方相加。
        - 將這些平方差的總和除以數據的總數減一。
        - 最後，取這個總和的平方根。
4. **中位數 (Median)**：
    - 中位數是將數據按大小排序後，處於中間位置的值。它將數據分成一半較小的值和一半較大的值。
    - 例如，如果您有一組數據 {2, 4, 6, 8, 10}，則中位數為 6。
5. **Gen0 (Generation 0)**：
    - Gen0 是 .NET 中的垃圾回收代數之一。它跟蹤新分配的物件，並在需要時進行回收。
    - Gen0 收集次數表示每 1000 次操作中有多少次 Gen0 垃圾回收。
6. **分配的記憶體 (Allocated Memory)**：
    - 分配的記憶體表示每個單一操作（僅限於受管理的內存）所使用的記憶體量。通常以 KB 或 MB 表示。
7. **1 納秒 (1 ns)**：
    - 1 納秒等於 0.000000001 秒，是極小的時間單位。

### 7.增加記憶體比較
除了時間比較意外，也需要比較用空間用量，使用屬性`[MemoryDiagnoser]`
```csharp
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
```

執行後的結果
可以發現`Gen0`中`NewEmptyEnumerable`完全沒有使用到 `GC`

## 總結
如果之後需要確認什麼語法執行起來會比較有效的話，可以是用這個套件`BenchmarkDotNet` 進行測試

## 參考來源
- [C#: BenchmarkDotnet —— 效能測試好簡單 | 伊果的沒人看筆記本 (igouist.github.io)](https://igouist.github.io/post/2021/06/benchmarkdotnet/)
- [dotnet/BenchmarkDotNet: Powerful .NET library for benchmarking (github.com)](https://github.com/dotnet/BenchmarkDotNet)
- [详解如何使用BenchmarkDotNet进行.NET性能测试和优化-CSDN博客](https://blog.csdn.net/OKCRoss/article/details/132714277)