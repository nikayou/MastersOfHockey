using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(Collider2D))]

public class SoundOnWallHit : MonoBehaviour {

	public AudioClip [] sounds;

	void OnCollisionEnter2D (Collision2D col) {
		if (col.transform.tag == "Wall") {
			audio.PlayOneShot(GetRandom());
		}
	}

	AudioClip GetRandom () {
		return sounds[Random.Range(0, sounds.Length)];
	}

}
