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

	private void Awake ()
    {
        movement = GetComponent<Movement>();
        stats = GetComponent<Stats>();
        weapon = GetComponentInChildren<weapon>();
        weapon.owner = this;

        Cursor.lockState = CursorLockMode.Locked;
	}
	
	private void FixedUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            DoMeleeSwing();
	}

    void DoMeleeSwing()
    {
        if(!isPerformingAction && currentState == PlayerState.Sad)
        {
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
}

public enum PlayerState
{
    Happy,
    Normal,
    Sad
};