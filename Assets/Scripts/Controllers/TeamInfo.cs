using UnityEngine;

public class TeamInfo {

	private string name;
	private Color color;

	public TeamInfo(string n, Color c) {
		name = n;
		color = c;
	}

	public string GetName () {
		return name;
	}

	public Color GetColor () {
		return color;
	}

};