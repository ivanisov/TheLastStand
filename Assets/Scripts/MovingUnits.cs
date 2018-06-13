using UnityEngine;

public class MovingUnits : Unit
{
    public Transform mainTarget;

    private Transform target;
    private Unit targetUnit;
    private float timeToAttack = 0;

    void OnEnable()
    {
        onDead += OnDead;
    }

    void OnDisable()
    {
        onDead -= OnDead;
    }

    private void OnDead()
    {
        baseController.RemoveUnit(this);
        if (unitType == GameManager.UnitType.ENEMY)
        {
            GameManager.Instance.AddGold(gold);
        }
        Destroy(gameObject);
    }

    public void SetTarget(Transform currentTarget)
    {
        target = currentTarget;
        targetUnit = target.GetComponent<Unit>();
        if (targetUnit != null)
        {
            if (targetUnit.unitType == GameManager.UnitType.ENEMY)
            {
                MoveToTarget();
                targetUnit.topSelector.SetActive(true);
            }
            if (downSelector.activeSelf)
            {
                downSelector.SetActive(false);
            }
        }
    }

    private void MoveToTarget()
    {
        if (targetUnit != null && targetUnit.GetHealth() <= 0)
        {
            targetUnit = null;
            target = null;
            SearchTarget();
        }

        if (targetUnit == null)
        {
            SearchTarget();
        }

        if (Vector2.Distance(target.transform.position, transform.position) > (targetUnit != null ? attackRange : 1.5f))
        {
            Vector3 targetPos = target.transform.position - transform.position;
            float distance = targetPos.magnitude;
            Vector3 direction = targetPos / distance;

            transform.Translate(direction * (speed * Time.deltaTime));
        }
        else
        {
            if (targetUnit != null)
            {
                if (targetUnit.unitType != unitType)
                {
                    HitEnemy();
                }
            }
        }
    }

    public void SearchTarget()
    {
        Unit closerEnemy = baseController.enemyBase.GetClosestEnemy(transform.position);

        if (closerEnemy != null)
        {
            // Move to closer enemy
            SetTarget(closerEnemy.transform);
        }
        else
        {
            // Move to main target
            SetTarget(mainTarget);
        }
    }

    private void HitEnemy()
    {
        if (targetUnit != null)
        {
            if (timeToAttack <= 0)
            {
                if (targetUnit.GetHealth() <= 0)
                {
                    target = null;
                    targetUnit = null;
                    timeToAttack = 0;
                }
                else
                {
                    targetUnit.OnDamage(attack);
                }
                timeToAttack = attackSpeed;
            }
            else
            {
                timeToAttack -= Time.deltaTime;
            }
        }
    }

    private void Update()
    {
        if (target != null)
        {
            MoveToTarget();
        }
        else
        {
            SearchTarget();
        }
    }

    public void ClickOnUnit()
    {
        if (unitType == GameManager.UnitType.PLAYER)
        {
            SelectController.Instance.SelectUnit(this);
        }
        else if (unitType == GameManager.UnitType.ENEMY)
        {
            SelectController.Instance.SetTarget(this);
        }
    }
}
