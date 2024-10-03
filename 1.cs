using System;
using System.Collections.Generic;

public class LeaderboardSystem
{
    /**
     * 获取前m个最高分数。
     *
     * @param scores 输入的分数数组。
     * @param m      要获取的最高分数的数量。
     * @return 返回包含前m个最高分数的列表。
     */
    public static List<int> GetTopScores(int[] scores, int m)
    {
        if (scores == null || scores.Length == 0 || m <= 0)
            return new List<int>();
        if (m > scores.Length)
        {
            m= scores.Length; 
        }
        // 使用BFPRT算法找到第m大的分数
        int nthLargest = QuickSelect(scores, scores.Length - m);

        // 收集所有大于等于nthLargest的分数
        List<int> result = new List<int>();
        foreach (var score in scores)
        {
            if (score >= nthLargest)
            {
                result.Add(score);
                if (result.Count >= m)
                    break; 
            }
        }

        // 对结果按降序排序
        result.Sort((a, b) => b.CompareTo(a));

        return result;
    }

    /**
     * 快速选择算法的入口。
     *
     * @param arr 输入的数组。
     * @param k   要选择的元素的位置。
     * @return 返回第k小的元素的值。
     */
    private static int QuickSelect(int[] arr, int k)
    {
        return Select(arr, 0, arr.Length - 1, k);
    }

    /**
     * 递归选择第k小的元素。
     *
     * @param arr    输入的数组。
     * @param left   左边界的索引。
     * @param right  右边界的索引。
     * @param k      要选择的元素的位置。
     * @return 返回第k小的元素的值。
     */
    private static int Select(int[] arr, int left, int right, int k)
    {
        if (left == right) 
            return arr[left];

        int pivotIndex = Partition(arr, left, right); 
        if (k == pivotIndex) // 如果找到了第k小的元素，则直接返回
            return arr[k];
        else if (k < pivotIndex) // 如果k小于枢轴的位置，则在左侧子数组中继续查找
            return Select(arr, left, pivotIndex - 1, k);
        else // 如果k大于枢轴的位置，则在右侧子数组中继续查找
            return Select(arr, pivotIndex + 1, right, k);
    }

    /**
     * 使用中位数的中位数作为枢轴进行分区。
     *
     * @param arr    输入的数组。
     * @param left   左边界的索引。
     * @param right  右边界的索引。
     * @return 返回枢轴的新位置。
     */
    private static int Partition(int[] arr, int left, int right)
    {
        int pivotIndex = MedianOfMedians(arr, left, right); // 找到中位数的中位数作为枢轴
        int pivotValue = arr[pivotIndex]; 
        Swap(ref arr[pivotIndex], ref arr[right]); // 将枢轴移动到数组的末尾

        int storeIndex = left; // 存储小于枢轴的元素的位置
        for (int i = left; i < right; i++)
        {
            if (arr[i] < pivotValue) // 如果当前元素小于枢轴，则交换位置
            {
                Swap(ref arr[i], ref arr[storeIndex]);
                storeIndex++; // 更新存储位置
            }
        }
        Swap(ref arr[right], ref arr[storeIndex]); 
        return storeIndex; 
    }

    /**
     * BFPRT算法寻找中位数的中位数。
     *
     * @param arr    输入的数组。
     * @param left   左边界的索引。
     * @param right  右边界的索引。
     * @return 返回中位数的中位数的索引。
     */
    private static int MedianOfMedians(int[] arr, int left, int right)
    {
        List<int> medians = new List<int>(); 
        for (int i = left; i <= right; i += 5) 
        {
            int subArrayRight = Math.Min(i + 4, right); 
            int medianIndex = MedianIndex(arr, i, subArrayRight); 
            medians.Add(medianIndex); 
        }

        if (medians.Count == 1) 
            return medians[0];

        // 递归调用Select来找到中位数的中位数索引
        return Select(medians.ToArray(), 0, medians.Count - 1, medians.Count / 2);
    }

    /**
     * 寻找子数组的中位数索引。
     *
     * @param arr    输入的数组。
     * @param left   左边界的索引。
     * @param right  右边界的索引。
     * @return 返回中间位置的索引。
     */
    private static int MedianIndex(int[] arr, int left, int right)
    {
        Array.Sort(arr, left, right - left + 1); // 排序子数组
        return (left + right) / 2; // 返回中间位置的索引
    }

    /**
     * 交换数组中的两个元素。
     *
     * @param a 第一个元素。
     * @param b 第二个元素。
     */
    private static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }
}


public class LeaderboardSystemTests
{

    static public void TestGetTopScores()
    {
        // 
        int[] scores = { 1, 9, 2, 8, 3, 6, 4, 3, 7, 3, 3, 1 };
        int m = 5;

        // 测试用例
        List<Tuple<int[], int, int[]>> testCases = new List<Tuple<int[], int, int[]>>()
        {
            Tuple.Create(new int[] { 1, 9, 2, 8, 3, 6, 4, 3, 7, 3, 3, 1 }, 5, new int[] { 9, 8, 7, 6, 4 }),
            Tuple.Create(new int[] { 10, 20, 30, 40, 50, 60 }, 3, new int[] { 60, 50, 40 }),
            Tuple.Create(new int[] { 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 }, 4, new int[] { 100, 90, 80, 70 }),
            Tuple.Create(new int[] { 5, 4, 3, 2, 1 }, 2, new int[] { 5, 4 }),
            Tuple.Create(new int[] { 1, 2, 3, 4, 5 }, 5, new int[] { 5, 4, 3, 2, 1 }),
            Tuple.Create(new int[] { }, 0, new int[] { }),
            Tuple.Create(new int[] { 5 }, 1, new int[] { 5 }),
            Tuple.Create(new int[] { 3,5,6,5,4,1,2,4,6 }, 20, new int[] { 6,6,5,5,4,4,3,2,1 })
        };

        bool allTestsPassed = true;

        foreach (var testCase in testCases)
        {
            var result = LeaderboardSystem.GetTopScores(testCase.Item1, testCase.Item2);
            if (!result.SequenceEqual(testCase.Item3))
            {
                Console.WriteLine($"Test Failed for input: [{string.Join(", ", testCase.Item1)}] with m={testCase.Item2}");
                Console.WriteLine($"Expected: [{string.Join(", ", testCase.Item3)}]");
                Console.WriteLine($"Got: [{string.Join(", ", result)}]");
                allTestsPassed = false;
            }
        }

        if (allTestsPassed)
        {
            Console.WriteLine("All tests passed successfully.");
        }
    } 


}
