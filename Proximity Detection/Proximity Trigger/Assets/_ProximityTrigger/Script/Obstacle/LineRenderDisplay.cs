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
        Color colorBegining = colorObstacle.CurrentColor;
        colorBegining.a = 1.0f;
        Color colorEnd = baseUnit.Color;
        colorEnd.a = 1.0f;
        lineRenderer.SetColors(colorBegining, colorEnd);
    }
}
