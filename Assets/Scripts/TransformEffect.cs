using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Transform Effect")]
public class TransformEffect : ImageEffectBase
{

	public float    radius = 0f;
	public RenderTexture oldImage = null;
	
	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		material.SetFloat("_Radius", radius);
		material.SetTexture("_OldTex", oldImage);
		Graphics.Blit (source, destination, material);
	}

}
