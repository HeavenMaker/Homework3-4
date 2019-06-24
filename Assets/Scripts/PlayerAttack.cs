using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;

    private UI ui;

    private AudioSource m_AS;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        ui = GameObject.Find("Canvas").GetComponent<UI>();
        m_AS = gameObject.GetComponent<AudioSource>();
    }

    

    private void OnTriggerStay(Collider coll)
    {
        
        if (coll.gameObject.tag == "Monster" && player.attacking && coll.GetComponent<Monster>().beHurt == false)
        {
            m_AS.Play();
            ui.UpdateScore();
            coll.GetComponent<Monster>().beHurt = true;
        }

    }
    
}
