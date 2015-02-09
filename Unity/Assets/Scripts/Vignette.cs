using UnityEngine;
using System.Collections;

public class Vignette : MonoBehaviour {

	public Texture texture;
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		// 
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture, ScaleMode.StretchToFill, true);
	}
}
