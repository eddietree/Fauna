﻿using UnityEngine;
using System.Collections;

public class Notes : MonoBehaviour {

	public AudioClip[] notes;
	public InputRotator rotator;
	public Camera camera;

	private int noteIndex;
	private int notesPerDayCycle;

	private bool isDay;
	private float timeNoteCooldown;
	private float timeNoteCooldownMax;

	private Arpeggiator arp;

	// Use this for initialization
	void Start () {
		noteIndex = 0;
		isDay = true;
		notesPerDayCycle = 16;

		timeNoteCooldownMax = 1.75f;

		arp = GetComponent<Arpeggiator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		float angularSpeed = rotator.rigidbody.angularVelocity.magnitude;

		timeNoteCooldown -= angularSpeed * Time.deltaTime;

		if (timeNoteCooldown < 0.0f) {
			timeNoteCooldown = timeNoteCooldownMax;

			PlayNote();
			noteIndex +=1;

			if ( noteIndex >= notesPerDayCycle )
			{
				isDay = !isDay;
				OnChangeDay(isDay);
				noteIndex = 0;
			}

		}
	}

	void PlayNote()
	{
		int numNotesPerSet = notes.Length / 2;
		int indexOffset = isDay ? 0 : numNotesPerSet;

		int noteIndexMin = indexOffset;
		int noteIndexMax = noteIndexMin + numNotesPerSet;

		// random
		//int playNoteIndex = Random.Range (noteIndexMin, noteIndexMax);

		// linear!
		int playNoteIndex = noteIndexMin + noteIndex % numNotesPerSet;


		// reverse for night
		//if (!isDay)	playNoteIndex = noteIndexMin + (noteIndexMax-playNoteIndex) - 1;

		// temp!!
		//playNoteIndex = 0;

		//audio.clip = notes [playNoteIndex];
		//audio.Play ();

		Vector3 camPos = camera.transform.position;
		Vector3 soundDir = rotator.transform.forward;
		float time = Time.time * 3.0f;
		//Vector3 soundPos = camPos + (new Vector3 (Mathf.Cos (time), 0.0f, Mathf.Sin (time))) * 2.0f;
		Vector3 soundPos = camPos + soundDir * 2.0f;
		AudioSource.PlayClipAtPoint( notes[playNoteIndex], soundPos );


		GameObject.Find ("Katamari/Polyball").gameObject.GetComponent<Polyball> ().OnClick ();
		GameObject.Find ("Katamari/PolyballLines").gameObject.GetComponent<PolyballLines> ().OnClick ();


	}

	void OnChangeDay( bool isDay )
	{
		arp.ChangeDay (isDay);
	}
}
