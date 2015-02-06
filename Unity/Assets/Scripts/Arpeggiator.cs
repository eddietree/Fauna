using UnityEngine;
using System.Collections;

public class Arpeggiator : MonoBehaviour {

	public Camera camera;
	public float nightCoeff;
	public InputRotator inputRotator;

	// Use this for initialization
	void Start () {
		nightCoeff = 0.0f;	
	}
	
	// Update is called once per frame
	void Update () {
		Color colorDay = new Color( 243.0f/255.0f, 233.0f/255.0f, 93.0f/255.0f );
		Color colorNight = new Color( 81.0f/255.0f, 85.0f/255.0f, 141.0f/255.0f );

		camera.backgroundColor = Color.Lerp (colorDay, colorNight, nightCoeff);
	
	}
}
