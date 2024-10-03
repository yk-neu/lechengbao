using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Numerics;
using System;
using System.Collections.Generic;

public class EnergyFieldSystem
{
    public static float MaxEnergyField(int[] heights)
    {
        int n = heights.Length;
        float maxArea = 0f;

        // 构建辅助数组 topHeights
        int[] topHeights = new int[n];
        Stack<int> stack = new Stack<int>();
        List<int> optLeft = new List<int>();
        for (int a = 0; a < n; a++)
        { int h = heights[a];
            if (optLeft.Count() == 0)
            {
                optLeft.Add(a);
            }
            else
            {
                if (h > heights[optLeft[optLeft.Count-1 ]]) ;
                optLeft.Add(a);
            }

            float optmaxArea = 0;
            for (int i = 0; i < optLeft.Count; i++)
            {
                if (a > optLeft[i])

                {
                    int left = optLeft[i];
                    int lefth = heights[left];
                    float temp = CalRectArea(left, a, heights);
                    if (temp > optmaxArea)
                    {
                        optLeft.RemoveRange(0, i);
                        i = 0;
                        optmaxArea = temp;
                    }
                }
            }
            maxArea = MathF.Max(maxArea, optmaxArea);
        }


        Console.WriteLine(maxArea);
        return maxArea;
    }
    public static float CalRectArea(int left,int right, int[] heights)
    {   

        return ((float)heights[left]+heights[right])/2*(right-left);
    }
}



public class EnergyFieldSystemTests
{
  
     static public void TestMaxEnergyField()
    {
        // 测试用例
        List<Tuple<int[], float>> testCases = new List<Tuple<int[], float>>()
        {
            Tuple.Create(new int[] { 10, 100, 1, 9 }, 109f),
            Tuple.Create(new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 }, 52.5f),
            Tuple.Create(new int[] { 1, 2, 3, 4, 5 }, 12f),
            Tuple.Create(new int[] { 10, 20, 30, 40, 50, 60 }, 175f),
            Tuple.Create(new int[] { 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 }, 495f),
            Tuple.Create(new int[] { 5, 4, 3, 2, 1 }, 12f),
            Tuple.Create(new int[] { 1, 2, 3, 4, 5 }, 12f),
            Tuple.Create(new int[] { }, 0f),
            Tuple.Create(new int[] { 0,5 }, 0f),
            Tuple.Create(new int[] { 3, 5, 6, 5, 4, 1, 2, 4, 6 }, 38.5f)
        };

        bool allTestsPassed = true;

        foreach (var testCase in testCases)
        {
            var result = EnergyFieldSystem.MaxEnergyField(testCase.Item1);
            if ((float)result != testCase.Item2)
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