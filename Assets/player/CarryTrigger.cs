using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        ragdoll r = col.GetComponent<ragdoll>();
        if (r != null /*&& !e.stats.isAlive*/)
            GetComponentInParent<Player>().ragdollsNear.Add(r);
    }

    private void OnTriggerExit(Collider col)
    {
        ragdoll r = col.GetComponent<ragdoll>();
        if (r != null /*&& !e.stats.isAlive*/)
            GetComponentInParent<Player>().ragdollsNear.Remove(r);
    }
}