using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;
    public bool checkpoint = false;
    public List<string> collCoins;
    public List<string> collKeys;
    public int coins;
    public int keys;

    private void Start()
    {
        coins = 0;
        keys = 0;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}