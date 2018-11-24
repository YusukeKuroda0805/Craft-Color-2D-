using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {
   
    public bool jump = true;
    Rigidbody2D rb;
 

    public float jumpForce = 22f;       // ジャンプ時に加える力  Force applied at jump
    public float jumpThreshold = 1f;    // ジャンプ中か判定するための閾値 Threshold for judging whether jumping is in progress
    public float runForce = 1.5f;       // 走り始めに加える力 Power to start running
    public float runSpeed = 0.5f;       // 走っている間の速度 Speed ​​while running
    public float runThreshold = 2.2f;   // 速度切り替え判定のための閾値 Threshold for speed switching determination
    public bool isGround = true;        // 地面と接地しているか管理するフラグ Flag to manage whether it is grounded and ground
    public int key = 0;                 // Left / right input control
    public int keyStatus;
    public bool canHave = false;
    public Collider2D blockCol;
    public Sprite standing, running, jumping;

    string state;                // プレイヤーの状態管理 Player state management
    string prevState;            // 前の状態を保存 Save previous state
    float stateEffect = 1;       // 状態に応じて横移動速度を変えるための係数 A coefficient for changing the lateral movement speed according to the state



    Vector2 min;
    Vector2 max;

    void Start()
    {
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();
        //this.animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        min = Camera.main.ViewportToWorldPoint(Vector2.zero);//最小 minimum
        max = Camera.main.ViewportToWorldPoint(Vector2.one);//最大 maximum

        GetInputKey();          // ① 入力を取得
        ChangeState();          // ② 状態を変更する
        ChangeAnimation();      // ③ 状態に応じてアニメーションを変更する
        Move();                 // ④ 入力に応じて移動する

        Havingblock();  //ブロックを持つためのメソッド
    }

    void GetInputKey()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = running;
        key = 0;

        if (Input.GetAxisRaw("Horizontal") < 0) {
            key = -1;
            keyStatus = key;
        };
       
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            key = 1;
            keyStatus = key;
        };
        
    }

    void ChangeState()
    {
        // 空中にいるかどうかの判定。上下の速度(rigidbody.velocity)が一定の値を超えている場合、空中とみなす
        // Determining whether it is in the air. If the upper and lower speed (rigidbody.velocity) exceeds a certain value, it is regarded as being in the air
        
        if (Mathf.Abs(rb.velocity.y) > jumpThreshold)
        {
            isGround = false;
        }

        // 接地している場合 When it is grounded
        if (isGround)
        {
            // 走行中 Running
            if (key == 0)
            {
                state = "IDLE"; //待機状態 Standby state
                this.gameObject.GetComponent<SpriteRenderer>().sprite = standing;
            }
            else
            {
                state = "RUN"; ;
            }
            // 空中にいる場合 When in the air
        }
        else
        {
            // 上昇中 Rising
            if (rb.velocity.y > 0)
            {
                state = "JUMP";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = jumping;
                // 下降中 Falling
            }
            else if (rb.velocity.y < 0)
            {
                state = "FALL";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = jumping;
            }
        }
    }

    void ChangeAnimation()
    {
        // 状態が変わった場合のみアニメーションを変更する Change animation only when the state changes
        if (prevState != state)
        {
            switch (state)
            {
                case "JUMP":
                    
                    stateEffect = 0.5f;
                    break;
                case "FALL":
                    
                    stateEffect = 0.5f;
                    break;
                case "RUN":
                   
                    stateEffect = 1f;
                    //GetComponent<SpriteRenderer> ().flipX = true;
                    transform.localScale = new Vector3(2 * key , transform.localScale.y, 1); // 向きに応じてキャラクターのspriteを反転 Flip character sprite according to orientation
                    break;
                default:
                   
                    stateEffect = 1f;
                    break;
            }
            // 状態の変更を判定するために状態を保存しておく Save the state to judge the state change
            prevState = state;
        }
    }

    void Move()
    {
        // 設置している時にSpaceキー押下でジャンプ Jump by pressing Space key when installing
        if (isGround)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetButtonDown("Jump")) 
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = jumping;
                this.rb.AddForce(transform.up * this.jumpForce);
				//se01.PlayOneShot (se01.clip);
                isGround = false;
            }
        }

        // 左右の移動。一定の速度に達するまではAddforceで力を加え、それ以降はtransform.positionを直接書き換えて同一速度で移動する
        // Move left and right. Addforce applies force until it reaches a certain speed and after that it transfers transform.position directly and moves at the same speed
        float speedX = Mathf.Abs(this.rb.velocity.x);

        if (speedX < this.runThreshold)
        {
             this.rb.AddForce(transform.right * key * this.runForce * stateEffect); //未入力の場合は key の値が0になるため移動しない
             //transform.localScale = new Vector3(key, 1, 1); // 向きに応じてキャラクターのspriteを反転
        }
        else
        {      
            this.transform.position += new Vector3(runSpeed * Time.deltaTime * key * stateEffect, 0, 0);
            //transform.localScale = new Vector3(key, 1, 1); // 向きに応じてキャラクターのspriteを反転
        }

    }

    void Havingblock()
    {

        if (Input.GetMouseButton(0))//マウス右ボタンをクリック Click the right mouse button
        {
            if (canHave)
            {
                //blockCol.GetComponentInParent<GameObject>();//transform.parent = this.transform;
                Debug.Log(blockCol.GetComponentInParent<GameObject>());
            }
        }
    }

    // 着地判定 Landing determination
    void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.tag == "ground")
        //{
        if (!isGround)
                isGround = true;
        //}


    }

    //private void OnCollisionExit2D(Collision2D col)
    //{
    ///if (col.gameObject.tag == "ground")
    //{
    //if (isGround)
    //isGround = false;
    //}
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canHave == false)
        {

            if (collision.tag == "red" || collision.tag == "blue" || collision.tag == "green")
            {
                canHave = true;
                blockCol = collision;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "red" || collision.tag == "blue" || collision.tag == "green")
        {
            canHave = false;
        }
    }
}
