using UnityEngine;
using System.Collections;

public static class Util {

	public static bool CompareTags(this Component _component, params string[] _tags) {
		foreach (var tag in _tags) {
			if (_component.CompareTag(tag)) return true;
		}
		return false;
	}

	public static T Choose<T>(int index, params T[] args) {
		if (index < 1 || index > args.Length)
		{
			return default(T);
		}
		else
		{
			return args[--index];
		}
	}

    // 配列をシャッフルする
    public static void ShuffleIntArray(int[] ary) {
        System.Random rng = new System.Random();
        int n = ary.Length;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            int tmp = ary[k];
            ary[k] = ary[n];
            ary[n] = tmp;
        }
    }

}
