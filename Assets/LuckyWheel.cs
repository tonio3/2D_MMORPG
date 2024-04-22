using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LuckyWheel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private bool isDragging;
    [SerializeField] float dragSpeed = 400f;
    [SerializeField] float rotationSpeed = 10f;
    private Vector2 dragOrigin;
    private RectTransform rectTransform;
    [SerializeField] float weight = 1;
    private Vector2 lastMousePosition;
    private float distance = 0;
    private bool left = false;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    [SerializeField] float maxdelay = 0.15f;
    float delay = 0;

    private void Start()
    {
        delay = maxdelay;
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        dragOrigin = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector3 pos = Camera.main.ScreenToViewportPoint(eventData.position - dragOrigin);
        rectTransform.Rotate(new Vector3(0, 0, pos.x * dragSpeed), Space.World);
        dragOrigin = eventData.position;

        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            lastMousePosition = eventData.position;
            delay = maxdelay;
        }


    }

    private void Update()
    {
        textMeshProUGUI.text = GetNumber() + "";
        if (!isDragging)
        {

            if (!left && distance > 0)
                distance -= Time.deltaTime * weight;

            else if (left && distance < 0)
                distance += Time.deltaTime * weight;

            else
            {
                
                return;
            }
                

            rectTransform.Rotate(new Vector3(0, 0, Time.deltaTime * distance * rotationSpeed), Space.World);
            
        }
    }

    private int GetNumber()
    {
        float angle = rectTransform.eulerAngles.z;

        if (angle < 0)
        {
            angle += 360;
        }

        if (angle < 45 || angle > 315)
        {
            return 1;
        }
        else if (angle < 135)
        {
            return 2;
        }
        else if (angle < 225)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        isDragging = false;

        distance = GetVectorLength(lastMousePosition, eventData.position);
    }

    public float GetVectorLength(Vector2 start, Vector2 end)
    {
        Vector2 direction = end - start;
        float length = direction.magnitude;

        left = false;

        if (Vector2.Dot(direction, end) < 0)
        {
            left = true;
            length = -length;
        }

        return length;
    }
}