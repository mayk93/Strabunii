using UnityEngine;
using System.Collections;

/* 
 * Script Requirements
 * This script will ensure the object it is attached to has a mesh filter,renderer and collider 
 */

[ExecuteInEditMode]
[RequireComponent (typeof(MeshFilter))] 
[RequireComponent (typeof(MeshRenderer))] 
[RequireComponent (typeof(MeshCollider))] 
public class TileMap : MonoBehaviour 
{
	#region Public Variables
	/* 
	 * The height and width of the map, IN TILES.
	 * mapHeight tells Unity the map will be X tiles high, it does not refer to Unity units.
	 * A tile will be one Unity unit in size.
	 */

	public int mapHeight = 100;
	public int mapWidth = 50;
	public float tileSize = 1.0f;
	#endregion

	// Use this for initialization
	void Start () 
	{
		BuildMapMesh ();
	}

	/* This method creates our game map */
	public void BuildMapMesh()
	{
		#region Variables
		int numberOfTiles = mapHeight * mapWidth;
		int numberOfTriangles = numberOfTiles * 2;
		
		int numberOfVerticalVertices = mapHeight + 1;
		int numberOfHorizontalVertices = mapWidth + 1;
		int numberOfVertices = numberOfVerticalVertices * numberOfHorizontalVertices;
		#endregion

		/*
		 * Mesh data generation
		 * We generate the mesh data - the description of the mesh.
		 * We describe the shape using points and triangles that are formed by connecting the points 
	    */

		#region Mesh data initialization
		Vector3[] vertices = new Vector3[numberOfVertices];
		Vector3[] normals = new Vector3[numberOfVertices];
		Vector2[] uv = new Vector2[numberOfVertices]; // Used for bit-map application
		int[] triangles = new int[ numberOfTriangles * 3 ];
		#endregion

		#region Vertices generation
		for(int horizontalIndex=0; horizontalIndex < numberOfHorizontalVertices; horizontalIndex++) 
		{
			for(int verticalIndex=0; verticalIndex < numberOfVerticalVertices; verticalIndex++) 
			{
				int currentIndex = horizontalIndex * numberOfVerticalVertices + verticalIndex;
				vertices[ currentIndex ] = new Vector3( verticalIndex*tileSize, Random.Range(-0.5f,0.5f), horizontalIndex*tileSize );
				normals[ currentIndex ] = Vector3.up;
				uv[ currentIndex ] = new Vector2( (float)verticalIndex / numberOfVerticalVertices, (float)horizontalIndex / numberOfHorizontalVertices );
			}
		}
		#endregion

		#region Triangles generation
		for(int horizontalIndex=0; horizontalIndex < mapWidth; horizontalIndex++) {
			for(int verticalIndex=0; verticalIndex < mapHeight; verticalIndex++) {
				int tileIndex = horizontalIndex * mapHeight + verticalIndex;
				int triangleOffset = tileIndex * 6;
				triangles[triangleOffset + 0] = horizontalIndex * numberOfVerticalVertices + verticalIndex + 		   0;
				triangles[triangleOffset + 1] = horizontalIndex * numberOfVerticalVertices + verticalIndex + numberOfVerticalVertices + 0;
				triangles[triangleOffset + 2] = horizontalIndex * numberOfVerticalVertices + verticalIndex + numberOfVerticalVertices + 1;
				
				triangles[triangleOffset + 3] = horizontalIndex * numberOfVerticalVertices + verticalIndex + 		   0;
				triangles[triangleOffset + 4] = horizontalIndex * numberOfVerticalVertices + verticalIndex + numberOfVerticalVertices + 1;
				triangles[triangleOffset + 5] = horizontalIndex * numberOfVerticalVertices + verticalIndex + 		   1;
			}
		}
		#endregion

		/* 
		 * Mesh population
		 * We create a mesh for the map, the shape.
		 * We populate it with the data ( vertices, points and normals ) from above
		 */

		#region Mesh population
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		/* We assign our mesh to our filter/renderer/collider */
		MeshFilter mesh_filter = GetComponent<MeshFilter>();
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		MeshCollider mesh_collider = GetComponent<MeshCollider>();
		
		mesh_filter.mesh = mesh;
		mesh_collider.sharedMesh = mesh;
		#endregion
	}
}
