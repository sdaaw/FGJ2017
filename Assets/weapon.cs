using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Player owner;
    public int dmg;

    private void OnTriggerEnter(Collider col)
    {
        if (owner.doingDamage && col.GetComponent<Stats>() && col != owner.GetComponent<Collider>())
            col.GetComponent<Stats>().TakeDamage(dmg);
    }
}
