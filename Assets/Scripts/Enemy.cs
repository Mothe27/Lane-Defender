using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject killBox;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private int health = 1;
    public PlayerController playerController;

    private PointManager pointManager;

    public AudioClip HurtClip;
    public AudioClip DeathClip;
    public AudioClip LoseLifeClip;


    private void Start()
    {
        pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, killBox.transform.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("killbox"))
        {
            AudioSource.PlayClipAtPoint(LoseLifeClip, transform.position);
            playerController.LoseLife();
        }else
        
        if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(HurtClip, transform.position);
            HealthLoss();
        }
    }

    private void HealthLoss()
    {
        health--;
        if (health == 0)
        {
            AudioSource.PlayClipAtPoint(DeathClip, transform.position);
            pointManager.UpdateScore(10);
            Destroy(gameObject);
            
        }
    }

    
}
