using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchExample : MonoBehaviour
{
    private void OnEnable() => EnhancedTouchSupport.Enable();

    private void Update()
    {
        foreach (var item in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        {
            Debug.Log($"Touch at position: {item.screenPosition}");
        }
    }
}
