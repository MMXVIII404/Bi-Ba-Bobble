using UnityEngine;

public class ClickToBounce : MonoBehaviour
{
    private bool isRollable = false;
    public float initialRotationSpeed = 100.0f;
    public float upwardForce = 50.0f;
    public Rigidbody rb;

    public LayerMask clickLayer;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        isRollable = Physics.Raycast(ray, out hit, Mathf.Infinity, clickLayer) && hit.collider.CompareTag("Reward");

        if (Input.GetMouseButtonDown(0) && isRollable)
        {
            hit.collider.gameObject.GetComponent<ClickToBounce>().Bounce();
        }
    }

    private void Bounce()
    {
        rb.isKinematic = false;
        rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
        rb.AddTorque(Random.onUnitSphere * initialRotationSpeed, ForceMode.Impulse);
    }

}
