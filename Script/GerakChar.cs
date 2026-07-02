using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerakChar : MonoBehaviour
{
    public float kecepatan = 5f;
    public float kekuatanLompat = 7f;
    public GameObject uiMenang;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private float gerakanX;
    private bool sedangDiTanah = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Time.timeScale = 1f;
    }

    void Update()
    {
        gerakanX = Input.GetAxis("Horizontal");

        anim.SetFloat("Kecepatan", Mathf.Abs(gerakanX));

        if (gerakanX > 0)
        {
            sr.flipX = false;
        }
        else if (gerakanX < 0)
        {
            sr.flipX = true;
        }

        if (Input.GetButtonDown("Jump") && sedangDiTanah)
        {
            rb.velocity = new Vector2(rb.velocity.x, kekuatanLompat);
            sedangDiTanah = false;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(gerakanX * kecepatan, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            sedangDiTanah = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (uiMenang != null)
            {
                uiMenang.SetActive(true);
            }
            Time.timeScale = 0f;
        }
        
        else if (collision.gameObject.name == "CharJatuh")
        {
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}