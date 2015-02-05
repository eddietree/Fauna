﻿using UnityEngine;
using System.Collections;

public class PolyballLines : MonoBehaviour {

	public PolyballPointsManager polyball_points;

	// Use this for initialization
	void Start () {
		CreateMesh ();
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
			
			Color32 color = new Color32(255,255,255,255);
			
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
