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

    public Animator animator;

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

        bool inRangeForAttack = GetDistanceToPlayer() <= _suicideDistance || GetDistanceToExtractor() <= _suicideDistance;
        if (_suicideStartTime == Mathf.Infinity && inRangeForAttack) {
            StartSuicide();
        }

        if (Time.time - _suicideStartTime > _suicideChargeDuration) {
            PerformSuicide();
        }

        if (_navMeshAgent.enabled) {
            if (GetDistanceToPlayer() < 10f) {
                SetTarget(_player.gameObject);
            } else if (Extractor.Instance != null) {
                SetTarget(Extractor.Instance.gameObject);
            }
        }
    }

    private void StartSuicide() {
        _suicideStartTime = Time.time;

        _navMeshAgent.ResetPath();
        _navMeshAgent.enabled = false;

        animator.SetTrigger("explode");
    }

    private void PerformSuicide() {
        _health = 0;

        GameObject explosionGO = Instantiate(_explosionPrefab, transform.position + new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity, null);
        Explosion explosion = explosionGO.GetComponent<Explosion>();

        //_navMeshAgent.enabled = false;
        //_rigidBody.isKinematic = false;
        //_rigidBody.AddExplosionForce(explosion.Force, transform.position + transform.forward * 0.5f, explosion.Radius);

        Destroy(gameObject);
    }
}
