using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    private int appleCount;

    [SerializeField]
    private TMP_Text appleText;

    [SerializeField]
    private Transform lifeParent;

    [SerializeField]
    private GameObject lifePrefab;

    private Stack<GameObject> lives = new Stack<GameObject>();

    public void AddApple()
    {
        appleCount++;
        appleText.text = appleCount.ToString();
    }

    public void AddLife(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            lives.Push(Instantiate(lifePrefab, lifeParent));
        }
    }

    public void RemoveLife()
    {
        Destroy(lives.Pop());
    }
}
