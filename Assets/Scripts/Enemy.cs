using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float speed = 2f;
	public bool startLeft = false;


	private bool _goingRight = true;



	void Awake()
	{
		if(startLeft)
		{
			_goingRight = false;
		}
	}

	void Update()
	{
		if(_goingRight)
		{
			rigidbody2D.velocity = Vector2.right*speed + Vector2.up*rigidbody2D.velocity.y;
		}
		else
		{
			rigidbody2D.velocity = -Vector2.right*speed + Vector2.up*rigidbody2D.velocity.y;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Vector2 norm = col.contacts[0].normal;

		Vector2 point = col.contacts[0].point;
		Vector2 pos = transform.position;
		Vector2 dif = point - pos;

		if(Mathf.Abs (norm.x) > 0.1f && dif.y > Mathf.Abs(dif.x))
		{
			if(col.gameObject.tag=="Player")
			{
				col.gameObject.SendMessage("Die");
			}
			else
			{
				ChangeDirection();
				Update ();
				transform.position += rigidbody2D.velocity.x * Vector3.right * Time.deltaTime;
			}
		}
		else 
		{
			if(col.collider.tag=="Player")
			{
				if(ShiftObject.currentPerspective==Perspective.Optimistic)
				{
					Invoke ("Die",0.02f);

					col.collider.rigidbody2D.velocity += Vector2.up * 5f;
				}
			}
		}
	}



	void ChangeDirection()
	{
		_goingRight = !_goingRight;

		if(_goingRight)
		{
			transform.localScale = new Vector3(-1,1,1);
		}
		else
		{
			transform.localScale = new Vector3(1,1,1);
		}
	}

	void Die()
	{
		collider2D.enabled = false;
		rigidbody2D.fixedAngle = false;
		rigidbody2D.AddTorque(60f/Time.deltaTime);
		Destroy (gameObject,3f);
	}

}
