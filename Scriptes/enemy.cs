using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    private Rigidbody2D rb;

    public Transform leftant;
    public Transform rightant;

    //速度
    public float speed;
    private bool faceleft = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        Movenment();
    }

    //敌人移动
    void Movenment(){
        if(faceleft){
            rb.velocity = new Vector2(-speed,rb.velocity.y);
            if(transform.position.x<leftant.position.x){
                transform.localScale = new Vector3(-1,1,1);
                faceleft = false;
            }
        }else {
            rb.velocity = new Vector2(speed,rb.velocity.y);
            if(transform.position.x>rightant.position.x){
                transform.localScale = new Vector3(1,1,1);
                faceleft = true;
            }
        }
    }
}
