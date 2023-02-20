using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FrogController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    public Sprite leapSprite;
    public Sprite deadSprite;

    private Vector3 spawnPosition;
    private float farthestRow;
    private bool cooldown;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Move(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            Move(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            Move(Vector3.down);
        }
    }

    private void Move(Vector3 direction)
    {
        if (cooldown) {
            return;
        }

        Vector3 destination = transform.position + direction;

        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));

        if (barrier != null) {
            return;
        }

        if (platform != null) {
            transform.SetParent(platform.transform);
        } else {
            transform.SetParent(null);
        }

        if (obstacle != null && platform == null)
        {
            transform.position = destination;
            Respawn();
        }
        else
        {
            if (destination.y > farthestRow)
            {
                farthestRow = destination.y;
            }

            // Start leap animation
            StopAllCoroutines();
            StartCoroutine(Leap(destination));
        }
        GameObject.Find("GameManager").GetComponent<GameManagerController>().AddScoreByWalk();
    }

    private IEnumerator Leap(Vector3 destination)
    {
        Vector3 startPosition = transform.position;

        float elapsed = 0f;
        float duration = 0.125f;

        // Set initial state
        spriteRenderer.sprite = leapSprite;
        cooldown = true;

        while (elapsed < duration)
        {
            // Move towards the destination over time
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Set final state
        transform.position = destination;
        spriteRenderer.sprite = idleSprite;
        cooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool hitObstacle = other.gameObject.layer == LayerMask.NameToLayer("Obstacle");
        bool onPlatform = transform.parent != null;

        if (enabled && hitObstacle && !onPlatform) {
            //Death();
            Respawn();
        }
        /*if (enabled && other.gameObject.tag == "Water" && other.gameObject.tag != "Log")
        {
            transform.position = new Vector3(0f,-10f,0f);
        }*/
    }

    public void Respawn()
    {
        transform.position = new Vector3(0f,-10f,0f);
        GameObject.Find("GameManager").GetComponent<GameManagerController>().KillInScore();
    }
}
