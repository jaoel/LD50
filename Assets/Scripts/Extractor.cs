using System;
using UnityEngine;

class Extractor : MonoBehaviour {
    public static Extractor Instance { get; private set; }

    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private int _maxHealth = 1000;

    public GameObject extractionText;

    public AudioSource audioSource;

    private bool isExtracting = false;

    public void Awake() {
        Instance = this;
    }

    private void Update() {
    }

    public void BeginExtraction() {
        if (!isExtracting) {
            _animator.SetBool("active", true);
            extractionText.SetActive(false);
            GameManager.Instance.LevelManager.StartLevel();
            isExtracting = true;
            audioSource.Play();
        }
    }

    public void EndExtraction() {
        if (isExtracting) {
            _animator.SetBool("active", false);
            extractionText.SetActive(true);
            isExtracting = false;
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Player player)) {
            BeginExtraction();
        }
    }

    internal void ReceiveDamage(int damage) {
        _maxHealth -= damage;
        if (_maxHealth <= 0) {
            Debug.LogError("YOU LOST!");
        }
    }

    internal Vector3 CenterPos() {
        return transform.position + Vector3.up;
    }
}
