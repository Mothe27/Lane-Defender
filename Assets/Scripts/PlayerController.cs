using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    // gun variables
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;

    private Rigidbody2D rb;
    private float mx;
    private float my;

    private float fireTimer;

    private Vector2 mousePos;

    public GameObject[] Hearts;
    public int Lives;

    public GameObject LoseScreen;

    public AudioClip ShootClip;
    public AudioClip LifeClip;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LoseScreen.SetActive(false);
    }

    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x -
            transform.position.x) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetKey("space") && fireTimer <= 0f)
        {

            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

        if (Lives == 0)
        {
            Destroy(GameObject.FindWithTag("player"));
            LoseScreen.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx, my).normalized * speed;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        AudioSource.PlayClipAtPoint(ShootClip, transform.position);
    }

    public void LoseLife()
    {
        Lives--;
        Hearts[Lives].SetActive(false);
        AudioSource.PlayClipAtPoint(LifeClip, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            LoseLife();
        }
    }




}
