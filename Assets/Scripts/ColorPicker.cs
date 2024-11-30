using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Image colorPreview;

    public TMP_InputField ColorString;
    

    public Color selectedColor = Color.white;

    void Start()
    {
        UpdateColor(); // 初始化颜色

        redSlider.onValueChanged.AddListener(delegate { UpdateColor(); });
        greenSlider.onValueChanged.AddListener(delegate { UpdateColor(); });
        blueSlider.onValueChanged.AddListener(delegate { UpdateColor(); });

        ColorString.onValueChanged.AddListener(delegate { UpdateColor(true); });
    }

    public Action onValueChanged;

    public void SetColor (Color color)
    {
        redSlider.value = color.r;
        greenSlider.value = color.g;
        blueSlider.value = color.b;
    }

    public void UpdateColor(bool fromStr = false)
    {
        // 从 Slider 获取颜色值

        if (fromStr) {
            Color color;
            if (ColorUtility.TryParseHtmlString(ColorString.text, out color))
            {
                redSlider.value = color.r;
                greenSlider.value = color.g;
                blueSlider.value = color.b;
            }
        }
        else {
        float r = redSlider.value;
        float g = greenSlider.value;
        float b = blueSlider.value;

        selectedColor = new Color(r, g, b);

        // 更新预览
        if (colorPreview != null)
        {
            colorPreview.color = selectedColor;
        }

        // 更新输入框

        ColorString.text = "#" + ColorUtility.ToHtmlStringRGB(selectedColor);
        }


        // 触发事件
        if (onValueChanged != null)
        {
            onValueChanged.Invoke();
        }

        
    }

    public void ApplyColor(GameObject targetObject)
    {
        // 将颜色应用到目标对象的材质
        Renderer renderer = targetObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = selectedColor;
        }
    }
}
