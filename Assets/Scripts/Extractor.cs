using UnityEngine;

class Extractor : MonoBehaviour {
    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private int _maxHealth = 1000;

    public void Start() {
        
    }

    private void Update() {
        if (_maxHealth <= 0) {
            Debug.LogError("YOU LOST!");
        }
    }

    public void BeginExtraction() {
        _animator.SetBool("active", true);
    }

    public void EndExtraction() {
        _animator.SetBool("active", false);
    }
}
