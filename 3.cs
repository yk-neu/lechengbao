using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Numerics;
using System;
using System.Collections.Generic;

public class TreasureHuntSystem
{
    public static int MaxTreasureValue(int[] treasures)
    {
        if (treasures == null || treasures.Length == 0)
        {
            return 0;
        }

        int prev = 0, curr = 0, next = 0;

        for (int i = 0; i < treasures.Length; i++)
        {
            next = Math.Max(curr, prev + treasures[i]);
            prev = curr;
            curr = next;
        }

        return curr;
    }
}

public class TreasureHuntSystemTests
{
    
    static public void TestMaxTreasureValue()
    {
        // 测试用例
        List<Tuple<int[], int>> testCases = new List<Tuple<int[], int>>()
        {
            Tuple.Create(new int[] { 3, 1, 5, 2, 4 }, 12),
            Tuple.Create(new int[] { 1, 2, 3, 1 }, 4),
            Tuple.Create(new int[] { 2, 7, 9, 3, 1 }, 12),
            Tuple.Create(new int[] { 1, 2, -99,-100,7,10 }, 12),
            Tuple.Create(new int[] { 100 }, 100),
            Tuple.Create(new int[] { }, 0)
        };

        bool allTestsPassed = true;

        foreach (var testCase in testCases)
        {
            var result = TreasureHuntSystem.MaxTreasureValue(testCase.Item1);
            if (result != testCase.Item2)
            {
                Console.WriteLine($"Test Failed for input: [{string.Join(", ", testCase.Item1)}]");
                Console.WriteLine($"Expected: {testCase.Item2}");
                Console.WriteLine($"Got: {result}");
                allTestsPassed = false;
            }
        }

        if (allTestsPassed)
        {
            Console.WriteLine("All tests passed successfully.");
        }
       
    }
}