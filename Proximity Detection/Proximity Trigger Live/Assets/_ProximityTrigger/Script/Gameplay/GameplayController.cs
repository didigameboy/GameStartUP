using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private int maxUnits;
    [SerializeField] private BaseUnit baseUnitPrefab;

    private void Start()
    {
        for (int i = 0; i < maxUnits; i++)
        {
            BaseUnit baseUnitClone = Instantiate(baseUnitPrefab);
            baseUnitClone.transform.parent = this.transform;
        }
    }

}
