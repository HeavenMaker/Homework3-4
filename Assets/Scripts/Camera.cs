using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform m_Transform;
    private Transform player_Transform;
    Vector3 del = new Vector3(0, 40f, -40f);

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        player_Transform = GameObject.Find("Player").GetComponent<Transform>();

    }

    void Update()
    {
       m_Transform.position = player_Transform.position + del;
    }
}
