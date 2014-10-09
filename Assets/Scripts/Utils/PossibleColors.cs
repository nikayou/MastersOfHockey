
using System.Collections;
using UnityEngine;

/// <summary>
/// Possible colors for public.
/// </summary>
public class PossibleColors {
	
	public static Color [] possibleColors = new Color[] {
		new Color(205,92,92,255),
		new Color(240,128,128,255),
		new Color(233,150,122,255),
		new Color(220,20,60,255),
		new Color(178,34,34,255),
		new Color(139,0,0,255),
		new Color(255,192,203,255),
		new Color(255,105,180,255),
		new Color(199,21,133,255),
		new Color(219,112,147,255),
		new Color(255,99,71,255),
		new Color(255,69,0,255),
		new Color(255,140,0,255),
		new Color(255,215,0,255),
		new Color(255,228,181,255),
		new Color(255,218,185,255),
		new Color(240,230,140,255),
		new Color(189,183,107,255),
		new Color(216,191,216,255),
		new Color(238,130,238,255),
		new Color(218,112,214,255),
		new Color(186,85,211,255),
		new Color(147,112,219,255),
		new Color(138,43,226,255),
		new Color(139,0,139,255),
		new Color(75,0,130,255),
		new Color(106,90,205,255),
		new Color(50,205,50,255),
		new Color(144,238,144,255),
		new Color(60,179,113,255),
		new Color(34,139,34,255),
		new Color(0,100,0,255),
		new Color(154,205,50,255),
		new Color(128,128,0,255),
		new Color(85,107,47,255),
		new Color(32,178,170,255),
		new Color(175,238,238,255),
		new Color(95,158,160,255),
		new Color(70,130,180,255),
		new Color(176,196,222,255),
		new Color(135,206,235,255),
		new Color(100,149,237,255),
		new Color(65,105,225,255),
		new Color(25,25,112,255),
		new Color(210,180,140,255),
		new Color(205,133,63,255),
		new Color(139,69,19,255),
		new Color(160,82,45,255),
		new Color(220,220,220,255),
		new Color(192,192,192,255),
		new Color(105,105,105,255)
	};

	public static Color GetRandom () {
		Color c = possibleColors[Random.Range(0, possibleColors.Length)];
		c.a = 255;
		return c/255;
	}


}


