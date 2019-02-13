using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCountCheack : MonoBehaviour
{
    //範囲内のオブジェクトを所得、指定のタグのアイテムの数を数える
    private float tagObjectCount = 0;
    [SerializeField] private string BlockColor;
    [SerializeField] private GameObject YBlock,Gate,Rift;
    private Rigidbody2D Yrg;
    private bool CheackEnd = false,YAddGForse;

    private void Start()
    {
        Rift.SetActive(false);
        Yrg = YBlock.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        if(CheackEnd == false)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 3.0f);

            //範囲以内のオブジェクトをそれぞれ参照する
            foreach (Collider2D obj in targets)
            {

                if ((obj.tag == BlockColor))
                {
                    tagObjectCount++;
                    if (tagObjectCount == 10)//一つのオブジェクトにつき「２」加算されてしまうため、この数にした
                    {
                        BlockMove();
                        Debug.Log("赤いブロックの数が指定数に達しました");
                        CheackEnd = true;
                    }

                }
                Debug.Log(tagObjectCount);
            }
            tagObjectCount = 0;
        }
        if(YAddGForse) Yrg.AddForce(new Vector2(60, 0), ForceMode2D.Force);
    }

    void BlockMove()
    {

        YAddGForse = true;
        Invoke("GateOpen", 1.0f);//n秒後に処理
    }

    void GateOpen()
    {
        Gate.SetActive(false);
        Rift.SetActive(true);
    }
}
