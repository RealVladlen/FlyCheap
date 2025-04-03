using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    
    public ECountries Countries { get; set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
