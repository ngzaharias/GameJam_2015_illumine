using UnityEngine;
using System.Collections;

public class Utility 
{
	public static Quaternion RandomQuaternion()
	{
		return Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
	}
}
