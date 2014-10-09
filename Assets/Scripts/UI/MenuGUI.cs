using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.IO;

public enum MenuState
{
		MAIN_MENU,
		OPTIONS,
		TEAM,
		MATCH_SETTINGS,
		MATCH_END
}
;

public class MenuGUI : MonoBehaviour
{

		/// <summary>
		/// Custom menu skin.
		/// </summary>
		public GUISkin skin;
		/// <summary>
		/// The current menu that should be displayed.
		/// </summary>
		public MenuState menuState;
		public TeamInfo team1;
		public TeamInfo team2;

		/// <summary>
		/// All teams infos, read from the XML fil "teams.xml".
		/// </summary>
		private List<TeamInfo> allTeams;

		/// <summary>
		/// The font ratio, ie the relative size of fonts accordingly to screen height.
		/// </summary>
		//private static float fontRatio = 16f / 480f;
		//private static Vector2 conversionRatio = (new Vector2 (Screen.width / 640f, Screen.height / 480f));
		private int index1 = 0;
		private int index2 = 1;
		private float cameraSize = 12;
		private float goalLevel = 1.5f;
		private float matchTime = 180f;

		public string title = "Masters Of Hockey";

		void Awake ()
		{
				allTeams = new List<TeamInfo> ();
				GUIUtils.ScaleFonts (skin, GUIUtils.conversionRatio.y);
		}

		void Start ()
		{
				RetrieveTeams ();
				team1 = allTeams [0];
				team2 = allTeams [1];
		}

	
		void OnDestroy ()
		{
				GUIUtils.ScaleFonts (skin, 1.0f / GUIUtils.conversionRatio.y);
		}

		void OnGUI ()
		{
				switch (menuState) {
				case MenuState.MATCH_END:
						DisplayMatchEnd ();
						break;
				case MenuState.MATCH_SETTINGS:
						DisplayMatchSettings ();
						break;
				case MenuState.OPTIONS:
						DisplayOptions ();
						break;
				case MenuState.TEAM:
						DisplayTeamMenu ();
						break;
				default:
						DisplayMainMenu ();
						break;
				}
		}

		void DisplayMainMenu ()
		{
				GUI.Label (GUIUtils.CreateNormalRect (0f, 0.1f, 1f, 0.3f), title, skin.customStyles [0]);
				if (GUI.Button (GUIUtils.CreateNormalRect (0.4f, 0.7f, 0.2f, 0.1f), "Start", skin.button)) {
						menuState = MenuState.TEAM;
				}
				if (GUI.Button (GUIUtils.CreateNormalRect (0.4f, 0.85f, 0.2f, 0.1f), "Quit", skin.button)) {
						Application.Quit ();
				}

		}

		void DisplayMatchEnd ()
		{
				
		
		}

		void DisplayMatchSettings ()
		{
				GUI.Label (GUIUtils.CreateNormalRect (0f, 0.05f, 1f, 0.1f), "Match settings", skin.label);
				if (GUI.Button (GUIUtils.CreateNormalRect (0.4f, 0.7f, 0.2f, 0.1f), "Next", skin.button)) {
						LaunchMatch ();
				}
				if (GUI.Button (GUIUtils.CreateNormalRect (0.4f, 0.85f, 0.2f, 0.1f), "Back", skin.button)) {
						menuState = MenuState.TEAM;
				}
				GUI.Label (GUIUtils.CreateNormalRect (0.05f, 0.2f, 0.30f, 0.07f), "Camera size ", skin.label);
				GUI.Label (GUIUtils.CreateNormalRect (0.30f, 0.2f, 0.30f, 0.07f), "" + (int)cameraSize, skin.label);
				cameraSize = GUI.HorizontalSlider (GUIUtils.CreateNormalRect (0.5f, 0.225f, 0.30f, 0.07f), cameraSize, 7.0f, 20.0f);
				GUI.Label (GUIUtils.CreateNormalRect (0.05f, 0.35f, 0.30f, 0.07f), "Goal Level", skin.label);
				GUI.Label (GUIUtils.CreateNormalRect (0.30f, 0.35f, 0.30f, 0.07f), "" + ((Mathf.Round (goalLevel * 100f) / 100f) + 0.5f), skin.label);
				goalLevel = GUI.HorizontalSlider (GUIUtils.CreateNormalRect (0.5f, 0.375f, 0.30f, 0.07f), goalLevel, 0.5f, 3.5f);
				GUI.Label (GUIUtils.CreateNormalRect (0.05f, 0.45f, 0.30f, 0.15f), "Match time \n(in seconds) ", skin.label);
				GUI.Label (GUIUtils.CreateNormalRect (0.30f, 0.50f, 0.30f, 0.07f), "" + Mathf.Round (matchTime), skin.label);
				matchTime = GUI.HorizontalSlider (GUIUtils.CreateNormalRect (0.5f, 0.525f, 0.30f, 0.1f), matchTime, 60f, 900f);
		}
	
