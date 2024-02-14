using UnityEngine;

public class RV_IntroBanderoll : MonoBehaviour
{
    Vector3 ogPosition;

    [SerializeField] private float speed = 3;
    [SerializeField] private float offset = 600;

    private void Start()
    {
        ogPosition = transform.position;
        transform.position -= new Vector3(1, 0, 0) * offset;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, ogPosition, speed * Time.deltaTime);
    }
}
