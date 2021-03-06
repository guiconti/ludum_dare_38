﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	Transform audioListener;
	Transform playerTransform;
	SoundLibrary soundLibrary;

	AudioSource audioSource;

	bool canPlayImpact = true;
	float timeToPlayImpact = 0f;

	void Start() {

		if(instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);

			soundLibrary = GetComponent<SoundLibrary>();
			audioListener = FindObjectOfType<AudioListener>().transform;
			playerTransform = FindObjectOfType<Player>().transform;
			audioSource = GetComponent<AudioSource>();

		}
	}

	void Update() {
		if(!canPlayImpact) {
			canPlayImpact = Time.time >= timeToPlayImpact;
		}
		
		if (playerTransform != null) {
			audioListener.position = playerTransform.position;
		}
	}

	public void PlaySound(AudioClip clip) {
		if(clip != null) {
			//audioSource.PlayClipAtPoint(clip, position); 
			audioSource.PlayOneShot(clip, 3f);
		}
	}

	public void PlaySound(string clipName) {
		if (clipName == "Impact") {
			if (canPlayImpact) {
				timeToPlayImpact = Time.time + 0.05f;
				canPlayImpact = false;
				PlaySound(soundLibrary.GetClipFromName(clipName));
			}
		} else {
			PlaySound(soundLibrary.GetClipFromName(clipName));
		}
	}
}
