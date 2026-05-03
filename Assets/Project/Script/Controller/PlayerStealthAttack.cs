using UnityEngine;

public class PlayerStealthAttack : MonoBehaviour
{
    [Header("Setup")]
    public LayerMask enemyLayer;
    public float attackRange = 1.5f;

    [Header("Backstab Tuning")]
    public float backArcAngle = 95f; // more forgiving
    public float graceTime = 0.2f;

    private float graceTimer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryBackstab();
        }
    }

    void TryBackstab()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (var col in enemies)
        {
            EnemyAi ai = col.GetComponentInParent<EnemyAi>();

            if (ai != null && CanBackstab(ai.transform))
            {
                Debug.Log("✅ BACKSTAB SUCCESS on " + ai.name);

                ai.Die();
                return; // 🔥 CRITICAL
            }
        }

        Debug.Log("❌ No valid backstab");
    }

    bool CanBackstab(Transform enemy)
    {
        Vector2 toPlayer = (transform.position - enemy.position).normalized;

        // Enemy forward direction
        Vector2 forward = enemy.up;

        float angle = Vector2.Angle(forward, toPlayer);
        float distance = Vector2.Distance(transform.position, enemy.position);

        bool inBackArc = angle > backArcAngle;
        bool inRange = distance <= attackRange;

        // Grace window (important for moving enemy)
        if (inBackArc)
            graceTimer = graceTime;
        else
            graceTimer -= Time.deltaTime;

        bool inGrace = graceTimer > 0f;

        // DEBUG
        Debug.DrawLine(enemy.position, enemy.position + (Vector3)forward * 2f, Color.blue);
        Debug.DrawLine(enemy.position, transform.position, Color.green);
        Debug.Log($"Enemy:{enemy.name} angle:{angle:F1} dist:{distance:F2} backArc:{inBackArc} grace:{inGrace}");

        return inRange && (inBackArc || inGrace);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}