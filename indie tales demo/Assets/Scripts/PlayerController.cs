using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour {

    //movement and physics variables
    Rigidbody2D rbPlayer;
    float horizontalInput;
    float verticalInput;
    [SerializeField, Range(1f, 20f)] float moveSpeed = 3.0f;

    //attacking
    public Transform attackPoint;
    public LayerMask enemyLayers;
    Weapons currentWeapon = Weapons.noweapon;
    private bool attacking;
    private float attackTimer;
    private float attackCooldown = 0.5f;

    [Header("Crowbar")]
    [SerializeField, Range(0, 100)] int CrowbarAttackDamage = 34;
    [SerializeField, Range(0.1f, 10f)] float CrowbarAttackRange = 0.5f;

    [Header("Sledgehammer")]
    [SerializeField, Range(0, 100)] int SledeghammerAttackDamage = 51;
    [SerializeField, Range(0.1f, 10f)] float SledgehammerAttackRange = 0.5f;

    //animation variables
    Animator currentAnimator;
    string AnimatorCrowbar = "Animation/Crowbar",
           AnimatorNoWeapon = "Animation/NoWeapon",
           AnimatorSledgehammer = "Animation/Sledgehammer";
    Vector2 lookDirection = new Vector2(1, 0);

    void Start() {
        rbPlayer = GetComponent<Rigidbody2D>();
        currentAnimator = GetComponentInChildren<Animator>();                 
        GameManager.Instance.GetComponentInChildren<CinemachineVirtualCamera>().Follow = transform;
        GameManager.Instance.PLayGameStartSound();
        GameManager.Instance.StartTimeCounter();
    }

    void Update() {   
        GameManager.Instance.UpdateTimeCounter();     

        Vector2 move = GetInputs();
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
            SetAtackPoint();
        }
        
        if (attacking) {
            attackTimer -= Time.deltaTime;
            if(attackTimer < 0) {
                attacking = false;
            }
        }        

        if (Input.GetKeyDown(KeyCode.Space) && !attacking) {
            currentAnimator.SetTrigger("Attack");
            attacking = true;
            attackTimer = attackCooldown;
        }

        if (lookDirection.x > 0) {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            GetComponentInChildren<FlashLightController>().FlashRight();
        }
        else if(lookDirection.x < 0) {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
            GetComponentInChildren<FlashLightController>().FlashLeft();
        }
        else if(lookDirection.y < 0) {
            GetComponentInChildren<FlashLightController>().FlashDown();
        }
        else if (lookDirection.y > 0) {
            GetComponentInChildren<FlashLightController>().FlashUp();
        }

        currentAnimator.SetFloat("Look X", lookDirection.x);
        currentAnimator.SetFloat("Look Y", lookDirection.y);
        currentAnimator.SetFloat("Speed", move.magnitude);        
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

    public void AttackCalledbyAnimationEvents() {
        switch (currentWeapon) {
            case Weapons.noweapon:
                break;
            case Weapons.crowbar:
                AttackWithCrowbar();
                break;
            case Weapons.sledgehammer:
                AttackWithSledgeHammer();
                break;
        }
    }

    private void AttackWithCrowbar() {
        Collider2D[] hitWalls = Physics2D.OverlapCircleAll(attackPoint.position, CrowbarAttackRange, enemyLayers);
        foreach (Collider2D wall in hitWalls) {
            wall.GetComponentInParent<TileController>().AttackAtPosition(attackPoint.position, CrowbarAttackDamage);
        }
    }
    private void AttackWithSledgeHammer() {
        Collider2D[] hitWalls = Physics2D.OverlapCircleAll(attackPoint.position, SledgehammerAttackRange, enemyLayers);
        foreach (Collider2D wall in hitWalls) {
            wall.GetComponentInParent<TileController>().AttackAtPosition(attackPoint.position, SledeghammerAttackDamage);
        }
    }

    private void SetAtackPoint() {
        attackPoint.localPosition = lookDirection / 2;
    }

    public void SetWeapon(Weapons weapon) {
        currentWeapon = weapon;
        switch (weapon) {
            case Weapons.noweapon:
                currentAnimator.runtimeAnimatorController = Resources.Load(AnimatorNoWeapon) as RuntimeAnimatorController;
                break;
            case Weapons.crowbar:
                currentAnimator.runtimeAnimatorController = Resources.Load(AnimatorCrowbar) as RuntimeAnimatorController;
                break;
            case Weapons.sledgehammer:
                currentAnimator.runtimeAnimatorController = Resources.Load(AnimatorSledgehammer) as RuntimeAnimatorController;
                break;
        }
    }
}