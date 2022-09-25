using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //定义刚体组件
    public Rigidbody2D rb;
    //速度
    public float speed;
    //角色朝向
    public float jumpface;

    public Collider2D coll;
    //获取角色动画器
    public Animator anim;
    public int pig;
    public LayerMask ground;

    private bool ishurt;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!ishurt){
            move();
        }
        switchMove();
    }

    //移动
    void move(){

        float horizonMove = Input.GetAxis("Horizontal");
        float facedierction = Input.GetAxisRaw("Horizontal");
        if(horizonMove!=0){
            rb.velocity = new Vector2(horizonMove * speed,rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(facedierction));
        }
        if(facedierction!=0){
            transform.localScale = new Vector3(facedierction,1,1);
        }
        //跳跃
        if(Input.GetButtonDown("Jump")){
            rb.velocity = new Vector2(rb.velocity.x,jumpface * Time.fixedDeltaTime);
            anim.SetBool("jumping",true);
        }
        
    }

    //降落
    void switchMove(){
        if(anim.GetBool("jumping")){
            if(rb.velocity.y<0){
                anim.SetBool("jumping",false);
            }
        }
        //受伤状态恢复
        if(ishurt){
            anim.SetBool("hurt",true);
            if(Mathf.Abs(rb.velocity.x)< 0.1f){
                anim.SetBool("hurt",false);
                anim.SetBool("idle",true);
                ishurt=false;
            }

        }
        // 
    }

    //吃pig
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "collection"){
            Destroy(other.gameObject);
            pig+=1;
        }
    }
    //杀死敌人
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag=="enemy"){
            if(transform.position.x<collision.gameObject.transform.position.x){
                rb.velocity = new Vector2(-10,rb.velocity.y);
                ishurt = true;
            }else if(transform.position.x>collision.gameObject.transform.position.x){
                rb.velocity = new Vector2(10,rb.velocity.y);
                ishurt = true;
            }
        }
    }
}
