using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenUpgradeMenu : MonoBehaviour
{
    public GameObject upgradesMenu;

    public void ShowObject()
    {
        upgradesMenu.SetActive(true);
    }

    public void HideObject()
    {
        if (!IsPointerOverChildObject())
        {
            upgradesMenu.SetActive(false);
        }
    }

    private bool IsPointerOverChildObject()
    {
        // check if pointer is over this object or any child objects
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // check every UI element that the pointer is over
        foreach (RaycastResult result in results)
        {
            // if the pointer is over a child object, return true
            if (result.gameObject.transform.IsChildOf(transform))
            {
                return true;
            }
        }

        // if we get here, pointer is not over this object or any child objects
        return false;
    }
}
