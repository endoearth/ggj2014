using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour
{

	public Triggerable triggerable = null;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag=="Player")
		{
			if(triggerable!=null)
			{
				triggerable.Trigger();
			}
		}
	}


}
