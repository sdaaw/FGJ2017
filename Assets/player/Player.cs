using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Stats))]
public class Player : MonoBehaviour
{
    public Movement movement;
    public Stats stats;

    public weapon weapon;

    public PlayerState currentState;

    public bool isPerformingAction = false;

    public float meleeTime;
    public bool doingDamage = false;

    public CarryTrigger cA;
    public ragdoll currentDraggedEnemy;

    public List<ragdoll> ragdollsNear;

    public Animator anim;

    private Rigidbody m_rb;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        stats = GetComponent<Stats>();
        weapon = GetComponentInChildren<weapon>();
        cA = GetComponentInChildren<CarryTrigger>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            DoMeleeSwing();
        else if(Input.GetKeyDown(KeyCode.Mouse0))
            DoMeleeSwing2();

        if (Input.GetKeyDown(KeyCode.E) && currentDraggedEnemy == null)
            DragEnemy();
        else if (Input.GetKeyDown(KeyCode.E) && currentDraggedEnemy != null)
            DropEnemy();

        if(currentDraggedEnemy != null)
            currentDraggedEnemy.transform.position = cA.transform.position;

        anim.SetFloat("speed", m_rb.velocity.sqrMagnitude);
    }

    void DoMeleeSwing()
    {
        if (!isPerformingAction && currentState == PlayerState.Sad)
        {
            anim.SetTrigger("melee1");
            StartCoroutine(MeleeTimer(meleeTime));
        }
    }

    void DoMeleeSwing2()
    {
        if (!isPerformingAction && currentState == PlayerState.Sad)
        {
            anim.SetTrigger("melee2");
            StartCoroutine(MeleeTimer(meleeTime));
        }
    }

    IEnumerator MeleeTimer(float meleeTime)
    {
        isPerformingAction = true;
        doingDamage = true;
        yield return new WaitForSeconds(meleeTime);
        isPerformingAction = false;
        doingDamage = false;
    }

    void DragEnemy()
    {
        if (ragdollsNear.Count > 0)
        {
            currentDraggedEnemy = ragdollsNear[0];
            currentDraggedEnemy.isBeingDragged = true;
        }  
    }

    void DropEnemy()
    {
        currentDraggedEnemy.isBeingDragged = false;
        currentDraggedEnemy = null;
    }
}

public enum PlayerState
{
    Happy,
    Normal,
    Sad
};