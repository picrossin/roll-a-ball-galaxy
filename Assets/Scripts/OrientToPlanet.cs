using UnityEngine;

public class OrientToPlanet : MonoBehaviour
{
    [SerializeField] private GameObject planet;

    private void Update()
    {
        transform.up = (transform.position - planet.transform.position).normalized;
    }
}
