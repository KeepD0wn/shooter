using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControoller : MonoBehaviour
{
    CharacterController charter;

    float gravity=0;
    float gravityForce=25f;
    float jumpForce=10f;

    float speed = 5f;
    Vector3 moveVector = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        charter= gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Gravity();        
        Jump();
    }

    private void FixedUpdate()
    {
        
    }

    void Gravity()
    {
        if (!charter.isGrounded)
        {
            gravity -= gravityForce * Time.deltaTime;
        }
        else
        {
            gravity = -1f;

        }
    }   

    private void Move()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vertic = Input.GetAxis("Vertical");

        Vector3 forwardMove = new Vector3(horiz,0,vertic);
        forwardMove = transform.TransformDirection(forwardMove);
        forwardMove *= speed;       
        forwardMove.y = gravity;
       
        charter.Move(forwardMove * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && charter.isGrounded)
        {
            gravity = jumpForce ;
        }
    }

}
