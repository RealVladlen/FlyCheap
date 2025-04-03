using UI;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : WindowView
{
    [SerializeField] private Image spinnerImage; 
    [SerializeField] private float rotationSpeed = 200f;
    
    void Update()
    {
        if (spinnerImage != null)
            spinnerImage.transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }
}
