using UnityEngine;

// As in the original Asteroids, wraps on the opposite side of the screen if we go off the screen
public class WrapAroundScreen : MonoBehaviour
{
    [SerializeField] float _borderAdjustment = 0.07f; 
    private Vector3 _viewportPosition;
    
    private void Update()
    {
        // Converts world position to viewport position (between 0 and 1)
        _viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (_viewportPosition.x < 0 + _borderAdjustment)
            AdjustPosition(new Vector3(1 - _borderAdjustment, 0, 0));
        else if (_viewportPosition.x > 1 - _borderAdjustment)
            AdjustPosition(new Vector3(-1 + _borderAdjustment, 0, 0));
        else if (_viewportPosition.y < 0 + _borderAdjustment)
            AdjustPosition(new Vector3(0, 1 - _borderAdjustment, 0));
        else if (_viewportPosition.y > 1 - _borderAdjustment)
            AdjustPosition(new Vector3(0, -1 + _borderAdjustment, 0));
    }

    private void AdjustPosition(Vector3 adjustment)
    {
        transform.position = Camera.main.ViewportToWorldPoint(_viewportPosition + adjustment);
    }
}
