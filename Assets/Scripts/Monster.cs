using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private Transform m_Transform;
    private Rigidbody m_RigidBody;

    public NavMeshAgent m_NMA;

    private Transform player_Transform;

    private GameManager GM;
    private UI ui;

    float count = 0.0f;
    float harmCount = 0.0f;

    public bool findPlayer;
    public bool beHurt;

    public Vector3 target1;
    public Vector3 target2;

    private ParticleSystem m_PS;



    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_RigidBody = gameObject.GetComponent<Rigidbody>();
        m_NMA = gameObject.GetComponent<NavMeshAgent>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("Canvas").GetComponent<UI>();

        player_Transform = GameObject.Find("Player").GetComponent<Transform>();

        m_PS = gameObject.GetComponent<ParticleSystem>();
        
        //target1 = new Vector3(m_Transform .position .x, m_Transform.position.y, 50);
        //target2 = new Vector3(m_Transform.position.x, m_Transform.position.y, 150);

        findPlayer = false;
        beHurt = false;

        //m_NMA.SetDestination(target1);
       // m_NMA.speed = 5;

    }

    void Update()
    {
        if (beHurt) m_PS.Play();
        if(GM.gaming)
        {
            if (findPlayer == false)
            {
                if (Vector3.Distance(m_Transform.position, target1) < 10)
                {
                    m_NMA.SetDestination(target2);
                }
                if (Vector3.Distance(m_Transform.position, target2) < 10)
                {
                    m_NMA.SetDestination(target1);
                }
            }
            if (findPlayer)
            {
                if (count >= 2)
                {
                    count = 0.0f;
                    m_NMA.speed += 1.0f;
                }
                else
                {
                    count += Time.deltaTime;
                }

                if (!beHurt)
                {
                    m_NMA.SetDestination(player_Transform.position);
                }
                else
                {
                    m_NMA.SetDestination(m_Transform.position);
                }

            }
            if (Vector3.Distance(m_Transform.position, player_Transform.position) < 100)
            {
                findPlayer = true;
            }


            if (beHurt)
            {
                if (harmCount < 0.25f)
                {
                    m_Transform.Translate(Vector3.back * 1.0f, Space.Self);
                    harmCount += Time.deltaTime;
                }
                else
                {
                    harmCount = 0.0f;
                    beHurt = false;
                }
            }
        }
        
       
    
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision .gameObject.name == "Player")
        {
            GM.gaming = false;
            GM.lose = true;
            ui.OverUI();
        }
    }

    

}
