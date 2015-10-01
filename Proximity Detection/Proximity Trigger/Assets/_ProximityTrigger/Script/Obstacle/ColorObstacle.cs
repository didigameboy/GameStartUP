using System.Collections.Generic;
using UnityEngine;

public class ColorObstacle : MonoBehaviour
{
    [SerializeField] private TriggerVolume triggerVolume;
    [SerializeField] private LineRenderDisplay lineRenderDisplayPrefab;

    private new Renderer renderer;
    private List<LineRenderDisplay> lineRenderDisplays = new List<LineRenderDisplay>();
    private Color currentColor;

    public Color CurrentColor {get { return currentColor; } }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        lineRenderDisplayPrefab.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        triggerVolume.TriggerEnterAction += OnVolumeTriggerEnter;
        triggerVolume.TriggerExitAction += OnVolumeTriggerExit;
    }

    private void OnDisable()
    {
        triggerVolume.TriggerEnterAction -= OnVolumeTriggerEnter;
        triggerVolume.TriggerExitAction -= OnVolumeTriggerExit;
    }

    private void OnVolumeTriggerExit(TriggerVolume triggerVolume, Collider collider)
    {
        BaseUnit targetBaseUnit = collider.gameObject.GetComponent<BaseUnit>();


        LineRenderDisplay currentLineRenderDisplay = GetLineRenderOf(targetBaseUnit);

        lineRenderDisplays.Remove(currentLineRenderDisplay);
        Destroy(currentLineRenderDisplay.gameObject);
        RemoveColor(targetBaseUnit.Color);
    }

    private void OnVolumeTriggerEnter(TriggerVolume triggerVolume, Collider collider)
    {
        BaseUnit targetBaseUnit = collider.gameObject.GetComponent<BaseUnit>();

        LineRenderDisplay lineRenderDisplayClone = Instantiate(lineRenderDisplayPrefab);
        lineRenderDisplayClone.transform.parent = this.transform;
        lineRenderDisplayClone.transform.localPosition = Vector3.zero;
        lineRenderDisplayClone.transform.localRotation = Quaternion.identity;

        lineRenderDisplayClone.Inialize(this, targetBaseUnit);

        lineRenderDisplays.Add(lineRenderDisplayClone);

        AddColor(targetBaseUnit.Color);
    }

    private LineRenderDisplay GetLineRenderOf(BaseUnit targetBaseUnit)
    {
        for (int i = 0; i < lineRenderDisplays.Count; i++)
        {
            LineRenderDisplay lineRender = lineRenderDisplays[i];
            if (lineRender.BaseUnit == targetBaseUnit)
                return lineRender;
        }
        return null;
    }

    private void AddColor(Color targetColor)
    {
        currentColor = renderer.material.color;
        currentColor.r += targetColor.r;
        currentColor.g += targetColor.g;
        currentColor.b += targetColor.b;

        currentColor.r = Mathf.Clamp(currentColor.r, 0, 255);
        currentColor.g = Mathf.Clamp(currentColor.g, 0, 255);
        currentColor.b = Mathf.Clamp(currentColor.b, 0, 255);

        renderer.material.color = currentColor;
    }

    private void RemoveColor(Color targetColor)
    {
        currentColor = renderer.material.color;
        currentColor.r -= targetColor.r;
        currentColor.g -= targetColor.g;
        currentColor.b -= targetColor.b;

        currentColor.r = Mathf.Clamp(currentColor.r, 0, 255);
        currentColor.g = Mathf.Clamp(currentColor.g, 0, 255);
        currentColor.b = Mathf.Clamp(currentColor.b, 0, 255);

        renderer.material.color = currentColor;
    }
}
