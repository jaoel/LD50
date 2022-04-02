using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEnemy : EnemyBase
{
    [SerializeField]
    private float _suicideDistance = 0.0f;

    [SerializeField]
    private float _explosionRadius = 0.0f;

    [SerializeField]
    private float _suicideChargeDuration = 0.0f;

    private float _suicideStartTime = Mathf.Infinity;
    private float _defaultSpeed = 0.0f;

    protected override void Awake() {
        base.Awake();

        _defaultSpeed = _navMeshAgent.speed;
    }

    protected override void Start() {
        base.Start();

        SetTarget(_player.gameObject);
    }

    protected override void Update() {
        base.Update();

        if (_suicideStartTime == Mathf.Infinity && GetDistanceToPlayer() <= _suicideDistance) {
            StartSuicide();
        }

        if (Time.time - _suicideStartTime > _suicideChargeDuration) {
            PerformSuicide();
        }
    }

    private void StartSuicide() {
        _suicideStartTime = Time.time;

        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
    }

    private void PerformSuicide() {
        _health = 0.0f;

        KillMe();
        Destroy(gameObject);
    }
}
