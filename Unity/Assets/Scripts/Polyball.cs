using UnityEngine;
using System.Collections;

public class Polyball : MonoBehaviour {

	private Vector3[] vertsPos;
	private Vector2[] vertsUV;
	private int[] vertsIndices;
	private Color32[] vertsColor;

	private Vector2 mousePosPrev;

	Vector3 GetRandomSpherePos()
	{
		int numTheta = 6;
		int numPhi = 4;

		float deltaTheta = 2.0f * Mathf.PI / numTheta;
		float deltaPhi = Mathf.PI / numPhi;
		float theta = Random.Range (0, numTheta) * deltaTheta;
		float phi = Random.Range (0, numPhi) * deltaPhi - Mathf.PI*0.5f;

		float radius = 1.0f;
		float x = radius * Mathf.Cos( phi ) * Mathf.Sin ( theta );
		float y = radius * Mathf.Sin( phi ) * Mathf.Sin ( theta );
		float z = radius * Mathf.Cos ( theta );

		return new Vector3 (x, y, z);
	}

	void CreateMesh() {
		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;

		const int numFaces = 16;
		const int numVertsPerFace = 3;
		const int numVerts = numFaces * numVertsPerFace;

		vertsPos = new Vector3[numVerts];
		vertsUV = new Vector2[numVerts];
		vertsIndices = new int[numVerts];
		vertsColor = new Color32[numVerts];

		for (int i = 0; i < numFaces; i++) {

			int indexOffset = i * numVertsPerFace;

			Color32 color = new Color32( (byte)(Random.Range(0,255)), (byte)(Random.Range(0,255)), (byte)(Random.Range(0,255)), 255 );

			Vector3 pos0 = GetRandomSpherePos();
			Vector3 pos1 = GetRandomSpherePos();
			Vector3 pos2 = -(pos0+pos1)*0.5f; pos2.Normalize(); pos2 *= pos1.magnitude;
			//pos2 = GetRandomSpherePos();

			vertsPos[0+indexOffset] = pos0;
			vertsPos[1+indexOffset] = pos1;
			vertsPos[2+indexOffset] = pos2;

			vertsColor[0+indexOffset] = color;
			vertsColor[1+indexOffset] = color;
			vertsColor[2+indexOffset] = color;

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

	// Use this for initialization
	void Start () {

		mousePosPrev = new Vector2 (0.0f, 0.0f);
		CreateMesh ();
	}

	void OnTouchStart( Vector2 aPos ) {
		rigidbody.angularVelocity = Vector3.zero;
	}

	void OnTouchEnd( Vector2 aPos, Vector2 aRelativePos ) {
	}

	void OnTouchMove( Vector2 aPos, Vector2 aRelativePos ) {
	
		Vector3 force = Vector3.right * aRelativePos.y - Vector3.up * aRelativePos.x;
		
		//rigidbody.rotation
		float speed = 400.0f;
		rigidbody.AddTorque( force * speed );
	}

	void HandleRotationInput()
	{
		//if (Input.GetMouseButton (0)) {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			
			if (touch.phase == TouchPhase.Began) {
				OnTouchStart (touch.position);

			} else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
				OnTouchMove (touch.position, touch.deltaPosition);
				
			} else if (touch.phase == TouchPhase.Ended) {
				OnTouchEnd (touch.position, touch.deltaPosition);
			}
		} else {

			Vector3 inputMousePos = Input.mousePosition;
			Vector2 mousePosCurr = new Vector2( inputMousePos.x, inputMousePos.y );
			Vector2 mousePosRel = mousePosCurr - mousePosPrev;

			if ( Input.GetMouseButtonDown(0) )
			{
				OnTouchStart ( mousePosCurr );

			}
			else if ( Input.GetMouseButtonUp(0) )
			{
				OnTouchEnd( mousePosCurr, mousePosRel );
			}
			else if ( Input.GetMouseButton(0) )
			{
				OnTouchMove( mousePosCurr, mousePosRel );
			}

			mousePosPrev = mousePosCurr;
		}
		
		//transform.Rotate (1.0f, 0.0f, 0.0f);
	}

	// Update is called once per frame
	void FixedUpdate () {
		HandleRotationInput ();
	}
}
