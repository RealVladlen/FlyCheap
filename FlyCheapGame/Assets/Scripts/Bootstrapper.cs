using System.Collections;
using DG.Tweening;
using UI;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GameInit());
    }
    
    private IEnumerator GameInit()
    {
        DOTween.Init();
        
        yield return new WaitUntil(WindowsManager.Instance.Init);
        
        WindowsManager.Instance.ShowWindow(EWindow.Loading);
        Fader.Instance.HideFade(null);
        
        yield return new WaitForSeconds(5);
        
        Fader.Instance.ShowFade(() =>
        {
            WindowsManager.Instance.HideWindow(EWindow.Loading);
            WindowsManager.Instance.ShowWindow(EWindow.MainMenu);
            Fader.Instance.HideFade(null);
        });
    }
}
