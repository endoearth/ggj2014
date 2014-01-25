using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShiftGameObject : ShiftObject
{
	[System.Serializable]
	public class SwappableGameObject : ShiftObject.SwappableObject
	{
		public GameObject obj;
		
		public override void Swap(bool on, ShiftObject shifter)
		{
			if(obj!=null)
			{
				obj.SetActive(on);
			}
		}
	}

	public SwappableGameObject defaultSwap;
	public List<SwappableGameObject> swaps = new List<SwappableGameObject>();

	protected override SwappableObject _defaultSwap
	{
		get { return defaultSwap; }
	}


	protected override void CopyToSwaps()
	{
		foreach(SwappableGameObject swap in swaps)
		{
			_swaps.Add(swap);
		}
	}


}
