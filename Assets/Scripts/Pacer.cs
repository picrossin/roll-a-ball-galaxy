using UnityEngine;

public class Pacer : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    [SerializeField] private PlayerController player;

    private GameObject _parent;
    
    private void Start()
    {
        _parent = new GameObject();
        _parent.transform.position = Vector3.zero;
        transform.parent = _parent.transform;
    }

    private void Update()
    {   
        if (player.IsRunning)
        {
            transform.parent.rotation *= Quaternion.Euler(rotationVector);
        }
    }
}
