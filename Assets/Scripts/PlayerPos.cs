using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        if (gm.checkpoint)
        {
            PreserveGameMaster();
        }
        UpdateGameState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            gm.checkpoint = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PreserveGameMaster()
    {
        transform.position = gm.lastCheckPointPos;
        foreach (var coin in gm.collCoins)
        {
            GameObject.Find(coin).SetActive(false);
        }
        foreach (var key in gm.collKeys)
        {
            GameObject.Find(key).SetActive(false);
        }
    }

    public void UpdateGameState()
    {
        gm.checkpoint = false;
        var jam = GetComponent<PuddleControllerScript>();
        jam.coin = gm.coins;
        jam.key = gm.keys;
        if (jam.key > 0)
        {
            jam.door.GetComponent<SpriteRenderer>().sprite = jam.doorOpened.GetComponent<SpriteRenderer>().sprite;
        }
        jam.SetCoinText();
    }
}