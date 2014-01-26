using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public GameObject prefab;
	public float interval = 3f;

	IEnumerator Start ()
	{
		while(true)
		{
			Instantiate (prefab,transform.position,transform.rotation);
			yield return new WaitForSeconds(interval);
		}
	}

}
