using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    [SerializeField]
    private float _lifeTime = 1.0f;

    [SerializeField]
    private float _explosionRadius = 0.0f;

    [SerializeField]
    private float _explosionForce = 0.0f;

    [SerializeField]
    private int _damage = 0;

    private int _layerMask = -1;

    public float Damage => _damage;
    public float Force => _explosionForce;
    public float Radius => _explosionRadius;

    private void Awake() {
        _layerMask = Layers.GetMask(Layers.Player, Layers.Enemies, Layers.Props, Layers.Extractor);

        Destroy(gameObject, _lifeTime);
    }

    private void Start() {
        Collider[] colliders =  Physics.OverlapSphere(transform.position, _explosionRadius, _layerMask);

        foreach (Collider collider in colliders) {
            HandleCollider(collider);
        }
    }

    private void Update() {
    }

    private void HandleCollider(Collider other) {
        if (other.TryGetComponent(out Player player)) {
            player.ReceiveDamage(_damage);
        }
        else if (other.TryGetComponent(out EnemyBase enemy)) {
            //enemy.Damage(_damage);
        }
        else if (other.TryGetComponent(out Extractor extractor)) {
            extractor.ReceiveDamage(_damage);
        }
        else if (other.gameObject.layer == Layers.Props) {
            other.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}
