using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Warp : MonoBehaviour
{

	public List<Transform> warps = new List<Transform>();

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Alpha1))
		{
			WarpTo (1);
		}
		if(Input.GetKeyDown (KeyCode.Alpha2))
		{
			WarpTo (2);
		}
		if(Input.GetKeyDown (KeyCode.Alpha3))
		{
			WarpTo (3);
		}
		if(Input.GetKeyDown (KeyCode.Alpha4))
		{
			WarpTo (4);
		}
		if(Input.GetKeyDown (KeyCode.Alpha5))
		{
			WarpTo (5);
		}
		if(Input.GetKeyDown (KeyCode.Alpha6))
		{
			WarpTo (6);
		}
		if(Input.GetKeyDown (KeyCode.Alpha7))
		{
			WarpTo (7);
		}
		if(Input.GetKeyDown (KeyCode.Alpha8))
		{
			WarpTo (8);
		}
	}

	void WarpTo(int ind)
	{
		if(warps.Count>ind-1)
		{
			PlayerControl.main.transform.position = warps[ind-1].position;
		}
	}
}