		void DisplayOptions ()
		{
				
		}

		void DisplayTeamMenu ()
		{
				GUI.Label (GUIUtils.CreateNormalRect (0f, 0.05f, 1f, 0.1f), "Team Select", skin.label);
				if (GUI.Button (GUIUtils.CreateNormalRect (0.4f, 0.7f, 0.2f, 0.1f), "Next", skin.button)) {
						menuState = MenuState.MATCH_SETTINGS;
				}
				if (GUI.Button (GUIUtils.CreateNormalRect (0.4f, 0.85f, 0.2f, 0.1f), "Back", skin.button)) {
						menuState = MenuState.MAIN_MENU;
				}
				Color cacheColor = skin.button.normal.textColor;
				int cacheSize = skin.button.fontSize;
				skin.button.fontSize = Mathf.FloorToInt (skin.button.fontSize * 0.45f);
				float y = 0.3f;
				float x = 0.05f;
				float w = 0.175f;
				float h = 0.05f;
				float i = 0f;
				float rightX = 1.0f - x - w;
				foreach (TeamInfo ti in allTeams) {
						Color c = ti.GetColor ();
						skin.button.normal.textColor = c;
						skin.button.hover.textColor = c;
						float posY = y + i * (h + 0.05f);
						// left
						if (GUI.Button (GUIUtils.CreateNormalRect (x, posY, w, h), ti.GetName (), skin.button)) {
								if (team2 != ti) {
										team1 = ti;
										index1 = (int)i;
										
								} 
						}
						// right
						if (GUI.Button (GUIUtils.CreateNormalRect (rightX, posY, w, h), ti.GetName (), skin.button)) {
								if (team1 != ti) {
										team2 = ti;
										index2 = (int)i;
								} 
						}
						i++;
				}
				skin.button.fontSize = cacheSize;
				skin.button.normal.textColor = cacheColor;
				skin.button.hover.textColor = cacheColor;
				// displaying labels
				y = 0.3f + index1 * (h + 0.05f);
				x = 0.01f;
				rightX = 0.99f - h;
				GUI.Label (GUIUtils.CreateNormalRect (x, y, h, h), ">", skin.label);
				y = 0.3f + index2 * (h + 0.05f);
				GUI.Label (GUIUtils.CreateNormalRect (rightX, y, h, h), "<", skin.label);
		}
	
		/// <summary>
		/// Retrieves the teams located in the "teams.xml" file.
		/// </summary>
		void RetrieveTeams ()
		{
				XmlTextReader xtr = new XmlTextReader ("teams.xml");
				string name = "";
				string colorStr = "";
				while (xtr.Read()) {
						if (xtr.Name == "TEAM") {
								name = xtr.GetAttribute ("name");
								colorStr = xtr.GetAttribute ("color");
								TeamInfo ti = new TeamInfo (name, GUIUtils.StringRGBToColor (colorStr));
								allTeams.Add (ti);
						}
				}
				if (allTeams.Count <= 1) {
						Quit (4.0f, "Need at least 2 teams. Quitting...");
				}
		}

		

		public IEnumerator Quit (float delay, string msg = "")
		{
				print (msg);
				yield return new WaitForSeconds (delay);
				Application.Quit ();
		}

		void LaunchMatch ()
		{
				GameObject gc = GameObject.FindGameObjectWithTag ("GameController");
				gc.name = "OldGameController";
				gc.transform.tag = "OldGameController";
				MatchSettings ms = gc.AddComponent<MatchSettings> ();
				ms.cameraSize = cameraSize;
				ms.goalLevel = goalLevel;
				ms.team1 = team1;
				ms.team2 = team2;
				ms.matchTime = matchTime;
				DontDestroyOnLoad (gc);
				Application.LoadLevel (1);
		}
	
}
