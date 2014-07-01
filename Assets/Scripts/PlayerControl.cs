using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public float maxSpeed = 3f;
	public float forwardAccel = 10f;
	public float backwardAccel = 20f;

	private float _speed = 0f; 
	private bool facingRight = true;
	
	private int _groundedCount = 0;

	public static PlayerControl main;


	private Transform onGroundCheck;
	public LayerMask floorType;

	private bool jumping;
	public float globalZoom;

	public CameraMovement cameraController;

	/*public bool grounded
	{
		get { return _groundedCount > 0; }
	}*/

	private bool grounded;

	public Animator anim;

	void Awake()
	{
		main = this;
		anim = GetComponent<Animator>();
		onGroundCheck = GetComponent<Transform>();
	}


	public void setGlobalZoom(float value_)
	{
		globalZoom =  facingRight ? value_ : value_*-1;

		Vector2 myScale = transform.localScale;

		myScale.x = globalZoom;
		myScale.y = value_;

		transform.localScale = myScale;
	}

	void Update()
	{
		float forwardAmt = 0f;
		bool jump = false;

		//Debug.Log ( transform.localScale);

		anim.SetFloat("xSpeed", Mathf.Abs(rigidbody2D.velocity.x));
		anim.SetFloat("ySpeed", Mathf.Abs(rigidbody2D.velocity.y));

		grounded = Physics2D.OverlapCircle(onGroundCheck.position, 0.2f, this.floorType);
		Collider2D[] floorArray =  Physics2D.OverlapAreaAll(new Vector2 (transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y + 0.2f) );
		
		foreach( Collider2D myCollider in floorArray)
		{
			if(myCollider.tag == "Floor")// Debug.LogWarning("Floro Touching");
			{
				grounded = true;
				jumping = true;
			}
		}

		if(grounded && jumping) jumping = false;

		anim.SetBool("onGround", grounded);

		float xVelocity = rigidbody2D.velocity.x;

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
		if(Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			jump = true;
		}

		if(forwardAmt==0f)
		{
			forwardAmt += Input.GetAxis("Horizontal");
		}
		
		if(Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}


		if(jump) 
		{
			jumping = true;

			jump = false;
			Vector2 _velocity = rigidbody2D.velocity;

			//_velocity.y = 7f;
			rigidbody2D.velocity = _velocity;
			
			StartCoroutine(TryJump(10));
		}

		anim.SetBool("jumping", jumping);
		
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
		//	ShiftObject.ShiftAllTo(Perspective.Default);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
		//	ShiftObject.ShiftAllTo(Perspective.Pessimistic);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
		//	ShiftObject.ShiftAllTo(Perspective.Optimistic);
		}

		if(Input.GetButtonDown("change") || Input.GetKeyDown(KeyCode.X))
		{

			if(ShiftObject.currentPerspective == Perspective.Optimistic)
			{
				cameraController.worldSwitch(false);
				ShiftObject.ShiftAllTo(Perspective.Pessimistic);
			} else if(ShiftObject.currentPerspective == Perspective.Pessimistic)
			{
				cameraController.worldSwitch(true);
				ShiftObject.ShiftAllTo(Perspective.Optimistic);
			} else {
				ShiftObject.ShiftAllTo(Perspective.Pessimistic);
			}
		}


		float accel = forwardAccel;
		
		if(!grounded)
		{
			accel = forwardAccel * 0.4f;
		}
		else if(Mathf.Sign(_speed)==Mathf.Sign(forwardAmt))
		{
			accel = forwardAccel;
		}
		else
		{
			accel = backwardAccel;
		}


		_speed = rigidbody2D.velocity.x;

		//if(grounded || forwardAmt!=0f)
		{
			_speed = Mathf.MoveTowards(_speed,forwardAmt*maxSpeed,Time.deltaTime * accel);
		}


		Vector2 movement = new Vector2(_speed,rigidbody2D.velocity.y);

		rigidbody2D.velocity = movement;

		/*if(jump)
		{
			StartCoroutine(TryJump(10));
		}*/
	}

	private IEnumerator TryJump(int frames)
	{
		while(frames > 0)
		{
			if(grounded)
			{
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 8f);
				yield break;
			}
			frames--;
			yield return null;
		}
	}
	/*
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
	}*/

	void Flip()
	{

		Vector2 newScale = gameObject.transform.localScale;

		facingRight = !facingRight;
		
		newScale.x = globalZoom * -1;
		//newScale.y = globalZoom;
		
		gameObject.transform.localScale = newScale;
	}

}
