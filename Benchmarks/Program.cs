using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmarks;

internal class Program
{
    static void Main(string[] args)
    {
        //var demo = new Demo();
        //demo.UseExceptions();
        //Console.WriteLine("Hello, World!");

        var summary = BenchmarkRunner.Run<Demo>();
        
        //Console.ReadLine();
    }
}


[MemoryDiagnoser]
[DisassemblyDiagnoser]
[CsvMeasurementsExporter]
public class Demo
{
    private const int ListsCount = 1_000_000;
    private const int Count2 = 1_000;
    private const int IfSwitchCount = 100_000_000;


    #region Object Sizes
    [Benchmark]
    [Arguments(3)]
    public ObjectA FlatObject(int init) => new(init);

    [Benchmark]
    [Arguments(3)]
    public ObjectB NestedObject(int init) => new(init);
    #endregion

    #region Task vs non-Task
    [Benchmark]
    [Arguments(1)]
    [Arguments(3)]
    public async Task<double> UseTask(int init)
    {
        return await DoWorkAsync(init);
    }
    async Task<double> DoWorkAsync(int val)
    {
        return 15.0 / val;
    }

    [Benchmark]
    [Arguments(1)]
    [Arguments(3)]
    public double DontUseTask(int init)
    {
        return DoWork(init);
    }
    double DoWork(int val)
    {
        return 15.0 / val;
    }
    #endregion

    #region Wrapper
    [Benchmark]
    [Arguments(3)]
    public void IntWrapper(int init)
    {
        var b = new List<Wrapper> { new Wrapper() { IntVal = init } };
        for (var a = 0; a < ListsCount; a++)
        {
            b.Add(new Wrapper() { IntVal = a });
        }
        var c = b.Where(x => x.IntVal > 500_000).ToList();
    }

    [Benchmark]
    [Arguments(3)]
    public void ListOfInts(int init)
    {
        var b = new List<int> { init };
        for (var a = 0; a < ListsCount; a++)
        {
            b.Add(a);
        }
        var c = b.Where(x => x > 500_000).ToList();
    }
    #endregion

    #region IEnumerable vs List
    [Benchmark]
    [Arguments(-1)]
    public void GetIntsFromIEnumerable(int init)
    {
        var b = GetInts(init).ToList();
        var c = b.Where(x => x > 500_000).ToList();
    }
    IEnumerable<int> GetInts(int init)
    {
        yield return init;
        for (var a = 0; a < ListsCount; a++)
        {
            yield return a;
        }
    }

    [Benchmark]
    [Arguments(-1)]
    public void GetIntsFromList(int init)
    {
        var b = GetInts2(init);
        var c = b.Where(x => x > 500_000).ToList();
    }
    List<int> GetInts2(int init)
    {
        var b = new List<int>() { init };
        for (var a = 0; a < ListsCount; a++)
        {
            b.Add(a);
        }
        return b;
    }
    #endregion

