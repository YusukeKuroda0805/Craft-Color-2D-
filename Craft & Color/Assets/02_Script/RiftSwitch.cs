using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftSwitch : MonoBehaviour {

    [SerializeField] private GameObject ColorRift1,ColorRift2;        //　Inspectorから消したいオブジェクトを入れます
    [SerializeField] private string Color;
    public bool Reverse = false;

    private void Start()
    {
        ColorRift2.gameObject.SetActive(false);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == Color) {
            ColorRift1.gameObject.SetActive(false);
            ColorRift2.gameObject.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //if (Reverse) ColorRift.gameObject.SetActive(false);
        
    }
}
