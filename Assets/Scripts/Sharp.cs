using UnityEngine;
using System.Collections;

public class Sharp : MonoBehaviour
{

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag=="Player")
		{
			col.gameObject.SendMessage("Die");
		}
	}

}
