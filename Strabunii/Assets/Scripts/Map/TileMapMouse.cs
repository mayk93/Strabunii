using UnityEngine;
using System.Collections;

/* This script will handle mouseing over the map */
[RequireComponent(typeof(TileMap))]
public class TileMapMouse : MonoBehaviour 
{
	private Collider collider;
	private Renderer renderer;
	private TileMap tileMap;
	private Vector3 currentTileCoordinate;

	public GameObject Selection;

	void Start() 
	{
		tileMap = GetComponent<TileMap> ();
		collider = GetComponent<Collider>();
		renderer = GetComponent<Renderer> ();
	}
	// Update is called once per frame
	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		if (collider.Raycast (ray, out hitInfo, Mathf.Infinity)) 
		{
			/* 
			 * Here, x and z refer to the position of the mouse on the map.
			 * We substract the maps position to ensure everything is relative to it.
			 * We devide by the tileSize in order to account for the size of the tiles.
			 */
			int x = Mathf.FloorToInt(hitInfo.point.x - transform.position.x / tileMap.tileSize); 
			int z = Mathf.FloorToInt(hitInfo.point.z - transform.position.z / tileMap.tileSize);

			currentTileCoordinate.x = x;
			currentTileCoordinate.z = z;

			Selection.transform.position = currentTileCoordinate*tileMap.tileSize;
		} 
		else 
		{
			/* Here we hide the selection */
		}

		if( Input.GetMouseButton(0) )
		{
			print ("Move unit here.");
		}
	}
}
