using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour {

    //  IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler 
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public GameObject startSlot;
    public GameObject igneousSlot;
    public GameObject sedimentarySlot;
    public GameObject metamorphicSlot;
    public GameObject parentFolder;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0))
        {
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
            transform.SetParent(collision.transform);
        }
        else
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            if (Input.mousePosition != igneousSlot.GetComponent<RectTransform>().position ||
                 Input.mousePosition != sedimentarySlot.GetComponent<RectTransform>().position ||
                 Input.mousePosition != metamorphicSlot.GetComponent<RectTransform>().position)
            {
                transform.SetParent(parentFolder.transform);
                rectTransform.anchoredPosition = startSlot.GetComponent<RectTransform>().anchoredPosition;
            }
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (rectTransform.position != igneousSlot.GetComponent<RectTransform>().position ||
             rectTransform.position != sedimentarySlot.GetComponent<RectTransform>().position ||
             rectTransform.position != metamorphicSlot.GetComponent<RectTransform>().position)
        {
            rectTransform.anchoredPosition = startSlot.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    /*

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (rectTransform.position != igneousSlot.GetComponent<RectTransform>().position ||
             rectTransform.position != sedimentarySlot.GetComponent<RectTransform>().position ||
             rectTransform.position != metamorphicSlot.GetComponent<RectTransform>().position )
        {
            rectTransform.anchoredPosition = startSlot.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        return;
    }
    */
}
