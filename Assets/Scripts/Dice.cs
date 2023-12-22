using UnityEngine;
using UnityEngine.EventSystems;

public class Dice : MonoBehaviour
{
    private bool isRollable = false;
    public float initialRotationSpeed = 100.0f;
    public float upwardForce = 50.0f;
    public Rigidbody rb;

    public LayerMask clickLayer;

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            isRollable = Physics.Raycast(ray, out hit, Mathf.Infinity, clickLayer) && hit.collider.CompareTag("Dice");

            if (Input.GetMouseButtonDown(0) && isRollable)
            {
                hit.collider.gameObject.GetComponent<Dice>().RollDice();
            }
        }
    }

    private void RollDice()
    {
        rb.isKinematic = false;
        rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
        rb.AddTorque(Random.onUnitSphere * initialRotationSpeed, ForceMode.Impulse);
    }

}
