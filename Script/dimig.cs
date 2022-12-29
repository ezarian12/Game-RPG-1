using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dimig : MonoBehaviour, ICollisionHandler
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CollisionEnter(string colliderName, GameObject other)
    {
        if (colliderName == "dimig" && other.tag == "Player")
        {
            other.GetComponent<Player>().Actions.TakeHit1();
        }
    }

    public void CollisionExit(string colliderName, GameObject other)
    {
        if (colliderName == "dimig" && other.tag == "Player")
        {
        }
    }
}
