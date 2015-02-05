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

	public Vector3 GetRandomSpherePos()
	{
		int numTheta = 15;
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
