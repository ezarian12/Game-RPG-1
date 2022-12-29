using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUtilities
{
    private Player player;

    private List<Command> commands = new List<Command>();

    public PlayerUtilities(Player player)
    {
        this.player = player;

        commands.Add(new JumpCommand(player, KeyCode.Space));
        commands.Add(new AttackCommand(player, KeyCode.E));
        commands.Add(new WeaponSwapCommand(player, WEAPON.FISTS, KeyCode.Alpha1));
        commands.Add(new WeaponSwapCommand(player, WEAPON.GUN, KeyCode.Alpha2));
        commands.Add(new WeaponSwapCommand(player, WEAPON.SWORD, KeyCode.Alpha3));
    }

    public void HandleInput()
    {
        player.Stats.Direction = new Vector2(Input.GetAxisRaw("Horizontal"), player.Components.RigidBody.velocity.y);

        foreach (Command command in commands)
        {
            if (Input.GetKeyDown(command.Key))
            {
                command.GetKeyDown();
            }

            if (Input.GetKeyUp(command.Key))
            {
                command.GetKeyUp();
            }

            if (Input.GetKey(command.Key))
            {
                command.GetKey();
            }
        }

        //THIS TMP
        if (Input.GetKeyDown(KeyCode.O))
        {
            UIManager.Instance.RemoveLife();
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(player.Components.Collider.bounds.center, player.Components.Collider.bounds.size, 0, Vector2.down, 0.1f, player.Components.GroundLayer);

        if (hit.collider != null && hit.collider.tag == "Platform")
        {
            player.transform.parent = hit.transform;
        }

        else
        {
            player.transform.parent = null;
        }

        return hit.collider != null;
    }

    public void HandleAir()
    {
        if (IsFalling())
        {
            player.Components.Animator.TryPlayAnimation("Body_Fall");
            player.Components.Animator.TryPlayAnimation("Legs_Fall");
        }
    }

    private bool IsFalling()
    {
        if (player.Components.RigidBody.velocity.y < 0 && !IsGrounded())
        {
            return true;
        }

        return false;
    }
}
