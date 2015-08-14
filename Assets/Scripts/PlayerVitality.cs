using UnityEngine;
using System.Collections;

public class PlayerVitality : MonoBehaviour
{

	public void Die()
	{
		GetComponent<PlayerControl>().enabled = false;
		GetComponent<Collider2D>().isTrigger = true;
		GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f,12f);
		GetComponent<Rigidbody2D>().fixedAngle = false;
		GetComponent<Rigidbody2D>().angularVelocity = 100f;

		StartCoroutine(AnimateDeath());
	}


	private IEnumerator AnimateDeath()
	{
		yield return new WaitForSeconds(0.65f);

		GetComponent<Rigidbody2D>().isKinematic = true;

		ShiftCamera.main.DoDeathAnimation();

		yield return new WaitForSeconds(1f);

		Application.LoadLevel(Application.loadedLevel);
	}




}
