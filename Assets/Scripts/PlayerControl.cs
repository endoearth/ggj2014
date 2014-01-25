using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public float maxSpeed = 3f;
	public float forwardAccel = 10f;
	public float backwardAccel = 20f;

	private float _speed = 0f; 




	void Update()
	{
		float forwardAmt = 0f;
		bool jump = false;

		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) )
		{
			forwardAmt -= 1f;
		}
		if(Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			forwardAmt += 1f;
		}
		if(Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
		{
			jump = true;
		}

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			ShiftObject.ShiftAllTo(Perspective.Default);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			ShiftObject.ShiftAllTo(Perspective.Pessimistic);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			ShiftObject.ShiftAllTo(Perspective.Optimistic);
		}


		if(Mathf.Sign(_speed)==Mathf.Sign(forwardAmt))
		{
			_speed = Mathf.MoveTowards(_speed,forwardAmt*maxSpeed,Time.deltaTime * forwardAccel);
		}
		else
		{
			_speed = Mathf.MoveTowards(_speed,forwardAmt*maxSpeed,Time.deltaTime * backwardAccel);
		}


		Vector2 movement = new Vector2(_speed,rigidbody2D.velocity.y);

		if(jump)
		{
			movement.y = 7f;
		}

		rigidbody2D.velocity = movement;
	}

}
