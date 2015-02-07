using UnityEngine;
using System.Collections;

public class Polyball : MonoBehaviour {

	public PolyballPointsManager polyball_points;

	Color32 GetFaceColor(int aFace) {
		//Color32 color = new Color32( (byte)(Random.Range(0,255)), (byte)(Random.Range(0,255)), (byte)(Random.Range(0,255)), 255 );


		HSBColor baseColor = new HSBColor (new Color (243.0f/255.0f, 225/255.0f, 93.0f/255.0f));

		int numSteps = 2;
		baseColor.h += Random.Range (0, numSteps) / (float)(numSteps);// + Random.Range (-0.05f,0.05f);
		baseColor.s = Random.Range(0,3)==0 ? Random.Range (0.2f, 0.4f) : Random.Range (0.75f, 0.9f);
		baseColor.b = Random.Range(0,5)==0 ? Random.Range (0.2f, 0.4f) : Random.Range (0.8f, 1.0f);

		Color32 finalColor = baseColor.ToColor();
		return finalColor;
	}

	void CreateMesh() {
		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;


		const int numFaces = 16;
		const int numVertsPerFace = 3;
		const int numVerts = numFaces * numVertsPerFace;

		Vector3[] vertsPos = new Vector3[numVerts];
		Vector2[] vertsUV = new Vector2[numVerts];
		int[] vertsIndices = new int[numVerts];
		Color32[] vertsColor = new Color32[numVerts];

		for (int i = 0; i < numFaces; i++) {

			int indexOffset = i * numVertsPerFace;

			Color32 color = GetFaceColor(i);

			Vector3 pos0 = polyball_points.GetRandomSpherePos();
			Vector3 pos1 = polyball_points.GetRandomSpherePos();
			Vector3 pos2 = polyball_points.GetRandomSpherePos();;//-(pos0+pos1)*0.5f; pos2.Normalize(); pos2 *= pos1.magnitude;
			//pos2 = GetRandomSpherePos();

			vertsPos[0+indexOffset] = pos0;
			vertsPos[1+indexOffset] = pos1;
			vertsPos[2+indexOffset] = pos2;

			vertsColor[0+indexOffset] = color;
			vertsColor[1+indexOffset] = color;
			vertsColor[2+indexOffset] = color;

			float randNum0 = Random.Range(0.0f,1.0f);
			float randNum1 = Random.Range(0.0f,1.0f);
			vertsUV[0+indexOffset] = new Vector2( randNum0, randNum1 );
			vertsUV[1+indexOffset] = new Vector2( randNum0, randNum1 );
			vertsUV[2+indexOffset] = new Vector2( randNum0, randNum1 );

			vertsIndices[0+indexOffset] = indexOffset+0;
			vertsIndices[1+indexOffset] = indexOffset+1;
			vertsIndices[2+indexOffset] = indexOffset+2;
		}

		mesh.vertices = vertsPos;
		mesh.uv = vertsUV;
		mesh.triangles = vertsIndices;
		mesh.colors32 = vertsColor;

		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();	
	}

	void Update() {
		//transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one, 0.1f);
	}

	public void OnClick() {
		StartCoroutine(Pulsate());
	}

	IEnumerator Pulsate() {

		iTween.Stop (gameObject);
		yield return new WaitForSeconds(0.01f);

		transform.localScale = Vector3.one * 1.05f;


		iTween.ScaleTo (gameObject, iTween.Hash ("x",1.0f, 
		                                         "y",1.0f, 
		                                         "z",1.0f,
		                                         "time",1.1f, 
		                                         "easetype", iTween.EaseType.easeOutElastic));

		//iTween.PunchScale (gameObject, Vector3.one * 1.3f, 1.1f);
	}

	// Use this for initialization
	void OnEnable () {
		CreateMesh ();
	}

}
