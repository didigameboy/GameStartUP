using System;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class TriggerVolume : MonoBehaviour
{
    public Action<TriggerVolume, Collider> TriggerEnterAction;
    public Action<TriggerVolume, Collider> TriggerExitAction;
    public Action<TriggerVolume, Collider> TriggerStayAction;

    [SerializeField] private LayerMask collisionMask = -1;

    private new Collider collider;
    private List<Collider> containingCollider = new List<Collider>(); 


    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider targetCollider)
    {
        if(!IsInLayerMaks(targetCollider.gameObject))
            return;

        if(containingCollider.Contains(targetCollider))
            return;


        containingCollider.Add(targetCollider);


        if (TriggerEnterAction != null)
            TriggerEnterAction(this, targetCollider);

    }
    private void OnTriggerExit(Collider targetCollider)
    {
        if (!IsInLayerMaks(targetCollider.gameObject))
            return;

        if(!containingCollider.Contains(targetCollider))
            return;

        containingCollider.Remove(targetCollider);

        if (TriggerExitAction != null)
            TriggerExitAction(this, targetCollider);
    }
    private void OnTriggerStay(Collider targetCollider)
    {
        if (!IsInLayerMaks(targetCollider.gameObject))
            return;

        if (!containingCollider.Contains(targetCollider))
            return;

        if (TriggerStayAction != null)
            TriggerStayAction(this, targetCollider);
    }

    private bool IsInLayerMaks(GameObject targetGameObject)
    {
        return ((collisionMask.value & (1 << targetGameObject.layer)) > 0);
    }
}
