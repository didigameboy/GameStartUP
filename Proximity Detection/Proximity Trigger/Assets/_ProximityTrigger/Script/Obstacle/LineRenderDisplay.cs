using UnityEngine;
using System.Collections;

public class LineRenderDisplay : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private ColorObstacle colorObstacle;
    private BaseUnit baseUnit;

    public BaseUnit BaseUnit {get { return baseUnit; } }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Inialize(ColorObstacle targetColorObstacle, BaseUnit targetBaseUnit)
    {
        colorObstacle = targetColorObstacle;
        baseUnit = targetBaseUnit;

        gameObject.SetActive(true);
    }


    private void Update()
    {
        lineRenderer.SetPosition(0, colorObstacle.transform.position);
        lineRenderer.SetPosition(1, baseUnit.transform.position);

        lineRenderer.SetColors(colorObstacle.CurrentColor, baseUnit.Color);
    }
}
