using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Player owner;
    public int dmg;
    public float pushBack;

    /*private void OnTriggerEnter(Collider col)
    {
        if (owner.doingDamage && col.GetComponent<Stats>() && col != owner.GetComponent<Collider>())
        {
            col.GetComponent<Stats>().TakeDamage(dmg);
            if(col.GetComponent<Rigidbody>())
                col.GetComponent<Rigidbody>().AddForce(transform.forward * pushBack, ForceMode.Impulse);
        }  
    }*/

    private void OnTriggerStay(Collider col)
    {
        if (owner.doingDamage && col.GetComponent<Stats>() && col != owner.GetComponent<Collider>())
        {
            col.GetComponent<Stats>().TakeDamage(dmg);
           if (col.GetComponent<Rigidbody>())
                col.GetComponent<Rigidbody>().AddForce(transform.forward * pushBack, ForceMode.Impulse);
        }
    }
}
