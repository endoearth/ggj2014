using UnityEngine;
using System.Collections;

public class SetDefaultPerspective : MonoBehaviour
{

	IEnumerator Start()
	{
		yield return null;

		ShiftObject.ShiftAllTo(Perspective.Pessimistic);
	}
}
