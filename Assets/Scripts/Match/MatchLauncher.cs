using UnityEngine;
using System.Collections;


public class MatchLauncher : MonoBehaviour
{

		private MatchSettings matchSettings;

		void Awake ()
		{
				matchSettings = gameObject.GetComponent<MatchSettings> ();
				GameObject gc = GameObject.Find ("OldGameController"); 
				if (gc) {
						gc.transform.tag = "OldGameController";
						MatchSettings gcOld = gc.GetComponent<MatchSettings> ();
						matchSettings.matchTime = gcOld.matchTime;
						matchSettings.goalLevel = gcOld.goalLevel;
						matchSettings.cameraSize = gcOld.cameraSize;
						matchSettings.team1 = gcOld.team1;
						matchSettings.team2 = gcOld.team2;
						Destroy (gc);
				} 
		}

		void Start ()
		{

				SetCamera ();
				SetGoals ();
				SetPlayersTeam ();
				SetEnd ();
		}

		void SetCamera ()
		{
				Camera.main.orthographicSize = matchSettings.cameraSize;
		}

		/// <summary>
		/// Sets both goal levels and teams.
		/// </summary>
		void SetGoals ()
		{
				foreach (GameObject g in GameObject.FindGameObjectsWithTag("GoalKeeper")) {
						Color color;
						if (g.name == "GoalKeeper1") {
								color = matchSettings.team1.GetColor ();
						} else {
								color = matchSettings.team2.GetColor ();
						}
						ColorChange.SetColor (g.transform, color);
						g.GetComponent<GoalAI> ().smoothness = matchSettings.goalLevel;
				}
		}

		void SetPlayersTeam ()
		{
				ColorChange.SetColor (GameObject.Find ("Player1").transform, matchSettings.team1.GetColor ());
				ColorChange.SetColor (GameObject.Find ("Player2").transform, matchSettings.team2.GetColor ());
		}

		void SetEnd ()
		{
				StartCoroutine (EndMatch ());
		}

		IEnumerator EndMatch ()
		{
				yield return new WaitForSeconds (matchSettings.matchTime);
		}


}