using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CountriesListButton : MonoBehaviour
{
    [SerializeField] private RectTransform  frame;

    private Button _button;
    private readonly float _closedY = -185f; 
    private readonly float _openY = -425f;  
    private readonly float _tweenDuration = 0.3f;
    private bool _openState;
    private Tween _moveTween;

    private void Awake()
    {
        _button = GetComponent<Button>();
        frame.offsetMin = new Vector2(frame.offsetMin.x,-185f);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Choice);
    }

    private void Choice()
    {
        _openState = !_openState;

        _moveTween?.Kill();

        float targetBottom = _openState ? _openY : _closedY;

        _moveTween = DOVirtual.Float(frame.offsetMin.y, targetBottom, _tweenDuration, value =>
        {
            Vector2 offset = frame.offsetMin;
            offset.y = value;
            frame.offsetMin = offset;
        }).SetEase(Ease.OutQuad);
    }
    
    private void OnDisable()
    {
        _moveTween?.Kill();
        _button.onClick.RemoveAllListeners();
    }
}
