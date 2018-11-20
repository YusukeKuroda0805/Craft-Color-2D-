using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public Vector3 player;       //プレイヤーを格納するGameObject型変数
    public Vector3 firstPosition;
    private Vector3 offset;         //Vector3型の変数
    private Vector3 campos;       //vector3型の変数


    void Start()
    {
        player = GameObject.Find("Player").transform.position;
        firstPosition = player;
        // プレイヤーとカメラのポジションどうしをひいて、位置の差を変数offsetに格納します
        offset = this.transform.position - player;
        //はじめのカメラポジションの高さを所得しておく
        campos.y = GetComponent<Transform>().position.y;
    }

    void LateUpdate()
    {   //プレイヤーの位置が決まった後でカメラ位置を毎回決めます
        //  　毎回、Scene内から“Ball”というオブジェクトを探して変数playerに入れます
        //　この（カメラの）ポジションは　プレイヤーの位置にoffsetに入れられた値を足した場所です
        player = GameObject.Find("Player").transform.position;

        Vector3 fixYpos = new Vector3(player.x, campos.y - offset.y, player.z);

        this.transform.position = fixYpos + offset;

        if(player.x < firstPosition.x)
        {
            
        }
    }
}
