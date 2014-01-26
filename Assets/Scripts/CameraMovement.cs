using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject player;

	private Vector2 targetPosition;

	//bool worldChange = false;

	float halfSize;

	Vector2 playerTargetSize;

	private bool _worldSwitch = true;

	float originalCameraSize;

	float playerOriginalSize;

	public void worldSwitch(bool value_)
	{
		_worldSwitch = value_;
	}
	// Use this for initialization
	void Start () 
	{
	
	}

	void Awake()
	{
		halfSize = GetComponentInChildren<Camera>().orthographicSize * 0.8f;
		originalCameraSize = GetComponentInChildren<Camera>().orthographicSize;

		playerTargetSize.x = player.transform.localScale.x;// * 0.2f;
		playerTargetSize.y = player.transform.localScale.y;// * 0.2f;

		// holds a reference to the player original scale value
		playerOriginalSize = player.transform.localScale.x;

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
			float gradual = Mathf.MoveTowards(currentSize, halfSize, Time.deltaTime * speed);
			
			cams[i].orthographicSize = gradual;
			
			Vector2 playerGradual = new Vector2();
			float perZoom =  gradual / originalCameraSize;
			
			playerGradual.x = (playerTargetSize.x * perZoom);
			playerGradual.y = (playerTargetSize.y * perZoom);
			//playerGradual.x = Mathf.MoveTowards(playerCurrent.x, playerTargetSize.x, Time.deltaTime);
			//playerGradual.y = Mathf.MoveTowards(playerCurrent.y, playerTargetSize.y, Time.deltaTime);
			
			//Debug.Log (playerGradual);
			PlayerControl playerControl = player.GetComponent<PlayerControl>();
			playerControl.setGlobalZoom(playerGradual.x);

			//Debug.LogWarning(playerControl.globalZoom);
		}
	}

	void zoomOut ()
	{
		Camera[] cams = GetComponentsInChildren<Camera>(true);

		for(int i =0; i < cams.Length; i++)
		{
			float currentSize = cams[i].orthographicSize;
			
			//newScale *= 0.5f;
			float speed = 4f;
			
			//c.orthographicSize = newScale;
			float gradual = Mathf.MoveTowards(currentSize, originalCameraSize, Time.deltaTime * speed);
			
			cams[i].orthographicSize = gradual;
			
			Vector2 playerGradual = new Vector2();
			float perZoom =  gradual / (originalCameraSize);
			
			playerGradual.x = (playerOriginalSize * perZoom);
			playerGradual.y = (playerOriginalSize * perZoom);

			PlayerControl playerControl = player.GetComponent<PlayerControl>();
			playerControl.setGlobalZoom(playerGradual.x);
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
			_worldSwitch = !_worldSwitch;
		}

		if(_worldSwitch)
		{
			zoomIn();
		} else {
			zoomOut();
		}

		Camera[] cams = GetComponentsInChildren<Camera>(true);

		for(int i =0; i < cams.Length ;i++)
		{
			targetPosition.x = Merge(cams[i].transform.position.x, player.transform.position.x);
			targetPosition.y = Merge(cams[i].transform.position.y,  player.transform.position.y);

			cams[i].transform.position = targetPosition;
		}
	}

	float Merge(float numA = 0 , float numB = 0, float factor_ = 0.3f)
	{
		return  numA + ((numB - numA) * factor_) ;
	}
}
