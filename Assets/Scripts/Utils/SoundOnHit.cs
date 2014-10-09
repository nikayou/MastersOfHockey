using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class SoundOnHit : MonoBehaviour {

	public AudioClip [] sounds;

	void OnCollisionEnter (Collision col) {
		if (col.transform.tag == "Puck") {
			audio.PlayOneShot(GetRandom());
		}
	}

	AudioClip GetRandom () {
		return sounds[Random.Range(0, sounds.Length)];
	}

}
