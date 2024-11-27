using UnityEngine;

public class SpinnerBehaviour : MonoBehaviour
{
    #region PRIVATE_VARS
    [SerializeField] private float spinForceMultiplier = 500f; // Multiplier for spin force
    
    private Rigidbody rb;
    private Camera mainCamera;

    private Vector3 previousMousePosition;
    private float previousInputTime;
    private bool isDragging = false;
    private float lastAppliedForceTime = 0;
    private float thresholdTimeToReapplyForce = 1;
    #endregion

    #region UNITY_CALLBACKS
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleMouseInput();
    }
    #endregion

    #region PRIVATE_METHODS
    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            previousMousePosition = Input.mousePosition;
            previousInputTime = Time.time;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentViewPortPoint = mainCamera.ScreenToViewportPoint(Input.mousePosition);
            Vector2 previousViewPortPoint = mainCamera.ScreenToViewportPoint(previousMousePosition);
            float movedAmount = Vector3.Distance(currentViewPortPoint, previousViewPortPoint);
            float timeDifference = Time.time - previousInputTime;
            float swipeSpeed = movedAmount / timeDifference;

            if (movedAmount > 0 && (Time.time - lastAppliedForceTime) > thresholdTimeToReapplyForce)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
                {
                    ApplySpinForce(hit.point, (Input.mousePosition - previousMousePosition).normalized, swipeSpeed);
                    lastAppliedForceTime = Time.time;
                }
            }

            previousMousePosition = Input.mousePosition;
            previousInputTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void ApplySpinForce(Vector3 touchPoint, Vector3 direction, float swipeSpeed)
    {
        Vector3 worldDirection = new Vector3(direction.x, 0, direction.y);
        rb.angularVelocity = Vector3.zero;
        rb.AddForceAtPosition(worldDirection * swipeSpeed * spinForceMultiplier, touchPoint, ForceMode.Impulse);
    }
    #endregion
}
