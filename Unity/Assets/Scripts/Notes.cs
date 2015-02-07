using UnityEngine;
using System.Collections;

public class Notes : MonoBehaviour {

	public AudioClip[] notes;
	public InputRotator rotator;

	private int noteIndex;
	private int numNotesPerSet;
	private int notesPerDayCycle;

	private bool isDay;
	private float timeNoteCooldown;
	private float timeNoteCooldownMax;

	private Arpeggiator arp;

	// Use this for initialization
	void Start () {
		noteIndex = 0;
		numNotesPerSet = notes.Length / 2;
		isDay = true;
		notesPerDayCycle = 8;

		timeNoteCooldownMax = 2.0f;

		arp = GetComponent<Arpeggiator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		float angularSpeed = rotator.rigidbody.angularVelocity.magnitude;

		timeNoteCooldown -= angularSpeed * Time.deltaTime;

		if (timeNoteCooldown < 0.0f) {
			timeNoteCooldown = timeNoteCooldownMax;

			noteIndex +=1;

			if ( noteIndex >= notesPerDayCycle )
			{
				isDay = !isDay;
				OnChangeDay(isDay);
				noteIndex = 0;
			}

			// temp
			AudioSource.PlayClipAtPoint( notes[0], Vector3.one );
		}
	}

	void OnChangeDay( bool isDay )
	{
		arp.ChangeDay (isDay);
	}
}
