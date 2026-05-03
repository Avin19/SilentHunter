using UnityEngine;
using Pathfinding;

public class EnemyAi : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject visionCone;
    public GameObject alertIcon;

    [Header("Detection")]
    public float detectionRange = 5f;
    public float viewAngle = 60f;
    public float chaseDuration = 5f;

    [Header("Search")]
    public float waitAtLastPosition = 2f;

    [Header("Attack")]
    public int damage = 5;
    public float attackCooldown = 1f;

    private Patrol patrol;
    private AIDestinationSetter chase;
    private AIPath aiPath;
    private PlayerHealth playerHealth;
    private SpriteRenderer coneSR;

    private UnitState currentState;

    private float attackTimer;
    private float chaseTimer;
    private float waitTimer;

    private Vector3 lastKnownPosition;

    void Start()
    {
        patrol = GetComponent<Patrol>();
        chase = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();

        if (player != null)
            playerHealth = player.GetComponent<PlayerHealth>();

        if (visionCone != null)
            coneSR = visionCone.GetComponent<SpriteRenderer>();

        ChangeState(UnitState.Patrol);
    }

    void Update()
    {
        DetectPlayer();
        HandleAttack();
        HandleSearch();

        if (visionCone != null)
            visionCone.transform.rotation = transform.rotation;
    }

    // ---------------- DETECTION ----------------
    void DetectPlayer()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        Vector2 dir = (player.position - transform.position).normalized;
        float angle = Vector2.Angle(transform.up, dir);

        if (distance < detectionRange && angle < viewAngle)
        {
            // 🔥 Player seen
            lastKnownPosition = player.position;
            chaseTimer = chaseDuration;

            ChangeState(UnitState.Chasing);
        }
        else
        {
            HandleChaseTimeout();
        }
    }

    void HandleChaseTimeout()
    {
        if (currentState == UnitState.Chasing)
        {
            chaseTimer -= Time.deltaTime;

            if (chaseTimer <= 0f)
            {
                ChangeState(UnitState.Search);
            }
        }
    }

    // ---------------- SEARCH ----------------
    void HandleSearch()
    {
        if (currentState != UnitState.Search) return;

        float distance = Vector2.Distance(transform.position, lastKnownPosition);

        // Move to last known position
        aiPath.destination = lastKnownPosition;

        if (distance < 0.2f)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0f)
            {
                ChangeState(UnitState.Patrol);
            }
        }
    }

    // ---------------- ATTACK ----------------
    void HandleAttack()
    {
        if (currentState != UnitState.Chasing) return;
        if (playerHealth == null) return;

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            playerHealth.TakeDamage(damage);
            attackTimer = attackCooldown;
        }
    }

    // ---------------- STATE MACHINE ----------------
    void ChangeState(UnitState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        patrol.enabled = false;
        chase.enabled = false;

        switch (currentState)
        {
            case UnitState.Patrol:
                patrol.enabled = true;
                ShowAlert(false);
                break;

            case UnitState.Chasing:
                chase.target = player;
                chase.enabled = true;

                ShowAlert(true);
                break;

            case UnitState.Search:
                waitTimer = waitAtLastPosition;
                ShowAlert(false);
                break;
        }

        UpdateVisuals();
    }

    // ---------------- VISUAL ----------------
    void UpdateVisuals()
    {
        if (coneSR == null) return;

        switch (currentState)
        {
            case UnitState.Patrol:
                coneSR.color = new Color(1f, 1f, 1f, 0.3f);
                break;

            case UnitState.Chasing:
                coneSR.color = new Color(1f, 0f, 0f, 0.4f);
                break;

            case UnitState.Search:
                coneSR.color = new Color(1f, 1f, 0f, 0.4f); // yellow search
                break;
        }
    }

    void ShowAlert(bool value)
    {
        if (alertIcon != null)
            alertIcon.SetActive(value);
    }

    // ---------------- DIE ----------------
    public void Die()
    {
        Debug.Log("💀 Enemy Died: " + name);
        Destroy(gameObject);
    }
}

public enum UnitState
{
    Idle,
    Patrol,
    Suspicious,
    Chasing,
    Search,
    Death
}