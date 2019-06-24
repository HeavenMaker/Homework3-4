using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Vector3 playerStart;
    private Vector3 monster1Start;
    private Vector3 monster2Start;

    private Transform player ;
    private Transform monster1;
    private Transform monster2;

    private Monster mon1;
    private Monster mon2;

    public bool gaming;
    public bool win;
    public bool lose;

    private UI ui;
    private Transform grid_Transform;
    private int[] highScoreList = new int[10];
    private Text[] highScoreText;

    void Awake()
    {
        playerStart = new Vector3(34, 0, 3);
        monster1Start = new Vector3(455, 0, 108);
        monster2Start = new Vector3(58, 0, 330);

        player = GameObject.Find("Player").GetComponent<Transform>();
        monster1 = GameObject.Find("Monster").GetComponent<Transform>();
        monster2 = GameObject.Find("Monster2").GetComponent<Transform>();


        mon1 =  GameObject.Find("Monster").GetComponent<Monster >();
        mon2 = GameObject.Find("Monster2").GetComponent<Monster>();

        gaming = false;

        
        /*
        ui = GameObject.Find("Canvas").GetComponent<UI>();
        grid_Transform = GameObject.Find("Canvas/HighScoreListList/Image/Grid").GetComponent<Transform>();
        highScoreText = grid_Transform.GetComponentsInChildren<Text>();

        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.GetInt(i.ToString(), 1);
        }
        */

        ResetGane();
    }


    
    public void ResetGane()
    {
        player.position = playerStart;
        monster1.position = monster1Start;
        monster2.position = monster2Start;
        mon1.findPlayer = false;
        mon1.beHurt = false;
        mon1.m_NMA.speed = 5.0f;
        mon1.m_NMA.SetDestination(mon1.target1);
        mon2.findPlayer = false;
        mon2.beHurt = false;
        mon2.m_NMA.speed = 5.0f;
        mon2.m_NMA.SetDestination(mon2.target1);

        win = false;
        lose = false;


    }

    /*
    public void UpdateHighScore(int gameScore)
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            temp.Add(PlayerPrefs.GetInt(i.ToString()));
        }
        temp.Add(gameScore);

        for (int i = 0; i < temp.Count; i++)
        {
            for (int j = i + 1; j < temp.Count; j++)
            {
                if (temp[i] < temp[j])
                {
                    int a = temp[i];
                    temp[i] = temp[j];
                    temp[j] = a;
                }
            }
        }
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), temp[i]);
        }
    }
    public void ShowHighScore()
    {
        for (int i = 0; i < 10; i++)
        {
            highScoreText[i].text = PlayerPrefs.GetInt(i.ToString()).ToString();
        }
    }
    */

}
