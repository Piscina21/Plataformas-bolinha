using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float maxVelocity = 15f;
    [SerializeField] private ForceMode forceMode = ForceMode.Force;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Transform cameraTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("PlayerController: Nenhum Rigidbody encontrado no GameObject!");
            enabled = false;
            return;
        }

       
        rb.freezeRotation = true;

        if (Camera.main != null)
            cameraTransform = Camera.main.transform;
        else
            Debug.LogWarning("PlayerController: Nenhuma MainCamera encontrada. Movimento será relativo ao mundo.");
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (rb == null) return;

       
        Vector3 moveDirection = Vector3.zero;

        if (cameraTransform != null)
        {
            
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            moveDirection = forward * moveInput.y + right * moveInput.x;
        }
        else
        {
           
            moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        }

        
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Vector3 force = moveDirection * moveSpeed;
            rb.AddForce(force, forceMode);

           
            if (rb.linearVelocity.magnitude > maxVelocity)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (rb != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, rb.linearVelocity);
        }
    }
}