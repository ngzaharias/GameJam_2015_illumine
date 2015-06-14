using UnityEngine;
using System.Collections;

public class DeveloperTools : MonoBehaviour 
{

	void Awake()
	{
	
	}

	void Start () 
	{
	
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.KeypadMinus))
		{
			Time.timeScale = Time.timeScale * 0.5f;
		}
		if (Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			Time.timeScale = Time.timeScale * 2.0f;
		}
	}
}
