using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : EnemyBase {
    private enum PlayerState {
        Moving,
        Attacking,
    }

    private float _attackCooldown = 0.75f;
    private float _attackTime = 0f;
    private Animator _animator = null;

    private PlayerState _currentState = PlayerState.Moving;

    [SerializeField]
    protected GameObject _hitEffectPrefab = null;


    protected override void Awake() {
        base.Awake();

        _animator = GetComponentInChildren<Animator>();
    }

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();

        switch (_currentState) {
            case PlayerState.Moving:
                UpdateMovementState();
                break;
            case PlayerState.Attacking:
                UpdateAttackState();
                break;
        }

        _animator.SetFloat("speed", Mathf.Clamp01(_navMeshAgent.velocity.magnitude / _navMeshAgent.speed));
    }

    private void Attack() {
        _attackTime = Time.time;
        _animator.SetTrigger("attack");
        _navMeshAgent.enabled = false;
       
        Vector3 toEnemy = _player.transform.position - transform.position;
        if (toEnemy.magnitude < 2.5f && Vector3.Angle(transform.forward, toEnemy) < 100f) {
            _player.ReceiveDamage(10);

            GameObject effect = Instantiate(_hitEffectPrefab, _player.CenterPos(), Quaternion.FromToRotation(Vector3.forward, toEnemy), null);
            Destroy(effect, 2f);
        }
    }

    private void UpdateMovementState() {
        if (GetDistanceToPlayer() <= 1.0f) {
            SwitchState(PlayerState.Attacking);
            Attack();

            return;
        }

        if (_navMeshAgent.enabled) {
            SetTarget(_player.gameObject);
        }
    }

    private void UpdateAttackState() {
        if (Time.time > _attackTime + _attackCooldown) {
            SwitchState(PlayerState.Moving);
            _navMeshAgent.enabled = true;
        }
    }

    private void SwitchState(PlayerState nextState) {
        switch (nextState) {
            case PlayerState.Moving:
                break;
            case PlayerState.Attacking:
                Attack();
                break;
        }

        _currentState = nextState;
    }
}
