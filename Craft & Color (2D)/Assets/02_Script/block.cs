using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour {

    PointEffector2D pe;
    public float attraction = -10; //引力
    public float repulsion = 10; //斥力 

    void Start () {
        this.pe = this.gameObject.GetComponent<PointEffector2D>();
        Debug.Log(pe.forceMagnitude);
    }
	
	void Update () {
        //pe.forceMagnitude = repulsion;
	}

    void OnTriggerEnter2D(Colloder2D colloder)
    {
        if(colloder.tag == this.tag)
        {
            pe.forceMagnitude = attraction;
            Debug.Log("ひきつけ");
        }
        else if (colloder.tag != this.tag)
        {
            pe.forceMagnitude = attraction;
            Debug.Log("反発");
        }
    }

    void OnTriggerExit2D(Colloder2D colloder)
    {
        pe.forceMagnitude = 0;
        Debug.Log("何とも触れてない");
    }
    
    
}
