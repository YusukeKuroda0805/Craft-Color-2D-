using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    public GameObject GoalWall;        //　Inspectorから消したいオブジェクトを入れます
    public bool Reverse = false;

    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (Reverse) GoalWall.gameObject.SetActive(true);
        GoalWall.gameObject.SetActive(false);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (Reverse) GoalWall.gameObject.SetActive(false);
        else GoalWall.gameObject.SetActive(true);
    }

}
