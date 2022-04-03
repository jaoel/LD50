using System.Collections;
using UnityEngine;

public class HitEffect : MonoBehaviour {
    public AudioSource audioSource;

    private Vector3 rotation = Vector3.zero;

    private void Awake() {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayDelayed(Random.Range(0f, 0.1f));
    }
}