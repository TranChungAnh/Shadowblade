using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private int amount;
    private GameMamager GM;
    private Collider2D col;
    private bool teleportEnabled = false;

    private void Awake()
    {
        anim.SetBool("Teleport2", true);
        col = GetComponent<Collider2D>();
        //col.enabled = false; 
    }

    private void Start()
    {
        GM = GameObject.Find("GameMamager").GetComponent<GameMamager>();
    }

    //private void Update()
    //{
    //    if (GM.isOpenTeleport && !teleportEnabled)
    //    {
    //        anim.SetBool("Teleport1", false);
    //        anim.SetBool("Teleport2", true);

    //        col.enabled = true; 
    //        teleportEnabled = true;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Part" + amount);
        }
    }
}
