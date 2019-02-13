using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCtrl : MonoBehaviour {

    private GameObject Camera;

	// Use this for initialization
	void Start () {
        Camera = GameObject.Find("Main Camera").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Camera.GetComponent<CameraMove>().zoomOut();
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Camera.GetComponent<CameraMove>().defaultAngle();
        }

    }
}
