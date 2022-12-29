using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerStats stats;

    [SerializeField]
    private PlayerComponents components;

    [SerializeField]
    private PlayerReferences references;

    [SerializeField]
    private PlayerUtilities utilities;

    [SerializeField]
    private PlayerActions actions;

    public PlayerComponents Components
    {
        get
        {
            return components;
        }
    }

    public PlayerReferences References
    {
        get
        {
            return references;
        }
    }

    public PlayerStats Stats
    {
        get
        {
            return stats;
        }
    }

    public PlayerActions Actions
    {
        get
        {
            return actions;
        }
    }

    public PlayerUtilities Utilities
    {
        get
        {
            return utilities;
        }
    }

    private Vector3 respawnPoint;

    public GameObject fallDetector;

    private GameObject currentTeleporter;

    public GameObject quizCanvas;

    public GameObject DeadCanvas;

    public Transform keyFollowPoint;

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad (gameObject);

        respawnPoint = transform.position;

        actions = new PlayerActions(this);
        utilities = new PlayerUtilities(this);

        stats.Speed = stats.WalkSpeed;

        stats.IsImortal = false;

        AnyStateAnimation[] animations =
            new AnyStateAnimation[] {
                new AnyStateAnimation(RIG.BODY,
                    "Body_idle",
                    "Body_Attack",
                    "Body_Hurt"),
                new AnyStateAnimation(RIG.BODY,
                    "Body_Walk",
                    "Body_Attack",
                    "Body_Jump",
                    "Body_Hurt"),
                new AnyStateAnimation(RIG.BODY, "Body_Jump", "Body_Hurt"),
                new AnyStateAnimation(RIG.BODY, "Body_Fall", "Body_Hurt"),
                new AnyStateAnimation(RIG.BODY, "Body_Attack", "Body_Hurt"),
                new AnyStateAnimation(RIG.BODY, "Body_Hurt", "Body_Die"),
                new AnyStateAnimation(RIG.BODY, "Body_Die"),
                new AnyStateAnimation(RIG.LEGS, "Legs_idle", "Legs_Attack"),
                new AnyStateAnimation(RIG.LEGS, "Legs_Walk", "Legs_Jump"),
                new AnyStateAnimation(RIG.LEGS, "Legs_Jump"),
                new AnyStateAnimation(RIG.LEGS, "Legs_Fall"),
                new AnyStateAnimation(RIG.LEGS, "Legs_Attack"),
                new AnyStateAnimation(RIG.LEGS, "Legs_Die")
            };

        Components.Animator.AnimationTriggerEvent += Actions.Shoot;

        stats.Weapons.Add(WEAPON.FISTS, true);
        stats.Weapons.Add(WEAPON.GUN, false);
        stats.Weapons.Add(WEAPON.SWORD, false);

        UIManager.Instance.AddLife(stats.Lives);

        components.Animator.AddAnimations (animations);
    }

    // Update is called once per frame
    private void Update()
    {
        if (stats.Alive)
        {
            utilities.HandleInput();
            utilities.HandleAir();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (currentTeleporter != null)
            {
                transform.position =
                    currentTeleporter
                        .GetComponent<Teleporter>()
                        .GetDestination()
                        .position;

                quizCanvas.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            quizCanvas.SetActive(false);
        }

        if (stats.Lives == 0)
        {
            Time.timeScale = 1f;
            DeadCanvas.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (stats.Alive)
        {
            actions.Move (transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        actions.Collide (collision);

        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
            AudioManager.instance.Play("Checkpoint");
        }
        else if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
