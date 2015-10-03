using System;
using System.Collections.Generic;
using UnityEngine;



public class ColorObstacle : MonoBehaviour
{
    [SerializeField] private TriggerVolume triggerVolume;
    [SerializeField] private LineRenderDisplay lineRenderDisplayPrefab;

    private new Renderer renderer;
    private List<LineRenderDisplay> lineRenderDisplays = new List<LineRenderDisplay>();
    private Color targetColor;
    public Color CurrentColor {get { return renderer.material.color; } }

    private Dictionary<Color, float> colorProportion = new Dictionary<Color, float>();


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


        colorProportion[targetBaseUnit.Color] -= 1.0f;

        if (colorProportion[targetBaseUnit.Color] <= 0.0f)
            colorProportion.Remove(targetBaseUnit.Color);

        UpdateColor();
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


        if (!colorProportion.ContainsKey(targetBaseUnit.Color))
            colorProportion.Add(targetBaseUnit.Color, 0.0f);

        colorProportion[targetBaseUnit.Color] += 1.0f;


        UpdateColor();
    }

    private void UpdateColor()
    {
        targetColor = Color.black;

        foreach (KeyValuePair<Color, float> colorProportionKeyValuePair in colorProportion)
        {
            float proportion = colorProportionKeyValuePair.Value/(float)triggerVolume.ContainingCount;

            targetColor.r += colorProportionKeyValuePair.Key.r*proportion;
            targetColor.g += colorProportionKeyValuePair.Key.g*proportion;
            targetColor.b += colorProportionKeyValuePair.Key.b*proportion;
        }
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

    private void Update()
    {
        renderer.material.color = Color.Lerp(renderer.material.color, targetColor, Time.deltaTime*2.0f);
    }
}
