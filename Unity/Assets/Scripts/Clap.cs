using UnityEngine;
using System.Collections;

public class Clap : MonoBehaviour {

	public GameObject boxTop;
	public GameObject boxBot;
	private Vector3 boxTopStartPos;
	private Vector3 boxBotStartPos;

	// Use this for initialization
	void Start () {
		boxTopStartPos = boxTop.transform.position;
		boxBotStartPos = boxBot.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		audio.Play ();

		StartCoroutine (Punch ());
	}

	IEnumerator Punch()
	{
		iTween.Stop (boxTop);
		iTween.Stop (boxBot);

		yield return new WaitForSeconds (0.01f);

		boxTop.transform.position = boxTopStartPos;
		boxBot.transform.position = boxBotStartPos;

		float moveTime = 0.9f;
		float separation = 0.1f;
		iTween.PunchPosition (boxTop, Vector3.up * -separation, moveTime);
		iTween.PunchPosition (boxBot, Vector3.up * separation, moveTime);
	}
}
