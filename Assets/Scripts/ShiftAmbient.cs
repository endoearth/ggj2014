using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShiftAmbient : ShiftObject
{
	[System.Serializable]
	public class SwappableAudio : ShiftObject.SwappableObject
	{
		public AudioClip clip;
		
		public override void Swap(bool on, ShiftObject shifter)
		{
			if(on)
			{
				((ShiftAmbient)shifter).audio.clip = clip;
				((ShiftAmbient)shifter).audio.Play();
			}
		}
	}

	public SwappableAudio defaultSwap;
	public List<SwappableAudio> swaps = new List<SwappableAudio>();

	protected override SwappableObject _defaultSwap
	{
		get { return defaultSwap; }
	}


	protected override void CopyToSwaps()
	{
		foreach(SwappableAudio swap in swaps)
		{
			_swaps.Add(swap);
		}
	}


}
