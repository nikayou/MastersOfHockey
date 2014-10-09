using UnityEngine;
using System.Collections;

/// <summary>
/// Detect if registered transform are out of screen, and displays a box in their direction.
/// </summary>
public class DetectOutOfScreen : MonoBehaviour {

	/// <summary>
	/// Elements to follow
	/// </summary>
	public Transform [] detectable;
	/// <summary>
	/// Colors of indicators. Follows the same index. 
	/// </summary>
	Color [] colors;
	public float detectorDimension = 40;
	public GUISkin skin; 

	void Start () {
		// TODO: not scalable here
		detectorDimension *= GUIUtils.conversionRatio.x;
		GameObject gc = GameObject.Find("GameController");
		MatchSettings ms = gc.GetComponent<MatchSettings>();
		colors = new Color[detectable.Length];
		colors[0] = ms.team1.GetColor();
		colors[1] = ms.team2.GetColor();
	}

	void OnGUI () {
		for (int i = 0; i < detectable.Length; i++) {
			Transform t = detectable[i];
			Vector3 screenPos = Camera.main.WorldToScreenPoint(t.position);
			if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height) {
				float x = screenPos.x;
				if (x < 0)
					x = 0;
				if (x > Screen.width)
					x = Screen.width;
				float y = Screen.height - screenPos.y;
				if (y < 0)
					y = 0;
				if (y > Screen.height)
					y = Screen.height;
				GUI.backgroundColor = colors[i];
				GUI.Box(new Rect(x-detectorDimension/2, y-detectorDimension/2, detectorDimension, detectorDimension), " ", skin.box);
			}
		}
	}

}
