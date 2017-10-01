using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public static class Extensions
{
	public static void Shuffle<T>(this IList<T> list)
	{
		Random rng = new Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public static void GizmosDrawRect(Vector3 center, float width, float height)
	{
		var topLeft = center + new Vector3(-width / 2f, 0, -height / 2f);
		var topRight = center + new Vector3(width / 2f, 0, -height / 2f);
		var bottomRight = center + new Vector3(width / 2f, 0, height / 2f);
		var bottomLeft = center + new Vector3(-width / 2f, 0, height / 2f);

		Gizmos.DrawLine(topLeft, topRight);
		Gizmos.DrawLine(topRight, bottomRight);
		Gizmos.DrawLine(bottomRight, bottomLeft);
		Gizmos.DrawLine(bottomLeft, topLeft);
	}
}
