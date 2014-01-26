using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShiftSize : ShiftObject
{
	[System.Serializable]
	public class SwappableSize : ShiftObject.SwappableObject
	{

		public Vector3 size;
		
		public override void Swap(bool on, ShiftObject shifter)
		{
			if(on)
			{
				((ShiftSize)shifter).ScaleTo(new Vector3(((ShiftSize)shifter).defaultSize.x * size.x,((ShiftSize)shifter).defaultSize.y,1f));
			}
		}
	}


	protected override void Start()
	{
		_defaultSize = scaleObject.transform.localScale;
	}

	public SwappableSize defaultSwap;
	public Transform scaleObject;
	public List<SwappableSize> swaps = new List<SwappableSize>();

	private Vector3 _defaultSize = Vector3.one;

	public Vector3 defaultSize
	{
		get { return _defaultSize; }
	}

	protected override SwappableObject _defaultSwap
	{
		get { return defaultSwap; }
	}


	protected override void CopyToSwaps()
	{
		foreach(SwappableSize swap in swaps)
		{
			_swaps.Add(swap);
		}
	}

	private void ScaleTo(Vector3 size)
	{
		StartCoroutine(_ScaleTo(size));
	}

	private IEnumerator _ScaleTo(Vector3 size)
	{
		float t = 0f;
		while(t<1f)
		{
			t = Mathf.MoveTowards(t,1f,Time.deltaTime*2f);
			scaleObject.transform.localScale = Vector3.Lerp(scaleObject.transform.localScale,size,t);
			yield return null;
		}
	}


}
