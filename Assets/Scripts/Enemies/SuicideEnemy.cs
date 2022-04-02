using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEnemy : EnemyBase
{
    [SerializeField]
    private float _suicideDistance = 0.0f;

    [SerializeField]
    private float _suicideChargeDuration = 0.0f;

    [SerializeField]
    private GameObject _explosionPrefab = null;

    private float _suicideStartTime = Mathf.Infinity;

    protected override void Awake() {
        base.Awake();
    }

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();

        if (IsStunned) {
            return;
        }

        if (_health <= 0.0f) {
            return;
        }

        if (_suicideStartTime == Mathf.Infinity && GetDistanceToPlayer() <= _suicideDistance) {
            StartSuicide();
        }

        if (Time.time - _suicideStartTime > _suicideChargeDuration) {
            PerformSuicide();
        }

        if (_navMeshAgent.enabled) {
            SetTarget(_player.gameObject);
        }
    }

    private void StartSuicide() {
        _suicideStartTime = Time.time;

        _navMeshAgent.ResetPath();
        _navMeshAgent.enabled = false; 
    }

    private void PerformSuicide() {
        _health = 0;

        GameObject explosionGO = Instantiate(_explosionPrefab, transform.position + new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity);
        Explosion explosion = explosionGO.GetComponent<Explosion>();

        _navMeshAgent.enabled = false;
        _rigidBody.isKinematic = false;
        _rigidBody.AddExplosionForce(explosion.Force, transform.position + transform.forward * 0.5f, explosion.Radius);
    }
}
