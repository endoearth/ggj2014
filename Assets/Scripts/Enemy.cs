using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float speed = 2f;
	public bool startLeft = false;

	public float maxDist = 5f;


	private bool _goingRight = true;

	private bool _changeThisFrame = false;

	private float _origPos = 0f;

	void Awake()
	{
		if(startLeft)
		{
			_goingRight = false;
		}

		_origPos = transform.position.x;
	}


	void Update()
	{
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)<0.1f)
			ChangeDirection();

		if(_goingRight)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.right*speed + Vector2.up*GetComponent<Rigidbody2D>().velocity.y;
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = -Vector2.right*speed + Vector2.up*GetComponent<Rigidbody2D>().velocity.y;
		}
	}

	void Enable()
	{
		enabled = true;
		_origPos = transform.position.x;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		for(int i=0;i<col.contacts.Length;i++)
		{
			Vector2 norm = col.contacts[i].normal;

			Vector2 point = col.contacts[i].point;
			Vector2 pos = transform.position;
			Vector2 dif = point - pos;
			
			if(Mathf.Abs (norm.x) > 0.1f)
			{
				if(col.gameObject.tag!="Player")
				{
					ChangeDirection();
				}
			}

			if(Mathf.Abs(dif.x) > dif.y+0.1f)
			{
				if(col.gameObject.tag=="Player")
				{
					if(ShiftObject.currentPerspective==Perspective.Pessimistic)
					{
						//col.gameObject.SendMessage("Die");
						col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(dif.x)*12f,2f);
					}
					else
					{
						enabled = false;
						CancelInvoke("Enable");
						Invoke("Enable",3f);
						GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Sign(dif.x)*12f,2f);
					}
			}
			}
			else 
			{
				if(col.collider.tag=="Player")
				{
					if(ShiftObject.currentPerspective==Perspective.Optimistic)
					{
						//Invoke ("Die",0.02f);

						//col.collider.rigidbody2D.velocity += Vector2.up * 5f;
					}
					else
					{

					}
				}
			}
		}
	}

	void LateUpdate()
	{
		if(!_changeThisFrame && Mathf.Abs(_origPos-transform.position.x) >= maxDist)
		{
			if(transform.position.x-_origPos >= maxDist && _goingRight)
				ChangeDirection ();
			else if(transform.position.x-_origPos <= -maxDist && !_goingRight)
				ChangeDirection();
		}

		_changeThisFrame = false;
	}

	void ChangeDirection()
	{
		if(_changeThisFrame)
		{
			return;
		}

		_changeThisFrame = true;
		_goingRight = !_goingRight;

		if(_goingRight)
		{
			transform.localScale = new Vector3(1,1,1);
		}
		else
		{
			transform.localScale = new Vector3(-1,1,1);
		}

		Update ();
		transform.position += GetComponent<Rigidbody2D>().velocity.x * Vector3.right * Time.deltaTime;
	}

	void Die()
    {
        GetComponent<Collider2D>().enabled = false;
		foreach(Collider2D col in GetComponentsInChildren<Collider2D>())
		{
			col.enabled = false;
		}
		GetComponent<Rigidbody2D>().fixedAngle = false;
		GetComponent<Rigidbody2D>().AddTorque(60f/Time.deltaTime);
		Destroy (gameObject,3f);
	}

}
