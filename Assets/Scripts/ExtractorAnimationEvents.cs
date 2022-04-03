﻿using System.Collections;
using UnityEngine;

public class ExtractorAnimationEvents : MonoBehaviour {
    private float _maxDistToPlayer = 50f;
    public AudioSource audioSourceThud1;
    public AudioSource audioSourceThud2;

    private Player _player;

    float ShakeAmount => 1f - Mathf.Clamp01(Vector3.Distance(_player.transform.position, transform.position) / _maxDistToPlayer);

    private void Start() {
        _player = FindObjectOfType<Player>();
    }

    public void Shake1() {

        Shaker.Shake(0.25f * ShakeAmount, 25f * ShakeAmount);
        audioSourceThud1.pitch = Random.Range(0.95f, 1.05f);
        audioSourceThud1.Play();
    }

    public void Shake2() {
        Shaker.Shake(0.15f * ShakeAmount, 15f * ShakeAmount);
        audioSourceThud2.pitch = Random.Range(0.95f, 1.05f);
        audioSourceThud2.Play();
    }
}