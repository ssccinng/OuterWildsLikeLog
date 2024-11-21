using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LogMono : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// 是否可以被拖动
    /// </summary>
    public bool CanDrag;
    
    public Button LogMainButton;

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LogUIAnimator.AnimateScaleIn(gameObject, 0.5f);

        LogMainButton.onClick.AddListener(() =>
        {
            LogUIAnimator.AnimateScaleIn(gameObject, 0.5f);

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
