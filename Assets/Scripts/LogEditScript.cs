using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogEditScript : MonoBehaviour
{

    public Button ExitButton;
    public TMP_InputField LogTitle;
    public TMP_InputField ScaleText;
    public ColorPicker ColorPicker;


    public LogNodeGO logNodeGO;



    void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LogTitle.onValueChanged.AddListener(delegate
        {
            // logNodeGO.LogNode.Name = LogTitle.text;
            logNodeGO.SetTitle(LogTitle.text);
        });

        ScaleText.onValueChanged.AddListener(delegate
        {
            if (float.TryParse(ScaleText.text, out float scale))
            {
                // logNodeGO.LogNode.Scale = scale;
                logNodeGO.SetScale(scale);
            }
        });

        ColorPicker.onValueChanged+=()=>
        {
            // logNodeGO.LogNode.Color = ColorPicker.SelectedColor;
            logNodeGO.SetColor(ColorPicker.selectedColor);

        };

        ExitButton.onClick.AddListener(delegate
        {
            gameObject.SetActive(false);
        });
    }

    public void ShowLogEdit(LogNodeGO logNodeGO)
    {
        this.logNodeGO = logNodeGO;
        LogTitle.text = logNodeGO.LogNode.Name;
        ScaleText.text = logNodeGO.LogNode.Scale.ToString();

        ColorPicker.SetColor(logNodeGO.LogNode.Color);

        gameObject.SetActive(true);
    }

    // Update is called once per frame

}
