using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public float maxSpeed = 3f;
	public float forwardAccel = 10f;
	public float backwardAccel = 20f;

	private float _speed = 0f; 

	bool facingRight = true;

	public Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		float forwardAmt = 0f;
		bool jump = false;

		anim.SetFloat("xSpeed", Mathf.Abs(rigidbody2D.velocity.x));

		float xVelocity =  rigidbody2D.velocity.x;

		//bool fliped = false;

		if(xVelocity < 0 && facingRight)
		{
			Flip();
		} else if (xVelocity > 0 && !facingRight){
			Flip();
		}


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

	void Flip()
	{
		Vector2 newScale = gameObject.transform.localScale;

		facingRight = !facingRight;
		
		newScale.x *= -1;
		
		gameObject.transform.localScale = newScale;
	}

}
