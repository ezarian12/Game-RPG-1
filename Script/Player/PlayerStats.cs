using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats 
{
    public Vector2 Direction { get; set; }

    public float Speed { get; set; }

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private int lives;

    [SerializeField]
    private float immortalityTime;

    private WEAPON weapon;

    public float WalkSpeed
    {
        get
        {
            return walkSpeed;
        }
    }

    public float JumpForce
    {
        get
        {
            return jumpForce;
        }
    }

    public bool Alive
    {
        get { return lives > 0;}
    }

    public WEAPON Weapon { get => weapon; set => weapon = value; }

    public Dictionary<WEAPON , bool> Weapons { get; set; } = new Dictionary<WEAPON, bool>();

    public int Lives { get => lives; set => lives = value;}

    public bool IsImortal { get; set; }

    public float ImmortalityTime { get => immortalityTime; set => immortalityTime = value;}
}

   
    
