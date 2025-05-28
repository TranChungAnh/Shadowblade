using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 5f;
    public Vector3 direction = Vector3.up;
    private Vector3 startPosition;
    private Death death;
    private States states;
    void Start()
    {
        startPosition = transform.position;

    }

    void Update()
    {
        death = GameObject.Find("Death").GetComponent<Death>();
        states = GameObject.Find("States")?.GetComponent<States>();
        float movement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + direction.normalized * movement;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            death.Die();
            states.Die();

        }
    }
}
