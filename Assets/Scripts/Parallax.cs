using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	
	public bool parallax = true;
	
	public float offsetFactor;
	public float advanceAmount = 25.6f;
	public float screenOffsetY;
	
	private Camera mainCamera;
	
	private Transform cameraTransform;
	private float camTargetX;
	private float camTargetY;
	private SpriteRenderer spriteRenderer;
	
	private float initialX;
	
	private bool makeMove;
	
	private float currentCell;
	private float lastCell;
	private float moveSign;
	
	private Vector2 objectPos;
	
	// Use this for initialization
	void Start () 
	{
		mainCamera = Camera.main;
		cameraTransform = mainCamera.transform;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		initialX = 0;
		
		lastCell = mainCamera.transform.position.x / advanceAmount;
	}
	
	void FixedUpdate ()
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		camTargetX = (mainCamera.transform.position.x);
		camTargetY = (mainCamera.transform.position.y) + screenOffsetY;
		
		currentCell = Mathf.RoundToInt(transform.position.x / advanceAmount);
		
		if(currentCell != lastCell)
		{
			makeMove = true;
		}
		
		if(makeMove)
		{
			makeMove = false;
			moveSign = Mathf.Sign(lastCell - currentCell);
			lastCell = currentCell;
			
			if(moveSign < 0) 
			{
				//Debug.Log("Cell Move Right");
				initialX += advanceAmount;
			}
			if(moveSign > 0 ) 
			{
				initialX -= advanceAmount;
				//Debug.Log("Cell Move Left");
			}
		}
		
		objectPos = gameObject.transform.position;
		
		objectPos.x = (camTargetX  + (parallax ? initialX : -10) ) * offsetFactor;
		objectPos.y = (camTargetY);
		gameObject.transform.position = objectPos;
	}
}