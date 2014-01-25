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
		Vector2 point = col.contacts[0].point;
		Vector2 pos = new Vector2(transform.position.x,transform.position.y);

		if(Mathf.Abs(point.x-pos.x) > Mathf.Abs(point.y-pos.y)+0.1f)
		{
			ChangeDirection();
		}
		else if(point.y > pos.y)
		{
			Die();
			col.collider.rigidbody2D.velocity += Vector2.up * 5f;
		}
	}



	void ChangeDirection()
	{
		_goingRight = !_goingRight;
	}

	void Die()
	{
		Destroy(gameObject);
	}

}
