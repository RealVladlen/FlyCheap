using System.Collections;
using DG.Tweening;
using UnityEngine;

public class JumpScaleAnimation : MonoBehaviour
{
    [SerializeField] private float delay = 2f;
    [SerializeField] private float speedJump = 0.125f;
    [SerializeField] private float startJumpScale = 1.2f;
    [SerializeField] private float endJumpScale = 1f;

    private Transform _transform;
    private Tween _tween;

    private void Awake() => _transform = GetComponent<Transform>();

    private void OnEnable()
    {
        _transform.localScale = new Vector3(endJumpScale,endJumpScale,endJumpScale);
        StartCoroutine(JumpScaleLoop());
    }

    private IEnumerator JumpScaleLoop()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            if (_tween != null)
                _tween.Kill();

            _tween = _transform.DOScale(startJumpScale, speedJump).SetUpdate(true);
            _tween.onComplete += () => _transform.DOScale(endJumpScale, speedJump).SetUpdate(true);
            yield return new WaitForSeconds(delay);
        }
    }
    private void OnDisable() => _tween.Kill();
}
