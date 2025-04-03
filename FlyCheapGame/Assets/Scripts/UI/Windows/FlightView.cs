using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class FlightView : WindowView
    {
        [SerializeField] private Button back;
        [SerializeField] private Button open;
        [SerializeField] private List<GameObject> points;
        [SerializeField] private RectTransform frame;

        private const float TweenDuration = 1f;
        private bool _backState;
        private readonly Vector2 _closeSize = new Vector2(385, 250);
        private readonly Vector2 _openSize = new Vector2(2500, 2500);
        private Tween _sizeTween;

        public bool GetState => _backState;

        private void OnEnable()
        {
            Show();
            UpdatePointsView();
            back.onClick.AddListener(Back);
            open.onClick.AddListener(Open);
            back.interactable = true;
        }

        private void UpdatePointsView()
        {
            foreach (var point in points) 
                point.SetActive(false);

            if ((int)GameData.Instance.Countries < points.Count)
                points[(int)GameData.Instance.Countries].SetActive(true);
        }

        private void Open()
        {
            if (_backState) return;

            _backState = true;
            ScaleFrame();
        }

        private void Back()
        {
            if (_backState)
            {
                _backState = false;
                ScaleFrame();
            }
            else
            {
                WindowsManager.Instance.HideWindow(EWindow.Flight, () => WindowsManager.Instance.ShowWindow(EWindow.MainMenu));
                back.interactable = false;
            }
        }

        private void ScaleFrame()
        {
            _sizeTween?.Kill();
            Vector2 targetSize = _backState ? _openSize : _closeSize;

            _sizeTween = frame.DOSizeDelta(targetSize, TweenDuration);
        }

        private void OnDisable()
        {
            back.onClick.RemoveListener(Back);
            open.onClick.RemoveListener(Open);
        }
    }
}
