using UI;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceCountriesButton : MonoBehaviour
{
    [SerializeField] private ECountries type;
    [SerializeField] private Image choiceIcon;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.interactable = true;
        _button.onClick.AddListener(ChoiceCountries);
        choiceIcon.gameObject.SetActive(false);
    }

    private void ChoiceCountries()
    {
        choiceIcon.gameObject.SetActive(true);
        GameData.Instance.Countries = type;
        WindowsManager.Instance.HideWindow(EWindow.MainMenu, () => WindowsManager.Instance.ShowWindow(EWindow.Flight));
        _button.interactable = false;
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }
}
