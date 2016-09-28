using UnityEngine;
using System.Collections;

public class PlayeController : MonoBehaviour {


    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    private Transform groundCheck;    //用来检测角色是否在地面上的对象
    private bool grounded = false;
    private Animator anim;
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;

    // Use this for initialization
    void Start () {
        groundCheck =GameObject.FindWithTag("groundCheck").transform;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //更新groundCheck位置
        groundCheck.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
        grounded = Physics2D.Linecast(transform.position, groundCheck.position,LayerMask.GetMask("Ground"));

        //绘制碰撞检测直线
        Debug.DrawLine(transform.position, groundCheck.position, Color.green);

        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
	}

    void Flip()
    {
        facingRight =!facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(h));
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (h * rb.velocity.x < maxSpeed)
        {
            rb.AddForce(Vector2.right * h * moveForce);    //线性输入与输出
        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            Flip();
        }
        if (jump)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
        UnityEngine.Debug.Log(rb.velocity.x);
    }
}
