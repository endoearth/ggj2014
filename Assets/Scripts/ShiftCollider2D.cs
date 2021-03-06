﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShiftCollider2D : ShiftObject
{
	[System.Serializable]
	public class SwappableCollider2D : ShiftObject.SwappableObject
	{
		public Collider2D col;
		
		public override void Swap(bool on, ShiftObject shifter)
		{
			if(col!=null)
			{
				col.enabled = on;
			}
		}
	}



	public SwappableCollider2D defaultSwap;
	public List<SwappableCollider2D> swaps = new List<SwappableCollider2D>();

	protected override SwappableObject _defaultSwap
	{
		get { return defaultSwap; }
	}

	protected override void CopyToSwaps()
	{
		foreach(SwappableCollider2D swap in swaps)
		{
			_swaps.Add(swap);
		}
	}



}
