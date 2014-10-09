using UnityEngine;
using System.Collections;

public class MatchSettings : MonoBehaviour {


	public float cameraSize = 12;
	public float goalLevel = 1.5f;
	public TeamInfo team1;
	public TeamInfo team2;
	public float matchTime = 180f;
	public Color defaultColor1 = Color.green;
	public Color defaultColor2 = Color.magenta;

	// Use this for initialization
	void Awake () {
		team1 = new TeamInfo("Home", defaultColor1);
		team2 = new TeamInfo("Visitors", defaultColor2);
	}

}
