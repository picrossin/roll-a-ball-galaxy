using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationDirection;
    [SerializeField] [Range(1f, 100f)] private float speed;
    
    private void Update()
    {
        transform.Rotate(rotationDirection * speed * Time.deltaTime);
    }
}
