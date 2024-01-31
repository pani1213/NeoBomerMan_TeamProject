using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D myRigidBody;
    Vector2 inputDir;
    RaycastHit2D hit;
    public BoomController boomController;
    float hAxis, VAxis;
    bool isGround = false;
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        VAxis = Input.GetAxisRaw("Vertical");
        inputDir = new Vector2(hAxis, VAxis).normalized;
        if (isGround && Input.GetKeyDown(KeyCode.Space))
            Instantiate(boomController).transform.position = hit.collider.transform.position;
    }
     void FixedUpdate()
    {
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.zero);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.zero, Color.red);
       if (hit.collider != null)
       {
            if (hit.collider.CompareTag("Ground"))
                isGround = true;
            else
                isGround = false;
       }
        myRigidBody.velocity = inputDir * moveSpeed * Time.deltaTime;
    }
}
