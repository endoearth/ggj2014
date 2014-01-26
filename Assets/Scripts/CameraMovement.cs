using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject player;

	private Vector2 targetPosition;

	//bool worldChange = false;

	float targetSize;

	Vector2 playerTargetSize;

	bool worldSwitch = false;

	float originalCameraSize;

	// Use this for initialization
	void Start () 
	{
	
	}

	void Awake()
	{
		targetSize = GetComponentInChildren<Camera>().orthographicSize * 0.5f;

		playerTargetSize.x = player.transform.localScale.x  * 0.5f;//* 0.2f;
		playerTargetSize.y = player.transform.localScale.y * 0.5f;// * 0.2f;

		originalCameraSize = GetComponentInChildren<Camera>().orthographicSize;


		//Debug.Log (GetComponentInChildren<Camera>() );
		/*foreach( Camera c in GetComponentsInChildren<Camera>() )
		{
			float newScale = c.orthographicSize;

			newScale *= 0.5f;

			c.orthographicSize = newScale;
		}*/
	}

	void zoomIn ()
	{
		Camera[] cams = GetComponentsInChildren<Camera>(true);

		for(int i =0; i < cams.Length ;i++)
		{
			float currentSize = cams[i].orthographicSize;
			
			//newScale *= 0.5f;
			float speed = 4f;
			
			//c.orthographicSize = newScale;
			float gradual = Mathf.MoveTowards(currentSize, targetSize, Time.deltaTime * speed);
			
			cams[i].orthographicSize = gradual;
			
			Vector2 playerGradual = new Vector2();
			float perZoom =  targetSize / gradual;
			
			playerGradual.x = 1f - (playerTargetSize.x * perZoom);
			playerGradual.y = 1f - (playerTargetSize.y * perZoom);
			//playerGradual.x = Mathf.MoveTowards(playerCurrent.x, playerTargetSize.x, Time.deltaTime);
			//playerGradual.y = Mathf.MoveTowards(playerCurrent.y, playerTargetSize.y, Time.deltaTime);
			
			//Debug.Log (playerGradual);
			
			player.transform.localScale = playerGradual;
		}
	}

	void zoomOut ()
	{
		Camera[] cams = GetComponentsInChildren<Camera>(true);

		for(int i =0; i < cams.Length ;i++)
		{
			float currentSize = cams[i].orthographicSize;
			
			//newScale *= 0.5f;
			float speed = 4f;
			
			//c.orthographicSize = newScale;
			float gradual = Mathf.MoveTowards(currentSize, originalCameraSize, Time.deltaTime * speed);
			
			cams[i].orthographicSize = gradual;
			
			Vector2 playerGradual = new Vector2();
			float perZoom =  targetSize / gradual;
			
			playerGradual.x = 1f - (playerTargetSize.x * perZoom);
			playerGradual.y = 1f - (playerTargetSize.y * perZoom);
			//playerGradual.x = Mathf.MoveTowards(playerCurrent.x, playerTargetSize.x, Time.deltaTime);
			//playerGradual.y = Mathf.MoveTowards(playerCurrent.y, playerTargetSize.y, Time.deltaTime);
			
			//Debug.Log (playerGradual);
			
			//player.transform.localScale = playerGradual;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	/*	float offsetY;

		if(worldSwitch)
		{
			offsetY = 1f;
		
		} else {
			offsetY = 2.5f;
		}*/

		if(player)
		{
			//targetPosition.x = Merge(gameObject.transform.position.x, player.transform.position.x);
			//targetPosition.y = Merge(gameObject.transform.position.y,  player.transform.position.y);
		}
		//float perZoom;
		//Vector2 playerGradual;

		if(Input.GetKeyDown(KeyCode.X))
		{
			worldSwitch = !worldSwitch;
		}

		if(worldSwitch)
		{
			zoomIn();
		} else {
			zoomOut();
		}

		//Debug.Log(perZoom);

		//Vector2 playerCurrent = player.transform.localScale * percentageZoomed;
		//playerGradual.x = Mathf.MoveTowards(playerCurrent.x, playerTargetSize.x, Time.deltaTime);
		//playerGradual.y = Mathf.MoveTowards(playerCurrent.y, playerTargetSize.y, Time.deltaTime);
		
		//foreach( Camera c in GetComponentsInChildren<Camera>() )
		Camera[] cams = GetComponentsInChildren<Camera>(true);

		for(int i =0; i < cams.Length ;i++)
		{
			targetPosition.x = Merge(cams[i].transform.position.x, player.transform.position.x);
			targetPosition.y = Merge(cams[i].transform.position.y,  player.transform.position.y);

			cams[i].transform.position = targetPosition;
			//playerGradual.x = Mathf.MoveTowards(playerCurrent.x, playerTargetSize.x, Time.deltaTime);
			//playerGradual.y = Mathf.MoveTowards(playerCurrent.y, playerTargetSize.y, Time.deltaTime);
		}
	}

	float Merge(float numA = 0 , float numB = 0, float factor_ = 0.3f)
	{
		return  numA + ((numB - numA) * factor_) ;
	}
}
