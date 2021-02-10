using UnityEngine;

public class LockPlayer : MonoBehaviour
{
    [SerializeField] private Transform ball;

    private void Update()
    {
        transform.position = ball.position;
        transform.rotation = ball.rotation;
    }
}
