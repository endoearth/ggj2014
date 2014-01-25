using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject player;

	private Vector2 targetPosition;

	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{

	}
	
	// Update is called once per frame
	void Update () {
		
		if(player)
		{
			targetPosition.x = Merge(gameObject.transform.position.x, player.transform.position.x);
			targetPosition.y = Merge(gameObject.transform.position.y,  player.transform.position.y);
		}

		gameObject.transform.position = targetPosition;
	}

	float Merge(float numA =0 , float numB =0, float factor_ = 0.3f)
	{
		return  numA + ((numB - numA) * factor_) ;
	}
}
