using UnityEngine;
using System.Collections;

public class Vignette : MonoBehaviour {

	public Texture texture;
	private Color guiColor;
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		guiColor = Color.white;
		guiColor.a = 0.7f;

	}

	void OnGUI()
	{
		GUI.color = guiColor;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture, ScaleMode.StretchToFill, true);
		GUI.color = Color.white;
	}
}
