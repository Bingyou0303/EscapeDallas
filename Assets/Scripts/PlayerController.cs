using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Move / Jump")]
    public float moveSpeed = 8f;
    public float jumpForce = 16f;
    public LayerMask groundLayer;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.15f;

    Rigidbody2D rb;
    PlayerControls input;

    Vector2 moveInput;
    bool jumpQueued;
    bool dropQueued;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerControls();

        // === �j�w��J ===
        input.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Gameplay.Move.canceled += _ => moveInput = Vector2.zero;
        input.Gameplay.Jump.performed += _ => jumpQueued = true;
        input.Gameplay.Drop.performed += _ => dropQueued = true;
    }
    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    void FixedUpdate()
    {
        // 1. ��������
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        // 2. �a���P�w
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // 3. ���D
        if (jumpQueued && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);              // �M�����ݳt
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        jumpQueued = false;     // �M����C
    }

    void Update()
    {
        if (dropQueued)
        {
            dropQueued = false;
            StartCoroutine(TempDisableOneWay());
        }
    }

    System.Collections.IEnumerator TempDisableOneWay()
    {
        int oneWay = LayerMask.NameToLayer("OneWay");
        Physics2D.IgnoreLayerCollision(gameObject.layer, oneWay, true);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreLayerCollision(gameObject.layer, oneWay, false);
    }
}

