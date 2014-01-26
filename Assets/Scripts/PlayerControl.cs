using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{

	public float maxSpeed = 3f;
	public float forwardAccel = 10f;
	public float backwardAccel = 20f;

	private float _speed = 0f; 

	bool facingRight = true;
	public bool grounded
	{
		get { return contacts.Count > 0; }
	}

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

	private List<Collider2D> contacts = new List<Collider2D>();

	void OnCollisionEnter2D(Collision2D col)
	{
		foreach(ContactPoint2D cp in col.contacts)
		{
			if(cp.normal.y > 0.1f)
			{
				if(!contacts.Contains(cp.collider))
				{
					contacts.Add(cp.collider);
				}
			}
		}
	}
	
	void OnCollisionStay2D(Collision2D col)
	{
		foreach(ContactPoint2D cp in col.contacts)
		{
			if(cp.normal.y <= 0.1f)
			{
				if(contacts.Contains(cp.collider))
				{
					contacts.Remove(cp.collider);
				}
			}
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		foreach(ContactPoint2D cp in col.contacts)
		{
			if(contacts.Contains(cp.collider))
			{
				contacts.Remove(cp.collider);
			}
		}
	}

	void Flip()
	{
		Vector2 newScale = gameObject.transform.localScale;

		facingRight = !facingRight;
		
		newScale.x *= -1;
		
		gameObject.transform.localScale = newScale;
	}

}
