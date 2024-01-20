using UnityEngine;

public class Testplayermovement : MonoBehaviour
{
    [SerializeField] private float speed; // TODO Handle with PlayerStats Scriptable Object
    [SerializeField] private float groundDist;
    [SerializeField] private Vector3 moveDir;

    public LayerMask terrainLayer;

    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out var hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveDir = new Vector3(x, 0, y);
        moveDir = Vector3.ClampMagnitude(moveDir, 1f);
        //rigidBody.velocity = moveDir * speed;
        rigidBody.AddForce(moveDir * speed, ForceMode.Force);
        

        // if (x != 0 && x < 0)
        // {
        //     //spriteRenderer.flipX = true;
        // }
        // else if (x != 0 && x > 0)
        // {
        //     //spriteRenderer.flipX = false;
        // }
        
        animator.SetFloat(Horizontal, x);
        animator.SetFloat(Vertical, y);
        animator.SetFloat(Speed, moveDir.magnitude);
    }
}

