using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody rb;
    // Vector3 movement = new Vector3(1.0f, 0.0f, 0.0f);
    float speed = 10f;
    float jumpForce = 10f;
    int score = 0;
    private Text scoreText;
    float hosrizontalInput;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
    }

    void Update()
    {
        hosrizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector3 move = transform.position + hosrizontalInput * Time.deltaTime * speed * Vector3.right;
        rb.MovePosition(move);
     }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "ScoreAdd")
        {
            //remove it from the scene
            score++;
            scoreText.text = "Points: " + score;
            other.gameObject.SetActive(false);
        }
    }
}
