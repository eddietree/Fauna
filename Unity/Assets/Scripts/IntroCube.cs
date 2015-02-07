using UnityEngine;
using System.Collections;

public class IntroCube : MonoBehaviour {

	private bool clicked = false;
	GameObject katamari;

	// Use this for initialization
	void Start () {
		clicked = false;
		katamari = GameObject.Find ("Katamari").gameObject;
		katamari.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate( 0.0f, Time.deltaTime * 30.0f, 0.0f );
	}

	void OnMouseDown() {

		if (!clicked) {
			clicked = true;
			iTween.MoveTo (gameObject, new Vector3 (0.0f, 5.0f, 0.0f), 1.5f);

			katamari.SetActive(true);
			audio.Play ();

			iTween.PunchScale( katamari, Vector3.one*0.5f, 1.5f );
		}
	}
}
