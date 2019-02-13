using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSquare : MonoBehaviour {

    [SerializeField] private GameObject InvisibleWall;
    private bool Tohave = false;

    void Update()
    {
        if (Input.GetButtonDown("Square") && Tohave)
        {
            InvisibleWall.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Tohave = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Tohave = false;
        }
    }
}


