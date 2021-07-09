using UnityEngine;

public class PlayerController : MonoBehaviour {
    //audio variables
    private AudioSource audioSource;

    //movement and physics variables
    Rigidbody2D rbPlayer;
    float horizontalInput;
    float verticalInput;
    [SerializeField, Range(1f, 20f)] float moveSpeed = 3.0f;

    //attacking
    public Transform attackPoint;
    public LayerMask enemyLayers;

    [Header("Little Attack")]
    [SerializeField, Range(0, 100)] int attackDamage = 15;
    [SerializeField, Range(0.1f, 10f)] float attackRange = 0.5f;

    //animation variables
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    void Start() {
        rbPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        bool attackAllowed = true;
        Vector2 move = GetInputs();
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
            SetAtackPoint();
        }

        if (Input.GetKeyDown(KeyCode.Space) && attackAllowed) {
            Attack();
        }

        /*
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        */
    }

    // FixedUpdate for Movement
    private void FixedUpdate() {
        Vector2 position = rbPlayer.position;
        position.x = position.x + moveSpeed * horizontalInput * Time.deltaTime;
        position.y = position.y + moveSpeed * verticalInput * Time.deltaTime;

        rbPlayer.MovePosition(position);
    }

    private Vector2 GetInputs() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        return new Vector2(horizontalInput, verticalInput);
    }

    private void PlaySound(AudioClip audioClip) {
        audioSource.PlayOneShot(audioClip);
    }

    private void Attack() {
        Collider2D[] hitWalls = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D wall in hitWalls) {
            wall.GetComponentInParent<TileController>().AttackAtPosition(attackPoint.position);
        }
    }

    private void SetAtackPoint() {
        attackPoint.localPosition = lookDirection / 2;
    }
}