using System;
using System.Collections.Generic;
using System.Linq;

public static class RandomSelection
{
    // 指定のリストからランダムで要素を取得する
    public static List<T> GetRandomElements<T>(List<T> source, int count, bool canDuplication = false)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (count < 0 || count > source.Count)
            throw new ArgumentOutOfRangeException(nameof(count), "The list does not have enough elements.");

        var random = new System.Random();
        var result = new List<T>();
        var tempList = new List<T>(source);

        for (int i = 0; i < count; i++)
        {
            int index = random.Next(tempList.Count);
            result.Add(tempList[index]);
            if (!canDuplication)
            {
                tempList.RemoveAt(index);
            }
        }

        return result;
    }
}