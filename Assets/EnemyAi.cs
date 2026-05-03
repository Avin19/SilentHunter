using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private UnitState currentState;



    void Start()
    {
        currentState = UnitState.Patrol;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentState = UnitState.Chasing;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case UnitState.Idle:
                //DONothing
                break;
            case UnitState.Patrol:
                GetComponent<Patrol>().enabled = true;
                break;
            case UnitState.Chasing:
                GetComponent<AIDestinationSetter>().enabled = true;
                break;


        }
    }



}

public enum UnitState
{
    Idle,
    Patrol,
    Chasing,
    Firing,
    Death
}
