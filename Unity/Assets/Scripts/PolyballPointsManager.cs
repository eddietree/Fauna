using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolyballPointsManager : MonoBehaviour {

	private List<Vector3> points;

	// Use this for initialization
	void Awake () {
		points = new List<Vector3>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetSurroundingTri( out Vector3 aVec0, out Vector3 aVec1, out Vector3 aVec2 ) {

		//swap
		int randIndex = Random.Range (0, points.Count-1);
		SwapIndices (0, randIndex);

		for( int kk = 0; kk < 2; kk++ )
		{
			int indexMin = 1;
			float minDistSqr = (points[indexMin] - points [0]).sqrMagnitude;

			for( int i = 2 + kk; i < points.Count; i++ )
			{
				float currMinDistSqr = (points [i] - points [0]).sqrMagnitude;

				if ( currMinDistSqr < minDistSqr )
				{
					indexMin = i;
					minDistSqr = currMinDistSqr;
				}
			}

			// swap next
			SwapIndices (1 + kk, indexMin);
		}
		
		aVec0 = points [0];
		aVec1 = points [1];
		aVec2 = points [2];
	}

	private void SwapIndices( int index0, int index1 )
	{
		//swap
		Vector3 temp = points[index0];
		points[index0] = points[index1];
		points[index1] = temp;
	}

	public Vector3 GetRandomSpherePos()
	{
		int numTheta = 6;
		int numPhi = 10;
		
		float deltaTheta = 2.0f * Mathf.PI / numTheta;
		float deltaPhi = Mathf.PI / numPhi;
		float theta = Random.Range (0, numTheta) * deltaTheta;
		float phi = Random.Range (1, numPhi-1) * deltaPhi - Mathf.PI*0.5f;
		
		float radius = 1.0f;
		float x = radius * Mathf.Cos( phi ) * Mathf.Sin ( theta );
		float y = radius * Mathf.Sin( phi ) * Mathf.Sin ( theta );
		float z = radius * Mathf.Cos ( theta );
		
		Vector3 result = new Vector3 (x, y, z);
		points.Add (result);

		return result;
	}
}
