using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShiftTime : ShiftObject
{
	[System.Serializable]
	public class SwappableTimeScale : SwappableObject
	{
		public float timeScale;
		
		public override void Swap(bool on, ShiftObject shifter)
		{
			if(on)
			{
				Time.timeScale = timeScale;
			}
		}
	}

	public SwappableTimeScale defaultSwap;
	public List<SwappableTimeScale> swaps = new List<SwappableTimeScale>();

	protected override SwappableObject _defaultSwap
	{
		get { return defaultSwap; }
	}

	protected override void CopyToSwaps()
	{
		foreach(SwappableTimeScale swap in swaps)
		{
			_swaps.Add(swap);
		}
	}


}
