using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftSwitch : MonoBehaviour {

    public GameObject ColorRift1,ColorRift2;        //　Inspectorから消したいオブジェクトを入れます
    [SerializeField] private string Color;
    public bool Reverse = false;

    private void Start()
    {
        //ColorRift2.gameObject.SetActive(false);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == Color) {
            if (ColorRift2 != null) ColorRift2.gameObject.SetActive(true);
            if(ColorRift1!= null) ColorRift1.gameObject.SetActive(false);
            
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //if (Reverse) ColorRift.gameObject.SetActive(false);
        
    }
}
