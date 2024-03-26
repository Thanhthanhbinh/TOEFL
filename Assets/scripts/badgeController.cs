using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class badgeController : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public GameObject badge;
    public GameObject title;
    private bool mouse_over = false;

    void Update()
    {
        if (mouse_over)
        {
            Debug.Log("Mouse Over");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        title.GetComponentInChildren<TMP_Text>().SetText(badge.name);
        Debug.Log(badge.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        title.GetComponentInChildren<TMP_Text>().SetText("");
        Debug.Log("");
    }
    
}
