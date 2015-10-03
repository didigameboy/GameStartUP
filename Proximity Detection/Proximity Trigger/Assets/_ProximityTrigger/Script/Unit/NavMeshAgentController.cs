using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAgentController : MonoBehaviour
{
    public Action OnReachDestination;
    public Action OnCantReachDestination;

    private NavMeshAgent navMeshAgent;

    private int areaMask;
    private bool reachDestination;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        areaMask = navMeshAgent.areaMask;
    }

    public void SetDestination(Vector3 targetDestination)
    {
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(targetDestination, out navMeshHit, 2.0f, areaMask);

        if (!navMeshHit.hit)
            return;

        reachDestination = false;
        navMeshAgent.SetDestination(navMeshHit.position);

        
        StopCoroutine(CheckDestination());
        StartCoroutine(CheckDestination());
    }

    private IEnumerator CheckDestination()
    {
        while (!reachDestination)
        {
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
                    {
                        if (!navMeshAgent.hasPath || Mathf.Abs(navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
                        {
                            reachDestination = true;

                            if (OnReachDestination != null)
                                OnReachDestination();
                        }
                    }
                    else
                    {
                        reachDestination = true;

                        if (OnCantReachDestination != null)
                            OnCantReachDestination();
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void WarpPosition(Vector3 targetPosition)
    {
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(targetPosition, out navMeshHit, 2.0f, areaMask);

        if (!navMeshHit.hit)
            return;

        navMeshAgent.Warp(navMeshHit.position);
    }
}
