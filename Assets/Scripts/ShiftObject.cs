using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public abstract class ShiftObject : MonoBehaviour
{
	[System.Serializable]
	public abstract class SwappableObject
	{
		public Perspective perspective;
		
		public abstract void Swap(bool on, ShiftObject shifter);
	}


	private static Perspective _currentPerspective = Perspective.Default;

	private static List<ShiftObject> _allObjects = new List<ShiftObject>();
	
	private static float _lastTimeSwapped = -100f;


	protected List<SwappableObject> _swaps = new List<SwappableObject>();

	protected SwappableObject _currentSwap = null;

	public SwappableObject currentSwap
	{
		get { return _currentSwap; }
	}


	public static Perspective currentPerspective
	{
		get { return _currentPerspective; }
	}



	void Awake()
	{
		_allObjects.Add(this);

		CopyToSwaps();
	}

	protected virtual void Start()
	{
		SetDefaultSwap();
	}

	void OnDestroy()
	{
		_allObjects.Remove(this);
	}
	


	public static void  ShiftAllTo(Perspective perspective)
	{
		if(Time.time-_lastTimeSwapped >= 0.75f)
		{
			foreach(ShiftObject shiftObj in _allObjects)
			{
				shiftObj.OnShift(perspective);
			}

			_currentPerspective = perspective;

			_lastTimeSwapped = Time.time;
		}
	}



	protected abstract void CopyToSwaps();

	protected abstract SwappableObject _defaultSwap
	{
		get;
	}
	
	
	private void SetDefaultSwap()
	{
		SetActiveSwap(_defaultSwap);
	}



	public void OnShift(Perspective perspective)
	{
		foreach(SwappableObject swap in _swaps)
		{
			if(swap.perspective==perspective)
			{
				SetActiveSwap(swap);
				return;
			}
		}
			
		SetDefaultSwap();
	}

	protected void SetActiveSwap(SwappableObject swap)
	{

		if(swap==_currentSwap)
		{
			return;
		}
		
		if(_currentSwap!=null)
		{
			_currentSwap.Swap(false,this);
		}
		
		_currentSwap = swap;
		
		if(_currentSwap!=null)
		{
			_currentSwap.Swap(true,this);
		}
	}


}


