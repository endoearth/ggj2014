using UnityEngine;
using System.Collections;

public class PlayerVitality : MonoBehaviour
{

	public void Die()
	{
		GetComponent<PlayerControl>().enabled = false;
		collider2D.isTrigger = true;
		rigidbody2D.velocity = new Vector2(-1.5f,6f);

		StartCoroutine(AnimateDeath());
	}


	private IEnumerator AnimateDeath()
	{
		yield return new WaitForSeconds(0.65f);

		rigidbody2D.isKinematic = true;

		ShiftCamera.main.DoDeathAnimation();
	}




}
