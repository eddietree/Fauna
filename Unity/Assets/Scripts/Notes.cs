using UnityEngine;
using System.Collections;

public class Notes : MonoBehaviour {

	public AudioClip[] notes;
	public InputRotator rotator;

	private int noteIndex;
	private int numNotesPerSet;
	private bool isDay;
	private float timeNoteCooldown;
	private float timeNoteCooldownMax;

	// Use this for initialization
	void Start () {
		noteIndex = 0;
		numNotesPerSet = notes.Length / 2;
		isDay = true;

		timeNoteCooldownMax = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
		float angularSpeed = rotator.rigidbody.angularVelocity.magnitude;

		timeNoteCooldown -= angularSpeed * Time.deltaTime;

		if (timeNoteCooldown < 0.0f) {
			timeNoteCooldown = timeNoteCooldownMax;

			noteIndex +=1;

			if ( noteIndex >= numNotesPerSet )
			{
				isDay = !isDay;
				OnChangeDay(isDay);
				noteIndex = 0;
			}

			// temp
			AudioSource.PlayClipAtPoint( notes[0], Vector3.one );
		}
		print (angularSpeed);
	}

	void OnChangeDay( bool isDay )
	{
		print ("CHANGE DAY");
	}
}
