using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MatchInfo))]
[RequireComponent (typeof(MatchTimer))]
[RequireComponent (typeof(MatchSettings))]

public class MatchGUI : MonoBehaviour {

	public GUISkin skin;
	private MatchInfo matchInfo;
	private MatchTimer timer;
	private MatchSettings settings;
	public static float fontRatio = 15f / 344f;

	// Use this for initialization
	void Start () {
		matchInfo = GetComponent<MatchInfo>();
		timer = GetComponent<MatchTimer>();
		settings = GetComponent<MatchSettings>();
		GUIUtils.ScaleFonts(skin, GUIUtils.conversionRatio.y);
	}

	void OnDestroy () {
		GUIUtils.ScaleFonts(skin, 1.0f / GUIUtils.conversionRatio.y);
	}

	void OnGUI () {
		GUI.backgroundColor = Color.black;
		GUI.Box( GUIUtils.CenteredNormal(0.5f, 0.05f, 0.3f, 0.1f), "", skin.box);
		skin.label.normal.textColor = settings.team1.GetColor();
		GUI.Label(GUIUtils.CenteredNormal(0.4f, 0.05f, 0.1f, 0.1f), ""+matchInfo.GetScore(1), skin.label);
		skin.label.normal.textColor = settings.team2.GetColor();
		GUI.Label(GUIUtils.CenteredNormal(0.6f, 0.05f, 0.1f, 0.1f), ""+matchInfo.GetScore(2), skin.label);
		skin.label.normal.textColor = Color.yellow;
		float remaining = Mathf.Floor(timer.GetRemainingTime());
		if (remaining < 0f)
			remaining = 0f;
		string timeStr = "";
		if (remaining / 100 < 1) {
			timeStr += "0";
		}
		if (remaining / 10 < 1) {
			timeStr += "0";
		}
		timeStr += remaining;
		GUI.Label(GUIUtils.CenteredNormal(0.5f, 0.05f, 0.1f, 0.1f), timeStr, skin.customStyles[0]);
	}
}
