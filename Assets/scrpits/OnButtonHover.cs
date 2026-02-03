using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// used unity docs
public class OnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector2 originalSize;
    [SerializeField] private float increaseBy;
    [SerializeField] private float increaseSpeed;
    private Vector2 increaseSize;
    
    RectTransform rt;

    public void Start() {
        rt = GetComponent<RectTransform>();
        increaseSize = originalSize * increaseBy;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // make it bigger
        StartCoroutine(ChangeSize(increaseSize));
        
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // go back to original side
        StartCoroutine(ChangeSize(originalSize));

        
        // gurt: yo
    }

    private IEnumerator ChangeSize(Vector2 targetSize)
{
    Vector2 currSize = rt.sizeDelta;
    float t = 0;
    while (currSize != targetSize)
    {
        yield return new WaitForEndOfFrame();
        t += Time.deltaTime * increaseSpeed;
        t = Mathf.Min(t, 1);
        rt.sizeDelta = Vector2.Lerp(currSize, targetSize, t);
    }
}

}
