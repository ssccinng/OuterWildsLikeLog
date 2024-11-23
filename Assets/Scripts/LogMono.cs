using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LogMono : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    /// <summary>
    /// 是否可以被拖动
    /// </summary>
    public bool CanDrag;
    
    public Button LogMainButton;
    public Animator animator; 
    public TextMeshProUGUI LogTitle;

    public Image backGroundImage;
    public Outline backOutline;

    // public GameObject textMesh;
    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        backOutline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
                backOutline.enabled = false;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LogUIAnimator.AnimateScaleIn(gameObject, 0.5f);

        LogMainButton.onClick.AddListener(() =>
        {
            LogUIAnimator.AnimateScaleIn(gameObject, 0.5f);

        });
        backOutline = backGroundImage.GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
