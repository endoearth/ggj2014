using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public float maxSpeed = 3f;
	public float forwardAccel = 10f;
	public float backwardAccel = 20f;

	private float _speed = 0f; 
	
	
	private int _groundedCount = 0;
	public bool grounded
	{
		get { return _groundedCount > 0; }
	}



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


		float accel = forwardAccel;


		if(Mathf.Sign(_speed)==Mathf.Sign(forwardAmt))
		{
			accel = forwardAccel;
		}
		else
		{
			accel = backwardAccel;
		}

		if(!grounded)
		{
			accel *= 0.25f;
		}

		_speed = rigidbody2D.velocity.x;

		//if(grounded || forwardAmt!=0f)
		{
			_speed = Mathf.MoveTowards(_speed,forwardAmt*maxSpeed,Time.deltaTime * accel);
		}


		Vector2 movement = new Vector2(_speed,rigidbody2D.velocity.y);

		rigidbody2D.velocity = movement;

		if(jump)
		{
			StartCoroutine(TryJump(10));
		}
	}

	private IEnumerator TryJump(int frames)
	{
		while(frames > 0)
		{
			if(grounded)
			{
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 7f);
				yield break;
			}
			frames--;
			yield return null;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		foreach(ContactPoint2D cp in col.contacts)
		{
			if(cp.normal.y > 0.1f)
			{
				_groundedCount++;
			}
		}
	}
	void OnCollisionExit2D(Collision2D col)
	{
		foreach(ContactPoint2D cp in col.contacts)
		{
			if(cp.normal.y > 0.1f)
			{
				_groundedCount--;
			}
		}
	}

}
