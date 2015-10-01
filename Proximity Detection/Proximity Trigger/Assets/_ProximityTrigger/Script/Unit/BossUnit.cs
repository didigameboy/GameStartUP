using System;
using UnityEngine;
using System.Collections;

public class BossUnit : MonoBehaviour
{
    [SerializeField] private TriggerVolume triggerVolume;

    private Transform targetToFollow;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        triggerVolume.TriggerEnterAction += OnTriggerVolumeEnter;
        triggerVolume.TriggerExitAction += OnTriggerVolumeExit;
    }
    private void OnDisable()
    {
        triggerVolume.TriggerEnterAction -= OnTriggerVolumeEnter;
        triggerVolume.TriggerExitAction -= OnTriggerVolumeExit;
    }

    private void OnTriggerVolumeExit(TriggerVolume arg1, Collider arg2)
    {
        if (targetToFollow != null && arg2.transform == targetToFollow)
            targetToFollow = null;
    }

    private void OnTriggerVolumeEnter(TriggerVolume triggerVolume, Collider collider)
    {
        targetToFollow = collider.transform;
    }

    private void Update()
    {
        if(targetToFollow == null)
            return;

        navMeshAgent.SetDestination(targetToFollow.position);
    }
}
