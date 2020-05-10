using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiTank : Tank
{
    public NavMeshAgent agent { get { return GetComponent<NavMeshAgent>(); } }
    public Animator fsm { get { return GetComponent<Animator>(); } }

    public Transform[] waypoints;
    private Vector3[] waypointPositions;
    private int index;

    private void Start()
    {
        waypointPositions = new Vector3[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypointPositions[i] = waypoints[i].position;
        }
    }

    protected override void Move()
    {
        //agent.SetDestination(other.position);
        float distance = Vector3.Distance(transform.position, other.position);
        fsm.SetFloat("distance",distance);
        float distanceFromWaypoint = Vector3.Distance(transform.position, waypointPositions[index]);
        fsm.SetFloat("distanceFromCurrentWaypoint", distanceFromWaypoint);
    }

   internal void Patrol()
    {
        LookAt(other);
        agent.SetDestination(waypointPositions[index]);
    }
    internal void Chase()
    {
        agent.SetDestination(other.position);
    }

    float delayed;
    internal void Shoot()
    {
        if((delayed += Time.deltaTime)> 1f)
        {
            Fire();
            delayed = 0;
        }
        
    }
    internal void LookAt()
    {
        LookAt(other.transform);
    }
    protected override IEnumerator LookAt(Transform other)
    {
        while (Vector3.Angle(turret.forward, (other.position - transform.position)) > 5f)
        {
            turret.Rotate(turret.up, 4f);
            yield return null;
        }

    }

    public void FindNewWaypoint()
    {
        switch (index)
        {
            case 0:
                index = 1;
                break;
            case 1:
                index = 0;
                break;
        }
        agent.SetDestination(waypointPositions[index]);
    }
}
