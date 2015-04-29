using UnityEngine;
using System.Collections;

/* This script will handle mouseing over the map */
public class TileMapMouse : MonoBehaviour 
{
	private Collider collider;
	private Renderer renderer;

	void Start() 
	{
		collider = GetComponent<Collider>();
		renderer = GetComponent<Renderer> ();
	}
	// Update is called once per frame
	void Update () 
	{
		Ray ray = Camera.mainCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		if (collider.Raycast (ray, out hitInfo, Mathf.Infinity)) 
		{
			renderer.material.color = Color.red;
		} 
		else 
		{
			renderer.material.color = Color.green;
		}
	}
}
