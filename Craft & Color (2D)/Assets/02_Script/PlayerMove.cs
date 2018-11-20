using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    //public float playerJump;

    //public float speed = 5;
    public bool jump = true;
    private Animator animator;
    Rigidbody2D rb;
    
	//[SerializeField] private AudioSource se01;

    public float jumpForce = 22f;       // ジャンプ時に加える力
    public float jumpThreshold = 1f;    // ジャンプ中か判定するための閾値
    public float runForce = 1.5f;       // 走り始めに加える力
    public float runSpeed = 0.5f;       // 走っている間の速度
    public float runThreshold = 2.2f;   // 速度切り替え判定のための閾値
    public bool isGround = true;        // 地面と接地しているか管理するフラグ
    public int key = 0;                 // 左右の入力管理

    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存
    float stateEffect = 1;       // 状態に応じて横移動速度を変えるための係数

    

    Vector2 min;
    Vector2 max;

    void Start()
    {
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        min = Camera.main.ViewportToWorldPoint(Vector2.zero);//最小
        max = Camera.main.ViewportToWorldPoint(Vector2.one);//最大

        GetInputKey();          // ① 入力を取得
        ChangeState();          // ② 状態を変更する
        ChangeAnimation();      // ③ 状態に応じてアニメーションを変更する
        Move();                 // ④ 入力に応じて移動する
    }

    void GetInputKey()
    {
        key = 0;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
            key = 1;
             
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
            key = -1;
    }

    void ChangeState()
    {
        // 空中にいるかどうかの判定。上下の速度(rigidbody.velocity)が一定の値を超えている場合、空中とみなす
        if (Mathf.Abs(rb.velocity.y) > jumpThreshold)
        {
            isGround = false;
        }

        // 接地している場合
        if (isGround)
        {
            // 走行中
            if (key == 0)
            {
                state = "IDLE"; //待機状態
            }
            else
            {
                state = "RUN"; ;
            }
            // 空中にいる場合
        }
        else
        {
            // 上昇中
            if (rb.velocity.y > 0)
            {
                state = "JUMP";
                // 下降中
            }
            else if (rb.velocity.y < 0)
            {
                state = "FALL";
            }
        }
    }

    void ChangeAnimation()
    {
        // 状態が変わった場合のみアニメーションを変更する
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
                    transform.localScale = new Vector3(key, 1, 1); // 向きに応じてキャラクターのspriteを反転
                    break;
                default:
                   
                    stateEffect = 1f;
                    break;
            }
            // 状態の変更を判定するために状態を保存しておく
            prevState = state;
        }
    }

    void Move()
    {
        // 設置している時にSpaceキー押下でジャンプ
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) 
            {
                this.rb.AddForce(transform.up * this.jumpForce);
				//se01.PlayOneShot (se01.clip);
                isGround = false;
            }
        }

        // 左右の移動。一定の速度に達するまではAddforceで力を加え、それ以降はtransform.positionを直接書き換えて同一速度で移動する
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

    // 着地判定
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            if (!isGround)
                isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            if (isGround)
                isGround = false;
        }
    }
}
