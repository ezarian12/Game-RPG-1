using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerActions
{
    private Player player;

    public PlayerActions(Player player)
    {
        this.player = player;
    }

    public void Move(Transform transform)
    {
        player.Components.RigidBody.velocity =
            new Vector2(player.Stats.Direction.x *
                player.Stats.Speed *
                Time.deltaTime,
                player.Components.RigidBody.velocity.y);

        if (player.Stats.Direction.x != 0)
        {
            transform.localScale =
                new Vector3(player.Stats.Direction.x > 0 ? 1 : -1, 1, 1);
            player.Components.Animator.TryPlayAnimation("Body_Walk");
            player.Components.Animator.TryPlayAnimation("Legs_Walk");
        }
        else if (player.Components.RigidBody.velocity == Vector2.zero)
        {
            player.Components.Animator.TryPlayAnimation("Body_idle");
            player.Components.Animator.TryPlayAnimation("Legs_idle");
        }
    }

    internal void PickUpWeapon(WEAPON weapon)
    {
        player.Stats.Weapons[weapon] = true;
    }

    public void Jump()
    {
        if (player.Utilities.IsGrounded())
        {
            player
                .Components
                .RigidBody
                .AddForce(new Vector2(0, player.Stats.JumpForce),
                ForceMode2D.Impulse);
            AudioManager.instance.Play("Jump");
            player.Components.Animator.TryPlayAnimation("Legs_Jump");
            player.Components.Animator.TryPlayAnimation("Body_Jump");
        }
    }

    public void Attack()
    {
        player.Components.Animator.TryPlayAnimation("Legs_Attack");
        player.Components.Animator.TryPlayAnimation("Body_Attack");
    }

    public void TrySwapWeapon(WEAPON weapon)
    {
        if (player.Stats.Weapons[weapon] == true)
        {
            player.Stats.Weapon = weapon;
            player.Components.Animator.SetWeapon((int) player.Stats.Weapon);
            SwapWeapon();
        }
    }

    public void SwapWeapon()
    {
        for (int i = 1; i < player.References.WeaponObjects.Length; i++)
        {
            player.References.WeaponObjects[i].SetActive(false);
        }

        if (player.Stats.Weapon > 0)
        {
            player
                .References
                .WeaponObjects[(int) player.Stats.Weapon]
                .SetActive(true);
        }
    }

    public void Shoot(string animation)
    {
        if (animation == "Shoot")
        {
            GameObject go =
                GameObject
                    .Instantiate(player.References.ProjectilePrefab,
                    player.References.GunBarrel.position,
                    Quaternion.identity);

            Vector3 direction = new Vector3(player.transform.localScale.x, 0);

            go.GetComponent<Projectile>().Setup(direction);
        }
    }

    public void TakeHit1()
    {
        if (!player.Stats.IsImortal)
        {
            if (player.Stats.Lives > 0)
            {
                UIManager.Instance.RemoveLife();
                player.Stats.Lives--;
                AudioManager.instance.Play("Hurt");
                player.Components.Animator.TryPlayAnimation("Body_Hurt");
            }

            if (player.Stats.Alive)
            {
                player.StartCoroutine(Immoartality());
            }

            if (!player.Stats.Alive)
            {
                player.Components.Animator.TryPlayAnimation("Body_Die");
                player.Components.Animator.TryPlayAnimation("Legs_Die");
            }
        }
    }

    private IEnumerator Blink()
    {
        while (player.Stats.IsImortal)
        {
            for (int i = 0; i < player.Components.SpriteRenderers.Length; i++)
            {
                player.Components.SpriteRenderers[i].enabled = false;
            }

            yield return new WaitForSeconds(.5f);

            for (int i = 0; i < player.Components.SpriteRenderers.Length; i++)
            {
                player.Components.SpriteRenderers[i].enabled = true;
            }

            yield return new WaitForSeconds(.5f);
        }
    }

    private IEnumerator Immoartality()
    {
        player.Stats.IsImortal = true;
        player.StartCoroutine(Blink());
        yield return new WaitForSeconds(player.Stats.ImmortalityTime);
        player.Stats.IsImortal = false;
    }

    public void Collide(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            collision.GetComponent<ICollectable>().Collect();
        }
    }
}