    #region Exception vs non-exception
    [Benchmark]
    [Arguments(3)]
    [Arguments(0)]
    public int UseExceptions(int init)
    {
        try
        {
            return 15 / init;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    [Benchmark]
    [Arguments(3)]
    [Arguments(0)]
    public int UseSpecificExceptions(int init)
    {
        try
        {
            return 15 / init;
        }
        catch (DivideByZeroException ex)
        {
            return 0;
        }
    }

    [Benchmark]
    [Arguments(3)]
    [Arguments(0)]
    public int DontUseExceptions(int init)
    {
        if (init == 0)
        {
            return 0;
        }

        return 15 / init;
    }
    #endregion

    #region Switch vs IfThenElse
    [Benchmark]
    [Arguments("A")]
    [Arguments("Z")]
    [Arguments("AI")]
    public int GetSwitchStatement(string b)
    {
        var result = 0;
        switch (b)
        {
            case "A":
                result = 1;
                break;
            case "B":
                result = 9;
                break;
            case "C":
                result = 3;
                break;
            case "D":
                result = 14;
                break;
            case "E":
                result = 5;
                break;
            case "F":
                result = 6;
                break;
            case "G":
                result = 7;
                break;
            case "H":
                result = 84;
                break;
            case "I":
                result = 9;
                break;
            case "J":
                result = 150;
                break;
            case "K":
                result = 11;
                break;
            case "L":
                result = 12;
                break;
            case "M":
                result = 13;
                break;
            case "N":
                result = 14;
                break;
            case "O":
                result = 165;
                break;
            case "P":
                result = 222;
                break;
            case "Q":
                result = 17;
                break;
            case "R":
                result = 18;
                break;
            case "S":
                result = 111;
                break;
            case "T":
                result = 270;
                break;
            case "U":
                result = 21;
                break;
            case "V":
                result = 22;
                break;
            case "W":
                result = 23;
                break;
            case "X":
                result = 24;
                break;
            case "Y":
                result = 432;
                break;
            case "Z":
                result = 726;
                break;
            case "AA":
                result = 27;
                break;
            case "AB":
                result = 2888;
                break;
            case "AC":
                result = 29;
                break;
            case "AD":
                result = 30;
                break;
            case "AE":
                result = 31;
                break;
            case "AF":
                result = 3212;
                break;
            case "AG":
                result = 33;
                break;
            case "AH":
                result = 3324;
                break;
            case "AI":
                result = 315;
                break;
            default:
                result = 5435;
                break;
        }

        return result + result;
    }

    [Benchmark]
    [Arguments("A")]
    [Arguments("Z")]
    [Arguments("AI")]
    public int GetSwitchExpression(string b)
    {
        var result = b switch
        {
            "A" => 1,
            "B" => 9,
            "C" => 3,
            "D" => 14,
            "E" => 5,
            "F" => 6,
            "G" => 7,
            "H" => 84,
            "I" => 9,
            "J" => 150,
            "K" => 11,
            "L" => 12,
            "M" => 13,
            "N" => 14,
            "O" => 165,
            "P" => 222,
            "Q" => 17,
            "R" => 18,
            "S" => 111,
            "T" => 270,
            "U" => 21,
            "V" => 22,
            "W" => 23,
            "X" => 24,
            "Y" => 432,
            "Z" => 726,
            "AA" => 27,
            "AB" => 2888,
            "AC" => 29,
            "AD" => 30,
            "AE" => 31,
            "AF" => 3212,
            "AG" => 33,
            "AH" => 3324,
            "AI" => 315,
            _ => 5435
        };

        return result + result;
    }

    [Benchmark]
    [Arguments("A")]
    [Arguments("Z")]
    [Arguments("AI")]
    public int GetIfThenElse(string b)
    {
        var result = 0;
        if (b == "A")
            result = 1;
        else if (b == "B")
            result = 9;
        else if (b == "C")
            result = 3;
        else if (b == "D")
            result = 14;
        else if (b == "E")
            result = 5;
        else if (b == "F")
            result = 6;
        else if (b == "G")
            result = 7;
        else if (b == "H")
            result = 84;
        else if (b == "I")
            result = 9;
        else if (b == "J")
            result = 150;
        else if (b == "K")
            result = 11;
        else if (b == "L")
            result = 12;
        else if (b == "M")
            result = 13;
        else if (b == "N")
            result = 14;
        else if (b == "O")
            result = 165;
        else if (b == "P")
            result = 222;
        else if (b == "Q")
            result = 17;
        else if (b == "R")
            result = 18;
        else if (b == "S")
            result = 111;
        else if (b == "T")
            result = 270;
        else if (b == "U")
            result = 21;
        else if (b == "V")
            result = 22;
        else if (b == "W")
            result = 23;
        else if (b == "X")
            result = 24;
        else if (b == "Y")
            result = 432;
        else if (b == "Z")
            result = 726;
        else if (b == "AA")
            result = 27;
        else if (b == "AB")
            result = 2888;
        else if (b == "AC")
            result = 29;
        else if (b == "AD")
            result = 30;
        else if (b == "AE")
            result = 31;
        else if (b == "AF")
            result = 3212;
        else if (b == "AG")
            result = 33;
        else if (b == "AH")
            result = 3324;
        else if (b == "AI")
            result = 315;
        else
            result = 5435;

        return result + result;
    }
    #endregion
}