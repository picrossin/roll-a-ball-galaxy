using UnityEngine;

public class Pacer : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    
    private GameObject _parent;
    
    private void Start()
    {
        _parent = new GameObject();
        _parent.transform.position = Vector3.zero;
        transform.parent = _parent.transform;
    }

    private void Update()
    {
        transform.parent.rotation *= Quaternion.Euler(rotationVector);
    }
}
