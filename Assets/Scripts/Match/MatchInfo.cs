using UnityEngine;
using System.Collections;

/// <summary>
/// Match info handles various match data : score, timer, rounds...
/// </summary>
public class MatchInfo : MonoBehaviour {

	/// <summary>
	/// The score of both team.
	/// </summary>
	private int [] score;

	// Use this for initialization
	void Start () {
		score = new int[2];
	}

	/// <summary>
	/// Adds the given amount of point to the given player index.
	/// </summary>
	/// <returns>The new score.</returns>
	/// <param name="nb">Number of points to add/remove.</param>
	/// <param name="index">Index of the player who takes the points.</param>
	public int AddScore (int nb, int index) {
		score[index-1] += nb;
		return score[index-1];
	}

	/// <summary>
	/// Returns the score of the given player index.
	/// </summary>
	/// <returns>The score of the given team.</returns>
	/// <param name="team">Index of the team to get score from.</param>
	public int GetScore (int team) {
		return score[team-1];
	}
}
