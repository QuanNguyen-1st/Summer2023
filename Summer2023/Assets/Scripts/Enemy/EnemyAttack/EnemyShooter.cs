using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyAttack
{
    [SerializeField] private EnemyStatSO _enemyData;
    private bool _isShooting = false;
    [SerializeField] private bool _isHound;
    [HideInInspector] public bool IsCharging = false;
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _anim;
    // private void Start() {
    //     _enemyAI = GetComponent<EnemyAI>();
    // }
    public override void Attack()
    {
        if (_enemyData.IsRange) {
            if (!_isShooting) {
                StartCoroutine(ShootCoroutine());
            }
        }
        else {
            if (_isHound) {
                if (!IsCharging) {
                    StartCoroutine(ChargeCoroutine());
                }
            }
        }
    }

    private IEnumerator ShootCoroutine()
    {
        _isShooting = true;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (_enemyData.Stagger) {
            timeBetweenProjectiles = _enemyData.TimeBetweenBurst / _enemyData.ProjectilePerBurst;
        }

        for (int i = 0; i < _enemyData.BurstCount; i++)
        {
            if (!_enemyData.Oscillate) {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (_enemyData.Oscillate && i % 2 != 1) {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            } 
            else if (_enemyData.Oscillate) {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

            for (int j = 0; j < _enemyData.ProjectilePerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(_enemyData.BulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(_enemyData.BulletMoveSpeed);
                }

                currentAngle += angleStep;

                if (_enemyData.Stagger) {
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }
            }

            currentAngle = startAngle;

            if (!_enemyData.Stagger) {
                yield return new WaitForSeconds(_enemyData.TimeBetweenBurst);
            }
        }

        yield return new WaitForSeconds(_enemyData.RestTime);
        _isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = (new Vector3(PointerInput.x, PointerInput.y, 0) - transform.position).normalized;

        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0f;
        if (_enemyData.AngleSpread != 0.0f)
        {
            angleStep = _enemyData.AngleSpread / (_enemyData.ProjectilePerBurst - 1);
            halfAngleSpread = _enemyData.AngleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle) {
        float x = transform.position.x + _enemyData.StartingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + _enemyData.StartingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        return new Vector2(x, y);
    }

    IEnumerator ChargeCoroutine() {
        IsCharging = true;
        _anim.SetBool("IsCharging", IsCharging);
        _enemyAI.AllowMove = false;
        
        Vector2 targetDirection = (new Vector3(PointerInput.x, PointerInput.y, 0) - transform.position).normalized;
        _rb.velocity = targetDirection * _enemyData.RunMaxSpeed * 2;
        _anim.SetFloat("DirX", targetDirection.x);
        
        yield return new WaitForSeconds(_enemyData.ChargeTime);
        
        _rb.velocity = Vector2.zero;
        IsCharging = false;
        _anim.SetBool("IsCharging", IsCharging);
        _enemyAI.AllowMove = true;
        yield return new WaitForSeconds(_enemyData.RestTime);
    }
}
