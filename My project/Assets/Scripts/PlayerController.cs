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

        // Congela rotação para evitar que a bolinha tombe sozinha (opcional)
        rb.freezeRotation = true;

        // Referência à câmera principal (para movimento relativo)
        if (Camera.main != null)
            cameraTransform = Camera.main.transform;
        else
            Debug.LogWarning("PlayerController: Nenhuma MainCamera encontrada. Movimento será relativo ao mundo.");
    }

    // Chamado automaticamente pelo PlayerInput (Behavior: Send Messages)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (rb == null) return;

        // Calcula direção do movimento baseado na câmera
        Vector3 moveDirection = Vector3.zero;

        if (cameraTransform != null)
        {
            // Direções relativas à câmera (ignorando pitch)
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
            // Fallback: movimento relativo ao mundo (eixos X e Z)
            moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        }

        // Aplica força apenas se houver input
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Vector3 force = moveDirection * moveSpeed;
            rb.AddForce(force, forceMode);

            // Limita velocidade máxima para evitar aceleração infinita
            if (rb.linearVelocity.magnitude > maxVelocity)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
            }
        }
    }

    // Opcional: feedback visual da direção do movimento
    private void OnDrawGizmosSelected()
    {
        if (rb != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, rb.linearVelocity);
        }
    }
}