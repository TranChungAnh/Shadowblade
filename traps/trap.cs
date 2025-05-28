using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class trap : MonoBehaviour
{
    protected Death death;
    protected States states;
    public void Update()
    {
        death = GameObject.Find("Death").GetComponent<Death>();
        states = GameObject.Find("States")?.GetComponent<States>();

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
