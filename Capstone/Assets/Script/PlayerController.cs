using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    [SerializeField] private Animator characterAnimation;
    public float jumpForce;
    private Rigidbody rb;
    [HideInInspector] public float move;
    public bool isfacingRight;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isProne;



    void Start()
    {
        characterAnimation = GetComponent<Animator>();
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isfacingRight = true;
        isProne = false;
    }
    public void FixedUpdate()
    {

        move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(move * runSpeed, rb.velocity.y, 0);
        characterAnimation.SetBool("Walk", true);
        if (move > 0 && !isfacingRight)
        {
            Proning();
            isfacingRight = !isfacingRight;
            transform.eulerAngles = new Vector3(0f, 90f, 0f);



            //transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (move < 0 && isfacingRight)
        {
            Proning();
            isfacingRight = !isfacingRight;
            transform.eulerAngles = new Vector3(0f, -90f, 0f);
            //transform.eulerAngles = new Vector3(0f, -180f, 0f); //flip the character on its x axis
        }
        else if (move == 0)
        {
            characterAnimation.SetBool("Walk", false);

        }
        else

            // isProne = false;
            characterAnimation.SetBool("Prone", false);




    }
    public void Proning()
    {
        StartCoroutine(Prone());
        isProne = true;



    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //Vector3 jump = new Vector3(0f, 2f, 0f);
            characterAnimation.SetBool("Jump", true);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    }
    IEnumerator Prone()
    {
        isProne = true;
        characterAnimation.SetTrigger("Prone");
        characterAnimation.Play("Prone");

        yield return new WaitForSeconds(.5f);
        isProne = false;
    }

    #region Checker
    void Flip()
    {
        isfacingRight = !isfacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            isGrounded = false;

        }
        else
            isGrounded = true;
        characterAnimation.SetBool("Jump", false);
        // else isGrounded = false;


    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "wall")
        {
            isGrounded = true;

        }
        // isGrounded = false;



    }
    #endregion


}
