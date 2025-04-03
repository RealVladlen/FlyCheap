using UI.Windows;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageDragger : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private RectTransform viewportRect;
    [SerializeField] private FlightView flightView;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float smoothSpeed = 10f; 

    private RectTransform _imageRect; 
    private Vector2 _minBounds;
    private Vector2 _maxBounds;
    private Vector2 _lastTouchPosition;
    private Vector2 _targetPosition;

    private void Start()
    {
        _imageRect = GetComponent<RectTransform>();
        CalculateBounds();

        canvas = GetComponentInParent<Canvas>();
        _targetPosition = _imageRect.anchoredPosition;
    }

    private void Update()
    {
        _imageRect.anchoredPosition = Vector2.Lerp(_imageRect.anchoredPosition, _targetPosition, Time.deltaTime * smoothSpeed);
    }

    private void CalculateBounds()
    {
        float viewportWidth = viewportRect.rect.width;
        float viewportHeight = viewportRect.rect.height;

        float imageWidth = _imageRect.rect.width;
        float imageHeight = _imageRect.rect.height;

        _minBounds = new Vector2(viewportWidth - imageWidth, viewportHeight - imageHeight) / 2;
        _maxBounds = -_minBounds;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(viewportRect, eventData.position, canvas.worldCamera, out _lastTouchPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!flightView.GetState) return;

        Vector2 currentTouchPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(viewportRect, eventData.position, canvas.worldCamera, out currentTouchPosition);

        Vector2 delta = currentTouchPosition - _lastTouchPosition;
        _lastTouchPosition = currentTouchPosition;

        Vector2 newPosition = _targetPosition + delta;

        newPosition.x = Mathf.Clamp(newPosition.x, _minBounds.x, _maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, _minBounds.y, _maxBounds.y);

        _targetPosition = newPosition; 
    }
}
