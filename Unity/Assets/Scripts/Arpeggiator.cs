using UnityEngine;
using System.Collections;

public class Arpeggiator : MonoBehaviour {

	public Camera camera;
	public Light light;
	public float nightCoeff;
	private float goalNightCoeff;

	public InputRotator inputRotator;

	// Use this for initialization
	void Start () {
		nightCoeff = 0.0f;	
		goalNightCoeff = nightCoeff;
	}
	
	// Update is called once per frame
	void Update () {

		nightCoeff = Mathf.Lerp (nightCoeff, goalNightCoeff, 0.07f);

		// bg color
		Color colorDay = new Color( 243.0f/255.0f, 233.0f/255.0f, 93.0f/255.0f );
		Color colorNight = new Color( 81.0f/255.0f, 85.0f/255.0f, 141.0f/255.0f );
		camera.backgroundColor = Color.Lerp (colorDay, colorNight, nightCoeff);

		// light color
		Color lightColorDay = new Color( 255.0f/255.0f, 244.0f/255.0f, 221.0f/255.0f );
		Color lightColorNight = new Color( 140.0f/255.0f, 143.0f/255.0f, 180.0f/255.0f );
		light.color = Color.Lerp (lightColorDay, lightColorNight, nightCoeff);

		GameObject polyBall = GameObject.Find ("Polyball");
		GameObject polyBallLines = GameObject.Find ("PolyballLines");

		if (polyBall != null ) polyBall.gameObject.renderer.material.SetFloat ("uDayCoeff", nightCoeff);
		if (polyBallLines != null ) polyBallLines.gameObject.renderer.material.SetFloat ("uDayCoeff", nightCoeff);

			//GameObject.Find ("Katamari/PolyballLines").gameObject.GetComponent<PolyballLines> ().OnClick ();
	}

	public void ChangeDay( bool day )
	{
		goalNightCoeff = day ? 0.0f : 1.0f;
	}
}
