using UnityEngine;
using System.Collections;

public class Polyball : MonoBehaviour {

	private Vector3[] vertsPos;
	private Vector2[] vertsUV;
	private int[] vertIndices;
	private Color32[] vertColors;

	void CreateMesh() {
		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;

		const int numVerts = 3;
		vertsPos = new Vector3[numVerts];
		vertsUV = new Vector2[numVerts];
		vertIndices = new int[numVerts];
		vertColors = new Color32[numVerts];

		vertsPos[0] = new Vector3 (0.0f, 0.0f, 0.0f);
		vertsPos[1] = new Vector3 (2.0f, 0.0f, 0.0f);
		vertsPos[2] = new Vector3 (-2.0f, 5.0f, 3.0f);

		vertsUV[0] = new Vector2 (0.0f, 0.0f);
		vertsUV[1] = new Vector2 (2.0f, 1.0f);
		vertsUV[2] = new Vector2 (-2.0f, 5.0f);

		vertColors [0] = new Color32 (255, 0, 0, 255);
		vertColors [1] = new Color32 (255, 255, 0, 255);
		vertColors [2] = new Color32 (255, 0, 255, 255);

		vertIndices[0] = 0;
		vertIndices[1] = 2;
		vertIndices[2] = 1;

		mesh.vertices = vertsPos;
		mesh.uv = vertsUV;
		mesh.triangles = vertIndices;
		mesh.colors32 = vertColors;

		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();

		//renderer.material = new Material (Shader.Find ("Diffuse"));
	
	}

	// Use this for initialization
	void Start () {
		CreateMesh ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
