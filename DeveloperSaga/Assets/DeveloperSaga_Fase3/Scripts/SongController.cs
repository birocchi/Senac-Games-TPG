﻿using UnityEngine;
using System.Collections;

public class SongController : MonoBehaviour
{
		public static int songToPlay = -1;
		public AudioClip[] avaliableSongs;
		private int playingSong;

		// Use this for initialization
		void Start ()
		{
				playingSong = songToPlay;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (songToPlay != playingSong) {
						Debug.Log ("Song changed! Song: " + songToPlay);
						if (avaliableSongs.Length > 0) {
								audio.volume = audio.volume -= 0.01f;
								if (audio.volume <= 0f) {
										audio.Stop ();
										audio.volume = 0.8f;
										audio.clip = avaliableSongs [songToPlay];
										playingSong = songToPlay;
										audio.Play ();
										playingSong = songToPlay;
								}
						}
				}
		}
}
