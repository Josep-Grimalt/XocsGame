using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private EventSystem eventSystem;

    private GameObject lastSelected;

    void Update()
    {
        GameObject current = eventSystem.currentSelectedGameObject;

        if (current && current != lastSelected)
        {
            lastSelected = current;

            if (current.transform.IsChildOf(content))
                ScrollToSelected(current);
        }
    }

    private void ScrollToSelected(GameObject selected)
    {
        RectTransform selectedTransform = selected.GetComponent<RectTransform>();

        if (!selectedTransform) return;

        float scrollPos = Mathf.Clamp01((selectedTransform.localPosition.y - content.rect.yMin) / content.rect.height);

        scrollRect.verticalNormalizedPosition = scrollPos;
    }
}
