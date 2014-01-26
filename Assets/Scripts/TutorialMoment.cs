using UnityEngine;
using System.Collections;

public class TutorialMoment : Triggerable
{

	public Vector3 targetPosition;

	private bool _set = false;

	void Start()
	{
		enabled = false;
	}

	public override void Trigger()
	{
		enabled = true;

		Invoke ("Hide",6f);
	}

	void Update()
	{
		if(!_set)
		{
			transform.position = Vector3.MoveTowards(transform.position,targetPosition,Time.deltaTime);
			if(transform.position==targetPosition)
			{
				_set = true;
			}
		}
	}

	void Hide()
	{
		StartCoroutine(_Hide());
	}

	IEnumerator _Hide()
	{
		float t = 0f;
		while(t<1f)
		{
			transform.position -= Vector3.up*0.5f*Time.deltaTime;
			t += Time.deltaTime;
			yield return null;
		}
	}

}
