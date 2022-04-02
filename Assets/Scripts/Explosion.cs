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
    private float _damage = 0.0f;

    private int _layerMask = -1;

    public float Damage => _damage;
    public float Force => _explosionForce;
    public float Radius => _explosionRadius;

    private void Awake() {
        _layerMask = Layers.GetMask(Layers.Player, Layers.Enemies, Layers.Props);

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
        if (other.gameObject.layer == Layers.Player) {
            Player player = other.gameObject.GetComponent<Player>();
            player.ReceiveDamage(_damage);

        }
        else if (other.gameObject.layer == Layers.Enemies) {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
        }
        else if (other.gameObject.layer == Layers.Props) {
            other.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}
