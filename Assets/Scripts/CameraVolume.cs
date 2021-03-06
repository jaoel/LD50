using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVolume : MonoBehaviour {
    private Camera _camera = null;

    private void Awake() {
        _camera = GetComponentInChildren<Camera>(true);

    }
    private void Start() {

    }

    private void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if (_camera.gameObject.tag == "MainCamera") {
            return;
        }

        Camera main = Camera.main;
        main.enabled = false;
        main.gameObject.tag = "Untagged";

        _camera.enabled = true;
        _camera.gameObject.tag = "MainCamera";
    }
}
