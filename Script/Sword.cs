using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable hit = collision.GetComponentInParent<IHitable>();
        AudioManager.instance.Play("SwordHit");

        if (hit != null)
        {
            hit.TakeHit (damage);
        }
    }
}
