using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Transform m_Transform;
    private CharacterController m_CC;
    private Animator m_Animator;

    public bool attacking = false;

    private GameManager GM;
    private UI ui;

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_CC = gameObject.GetComponent<CharacterController>();
        m_Animator = gameObject.GetComponent<Animator>();


        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("Canvas").GetComponent<UI>();

    }

    void Update()
    {
        if(GM.gaming)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(h, 0, v);
            m_CC.SimpleMove(dir * 20);

            if (h * h > 0.01f || v * v > 0.01f)
            {
                m_Animator.SetBool("walk", true);
                m_Transform.rotation = Quaternion.LookRotation(dir);
            }
            else
            {
                m_Animator.SetBool("walk", false);
            }
            if (Input.GetMouseButtonDown(0))
            {
                m_Animator.SetTrigger("attack");
            }
        }

        
        
    }

    
   
    private void Attack()
    {     
        StartCoroutine("Att");
    }
    IEnumerator Att()
    {
        attacking = true;
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "End")
        {
            GM.gaming = false;
            GM.win = true;
            ui.OverUI();
        }
    }

}
