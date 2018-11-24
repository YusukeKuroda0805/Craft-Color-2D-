﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour {

    PointEffector2D pe;
    public float attraction = -10; //引力 Attraction
    public float repulsion = 10; //斥力  Repulsive force

    void Start () {
        //This time we are using "PointEffector2D" component.
        this.pe = this.gameObject.GetComponent<PointEffector2D>(); 
    }
	
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == this.tag)
        {
            pe.forceMagnitude = attraction;
            Debug.Log("Attraction");
        }
        else if (collision.tag != this.tag)
        {
            pe.forceMagnitude = repulsion;
            Debug.Log("Repulsion");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pe.forceMagnitude = 0;
        Debug.Log("No power is working");
    }
    
}