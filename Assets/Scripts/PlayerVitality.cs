using UnityEngine;
using System.Collections;

public class PlayerVitality : MonoBehaviour
{

	public void Die()
	{
		GetComponent<PlayerControl>().enabled = false;
		collider2D.isTrigger = true;
		rigidbody2D.velocity = new Vector2(-1.5f,12f);
		rigidbody2D.fixedAngle = false;
		rigidbody2D.angularVelocity = 100f;

		StartCoroutine(AnimateDeath());
	}


	private IEnumerator AnimateDeath()
	{
		yield return new WaitForSeconds(0.65f);

		rigidbody2D.isKinematic = true;

		ShiftCamera.main.DoDeathAnimation();

		yield return new WaitForSeconds(1f);

		Application.LoadLevel(Application.loadedLevel);
	}




}
