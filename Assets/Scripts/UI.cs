using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Transform m_Transform;

    private GameManager GM;

    private GameObject startPanel;
    private GameObject gamePanel;
    private GameObject overPanel;
    private GameObject highScorePanel;

    private Button start_Button;
    private Button highScore_Button;
    private Button reset_Button;
    private Button close_Button;

    public Text gameScore;
    private Text time;
    private Text lastScore;

    private Transform grid_Transform;
    private Text[] highText;



    private AudioSource m_AS;

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        startPanel = m_Transform.Find("StartPanel").gameObject;
        gamePanel = m_Transform.Find("GamePanel").gameObject;
        overPanel = m_Transform.Find("OverPanel").gameObject;
        highScorePanel = m_Transform.Find("HighScoreListList").gameObject;

        start_Button = m_Transform.Find("StartPanel/StartButton").GetComponent<Button>();
        highScore_Button = m_Transform.Find("StartPanel/HighScore").GetComponent<Button>();
        reset_Button = m_Transform.Find("OverPanel/ReStart").GetComponent<Button>();
        close_Button = m_Transform.Find("HighScoreListList/Close").GetComponent<Button>();

        gameScore = m_Transform.Find("GamePanel/Score").GetComponent<Text>();
        gameScore.text = "0";
        time = m_Transform.Find("GamePanel/Time").GetComponent<Text>();
        lastScore = m_Transform.Find("OverPanel/LastScore").GetComponent<Text>();


        grid_Transform = m_Transform.Find("HighScoreListList/Image/Grid").GetComponent<Transform>();
        highText = grid_Transform.GetComponentsInChildren<Text>();

        m_AS = GameObject.Find("Directional light").GetComponent<AudioSource>();

        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.GetInt(i.ToString(), 0);
        }
        highScorePanel.SetActive(false);



        ResetUI();

        start_Button.onClick.AddListener (StartGame );
        reset_Button.onClick.AddListener(ResetUI);
        highScore_Button.onClick.AddListener(OpenHighScore);
        close_Button.onClick.AddListener(CloseHighScore);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log(PlayerPrefs.GetInt(i.ToString()));
            }
        }

        if(GM.gaming)
        {
            UpdateTime();
        }
        if (GM.win)
        {
            m_AS.Stop();
            lastScore.text = gameScore.text;
            UpdateHigh(int.Parse(lastScore.text));
            GM.win = false;
        }
        if (GM.lose)
        {
            m_AS.Stop();
            UpdateHigh(0);
            lastScore.text = "0";
            GM.lose = false;
        }
    }

    private void StartGame()
    {
        m_AS.Play();

        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        overPanel.SetActive(false);

        GM.gaming = true;

    }
    private void ResetUI()
    {
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        overPanel.SetActive(false);

        gameScore.text = "0";
        time.text = "120";
        GM.gaming = false;
        GM.ResetGane();
    }

    public void OverUI()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        overPanel.SetActive(true);
    }

    private void UpdateTime()
    {
        if(float.Parse( time.text) > 0)
        {
            time.text = (float.Parse(time.text)- Time.deltaTime).ToString ();
        }
        else
        {
            GM.gaming = false;
            GM.lose = true ;
            OverUI();
            
        }
    }
    public void UpdateScore()
    {
        gameScore.text = (int.Parse(gameScore.text) + 1).ToString();
    }

    private void OpenHighScore()
    {
        ShowHigh();
        highScorePanel.SetActive(true);
    }
    private void CloseHighScore()
    {
        highScorePanel.SetActive(false);
    }

    private void UpdateHigh(int gameScore)
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
        for (int i = 0; i < temp.Count ; i++)
        {

           Debug.Log(temp[i]);
        }
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), temp[i]);
        }
    }

    private void ShowHigh()
    {
        for (int i = 0; i < 10; i++)
        {
            highText[i].text = PlayerPrefs.GetInt(i.ToString()).ToString();
        }
    }


}
