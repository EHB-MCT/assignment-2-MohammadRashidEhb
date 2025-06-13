using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseToSelect : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        // When mouse hovers over this button, select it
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
