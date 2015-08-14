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

	public static ShiftCamera main = null;

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
	
	protected override SwappableObject _defaultSwap
	{
		get { return defaultSwap; }
	}


	protected override void Start()
	{
		main = this;

		base.Start();
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
			TransformEffect trEffect = next.gameObject.GetComponent<TransformEffect>();
            if(trEffect == null)
            {
                trEffect = next.gameObject.AddComponent<TransformEffect>();
            }
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

	public void DoDeathAnimation()
	{
		if(ShiftObject.currentPerspective==Perspective.Optimistic)
		{
			StartCoroutine(DoOptimisticAnimation());
		}
		if(ShiftObject.currentPerspective==Perspective.Pessimistic)
		{
			StartCoroutine(DoPessimisticAnimation());
		}
	}

	public IEnumerator DoOptimisticAnimation()
	{
		float t = 0f;

		while(t<1f)
		{
			t = Mathf.MoveTowards(t,1f,Time.deltaTime);

			_currentCam.camera.gameObject.GetComponent<Bloom>().bloomIntensity = Mathf.Lerp(1f,10f,t);
			_currentCam.camera.gameObject.GetComponent<Bloom>().bloomThreshhold = Mathf.Lerp(0.5f,0f,t);

			yield return null;
		}
	}
	
	public IEnumerator DoPessimisticAnimation()
	{
		float t = 0f;
		
		while(t<1f)
		{
			t = Mathf.MoveTowards(t,1f,Time.deltaTime);
			
			_currentCam.camera.gameObject.GetComponent<NoiseEffect>().grainIntensityMin = Mathf.Lerp(0.1f,5f,t);
			_currentCam.camera.gameObject.GetComponent<NoiseEffect>().grainIntensityMax = Mathf.Lerp(0.2f,5f,t);
			
			yield return null;
		}
	}

}
