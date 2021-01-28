using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{

    //  IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler 
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private static CanvasGroup canvasGroup;
    public GameObject startSlot;
    public GameObject igneousSlot;
    public GameObject sedimentarySlot;
    public GameObject metamorphicSlot;
    public GameObject parentFolder;
    public GameObject mouse;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public static void GameStart()
    {
        canvasGroup.alpha = 1f;
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0) && mouse != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            if (rectTransform.position != igneousSlot.GetComponent<RectTransform>().position ||
                 rectTransform.position != sedimentarySlot.GetComponent<RectTransform>().position ||
                 rectTransform.position != metamorphicSlot.GetComponent<RectTransform>().position)
            {
                transform.SetParent(parentFolder.transform);
                rectTransform.anchoredPosition = startSlot.GetComponent<RectTransform>().anchoredPosition;
                gameObject.AddComponent<Rigidbody>();
                gameObject.GetComponent<Rigidbody>().mass = 1e-07f;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(mouse.GetComponent<Rigidbody>());

            }
        }
    }


    public void OnTriggerStay(Collider collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouse = collision.gameObject;
            canvasGroup.alpha = .6f;
            transform.SetParent(collision.transform);
            Destroy(gameObject.GetComponent<Rigidbody>());
            collision.gameObject.AddComponent<Rigidbody>();
            collision.GetComponent<Rigidbody>().mass = 1e-07f;
            collision.GetComponent<Rigidbody>().isKinematic = true;
        }

    }
}
