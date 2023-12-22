using UnityEngine;
using UnityEngine.EventSystems;

public class DiceController : MonoBehaviour
{
    private bool isInteracting = false;
    private float tiltDuration = 0f;
    private Transform bodyTransform;

    public AnimationCurve AnimationCurve;
    public float rotationSpeed = 1f;

    public AudioSource aah;

    public Transform P0;
    public Transform P1;

    public LayerMask clickLayer;

    private void Start()
    {
        bodyTransform = transform.Find("Body");
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) && !isInteracting)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickLayer) && hit.collider.CompareTag("Character"))
                {
                    StartInteraction();
                }
            }

            if (isInteracting)
            {
                if (aah.isPlaying)
                {
                    aah.Stop();
                }
                aah.Play();

                float t = tiltDuration / 1f;
                float curveValue = AnimationCurve.Evaluate(t);

                bodyTransform.localRotation = Quaternion.Slerp(P0.localRotation, P1.localRotation, curveValue);

                tiltDuration += Time.deltaTime * rotationSpeed;

                if (tiltDuration >= 1f)
                {
                    isInteracting = false;
                }
            }
        }
    }

    private void StartInteraction()
    {
        isInteracting = true;
        tiltDuration = 0f;
    }
}
