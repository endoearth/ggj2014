using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShiftCamera : ShiftObject
{
	[System.Serializable]
	public class SwappableCamera : ShiftObject.SwappableObject
	{
		public Camera camera;
		
		public override void Swap(bool on, ShiftObject shifter)
		{
			if(on)
			{
				shifter.StartCoroutine( ((ShiftCamera)shifter).AnimateCamSwitch(this) );
			}
		}
	}



	public SwappableCamera defaultSwap;
	public List<SwappableCamera> swaps = new List<SwappableCamera>();

	private SwappableCamera _currentCam = null;
	private bool _swapping = false;

	protected override void CopyToSwaps()
	{
		foreach(SwappableCamera swap in swaps)
		{
			_swaps.Add(swap);
		}
	}
	
	protected override void SetDefaultSwap()
	{
		SetActiveSwap(defaultSwap);
	}


	public IEnumerator AnimateCamSwitch(SwappableCamera nextSwap)
	{
		if(_swapping)
			yield break;

		if(_currentCam==nextSwap)
			yield break;

		if(nextSwap==null)
			yield break;

		Camera current = _currentCam==null ? null : _currentCam.camera;
		Camera next = nextSwap.camera;

		_swapping = true;

		if(current!=null)
		{
			current.depth = 1;
		}

		next.depth = 2;
		next.gameObject.SetActive(true);
		
		if(current!=null && next!=current)
		{
			TransformEffect trEffect = next.gameObject.AddComponent<TransformEffect>();
			trEffect.shader = Shader.Find("Custom/Transform Effect");
			RenderTexture renderTex = new RenderTexture(Screen.width,Screen.height,32);
			trEffect.oldImage = renderTex;
			current.targetTexture = renderTex;
			current.Render();

			float t = 0f;
			while(t<1.5f)
			{
				t+= Time.deltaTime*3f;

				trEffect.radius = t;

				yield return null;
			}

			Destroy (renderTex);

			current.gameObject.SetActive(false);
			current.targetTexture = null;
			Destroy (trEffect);
		}

		_currentCam = nextSwap;

		_swapping = false;
	}

}
