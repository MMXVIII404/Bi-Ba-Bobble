using UnityEngine;
using UnityEngine.EventSystems;
public class CameraMove : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 previousMousePosition;
    public LayerMask moveLayer;

    private void Start()
    {
        previousMousePosition = Input.mousePosition;
    }
    void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseMovement = currentMousePosition - previousMousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, moveLayer) && !hit.collider.CompareTag("Character") && !hit.collider.CompareTag("Dice") && !EventSystem.current.IsPointerOverGameObject())
            {
                isDragging = true;
            }
        }

        if (isDragging)
        {
            transform.position -= new Vector3(mouseMovement.x, 0, mouseMovement.y) * Time.deltaTime;
        }

        previousMousePosition = currentMousePosition;
    }
}
