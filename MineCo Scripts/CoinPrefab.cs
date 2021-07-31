using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPrefab : MonoBehaviour
{
    public float timer;
    public bool isBouncing, isDone;

    public void Update()
    {
        if (isBouncing)
        {
            timer += Time.deltaTime;
            if (timer >= 2f && !isDone)
            {
                isDone = true;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            if (timer >= 5f)
            {
                gameObject.SetActive(false);
            }
        }
        if (transform.localPosition.x > 180 + 8 || transform.localPosition.x < 0f || transform.localPosition.y > 634 + 8 || transform.localPosition.y < 0f)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnDisable()
    {
        isBouncing = false;
        isDone = false;
        timer = 0;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;
    }

    public void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Ground") && !isBouncing)
        {
            isBouncing = true;
        }
    }

    public void Pop()
    {
        if (Random.value >= 0.5)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, Random.Range(3, 9)) * Random.Range(10, 16);
        }
        else gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, Random.Range(3, 9)) * Random.Range(10, 16);
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
