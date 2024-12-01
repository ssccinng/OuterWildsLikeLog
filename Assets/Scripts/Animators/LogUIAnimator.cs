using UnityEngine;
using UnityEngine.EventSystems;
public class LogUIAnimator : MonoBehaviour
{
    public static void AnimateScaleIn(GameObject uiElement, float duration = 0.5f)
    {
        var temp = uiElement.transform.localScale;
        // 初始缩放设置为0
        uiElement.transform.localScale = Vector3.zero;
        // 使用LeanTween进行动画，从小变大
        LeanTween.scale(uiElement, temp , duration).setEase(LeanTweenType.easeOutBack);
    }
}