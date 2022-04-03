using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {
    private static float _shakeAmount;
    private static float _intensity;

    [SerializeField]
    private Transform target;

    public static void Shake(float amount, float intensity) {
        _shakeAmount = Mathf.Max(amount, _shakeAmount);
        _intensity = Mathf.Max(intensity, _intensity);
    }

    float t = 0f;
    bool canUpdate = true;
    private void Update() {
        if (canUpdate) {
            _shakeAmount -= Time.deltaTime * 2f;
            _intensity -= Time.deltaTime;

            if (_shakeAmount < 0f) {
                _shakeAmount = 0f;
            }

            if (_intensity < 0f) {
                _intensity = 0f;
            }

            t += Time.deltaTime * _intensity;
            Vector3 shakeVector = new Vector3(Mathf.PerlinNoise(t, 10f), Mathf.PerlinNoise(t, t), Mathf.PerlinNoise(0, t)) * 2f - Vector3.one;
            target.localPosition = shakeVector * _shakeAmount;
            
            canUpdate = false;
        }
    }

    private void LateUpdate() {
        canUpdate = true;
    }

}
