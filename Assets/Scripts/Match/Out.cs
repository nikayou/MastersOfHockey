using UnityEngine;
using System.Collections;

/// <summary>
/// Manages the Out areas.
/// </summary>
public class Out : MonoBehaviour {

	private FaceOff faceOff;

	void Start () {
		faceOff = GameObject.FindGameObjectWithTag("GameController").GetComponent<FaceOff>();
	}

	void OnTriggerStay (Collider other) {
		faceOff.MakeFaceOff();
	}

}
