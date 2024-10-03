using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Numerics;
using System;
using System.Collections.Generic;

public class TalentAssessmentSystem
{
    private static int KthSmallest(int start1, int end1, int[] nums1, int start2, int end2, int[] nums2, int k)
    {
        int len1 = end1 - start1 + 1;
        int len2 = end2 - start2 + 1;

        
        if (len1 > len2) return KthSmallest(start2, end2, nums2, start1, end1, nums1, k);

        
        if (len1 == 0) return nums2[start2 + k - 1];

        
        if (k == 1) return Math.Min(nums1[start1], nums2[start2]);

       
        int i = start1 + Math.Min(len1, k / 2) - 1;
        int j = start2 + Math.Min(len2, k / 2) - 1;

       
        if (nums1[i] > nums2[j])
        {
            return KthSmallest(start1, end1, nums1, j + 1, end2, nums2, k - (j - start2 + 1));
        }
        else
        {
            return KthSmallest(i + 1, end1, nums1, start2, end2, nums2, k - (i - start1 + 1));
        }
    }

    public static double FindMedianTalentIndex(int[] fireAbility, int[] iceAbility)
    {
        int n = fireAbility.Length;
        int m = iceAbility.Length;

        int left = (n + m + 1) / 2;
        int right = (n + m + 2) / 2;

        return (KthSmallest(0, n - 1, fireAbility, 0, m - 1, iceAbility, left) +
                KthSmallest(0, n - 1, fireAbility, 0, m - 1, iceAbility, right)) * 0.5;
    }
}
public class TalentAssessmentSystemTests
{
    //[Test]
    static public void TestFindMedianTalentIndex()
    {
        // 测试用例
        List<Tuple<int[], int[], double>> testCases = new List<Tuple<int[], int[], double>>()
        {
            Tuple.Create(new int[] { 1, 3, 7, 9, 11 }, new int[] { 2, 4, 8, 10, 12, 14 }, 8.0),
            Tuple.Create(new int[] { 1, 2 }, new int[] { 3, 4 }, 2.5),
            Tuple.Create(new int[] { 1, 3 }, new int[] { 2 }, 2.0),
            Tuple.Create(new int[] { 1, 2, 3 }, new int[] { 4, 5, 6, 7 }, 4.0),
            Tuple.Create(new int[] { 1, 2, 3, 4 }, new int[] { 5, 6, 7, 8, 9 }, 5.0),
            Tuple.Create(new int[] { 1, 3, 5 }, new int[] { 2, 4, 6, 8, 10 }, 4.5)
        };

        bool allTestsPassed = true;

        foreach (var testCase in testCases)
        {
            var result = TalentAssessmentSystem.FindMedianTalentIndex(testCase.Item1, testCase.Item2);
            if (Math.Abs(result - testCase.Item3) > 0.001)
            {
                Console.WriteLine($"Test Failed for input: [{string.Join(", ", testCase.Item1)}], [{string.Join(", ", testCase.Item2)}]");
                Console.WriteLine($"Expected: {testCase.Item3}");
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