using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragArea : MonoBehaviour
{
    public int score;
    public Player p;

    private void Awake()
    {
        p = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider col)
    {
        ragdoll r = col.GetComponent<ragdoll>();

        if (r != null && !r.isBeingDragged)
        {
            GameManager.AddScore(score, (int)GameManager.gameManager.killCount);
            if (p.ragdollsNear.Contains(r))
                p.ragdollsNear.Remove(r);
            if (p.currentDraggedEnemy == r)
                p.currentDraggedEnemy = null;
            Destroy(col.transform.root.gameObject);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        ragdoll r = col.GetComponent<ragdoll>();

        if (r != null && !r.isBeingDragged)
        {
            GameManager.AddScore(score, (int)GameManager.gameManager.killCount);
            if (p.ragdollsNear.Contains(r))
                p.ragdollsNear.Remove(r);
            if (p.currentDraggedEnemy == r)

                p.currentDraggedEnemy = null;
            Destroy(col.transform.root.gameObject);
        }
    }
}
