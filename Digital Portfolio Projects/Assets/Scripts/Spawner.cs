using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject companion;

    [SerializeField] Transform playerRespawn;
    [SerializeField] Transform companionRespawn;

    [SerializeField] float playerTimer = 2.0f;
    [SerializeField] float companionTimer = 2.0f; 
    [SerializeField] float resetTimer = 2.0f; 

    void Update()
    {
        if (player.activeInHierarchy == false) PlayerRespawn(); 
        if (companion.activeInHierarchy == false) CompanionRespawn(); 
    }

    private void PlayerRespawn()
    {
        Debug.Log("Resurecting Player...");
        playerTimer -= Time.deltaTime; 
        if (playerTimer <= 0f)
        {
            Debug.Log("Player Resurected!");
            player.GetComponent<Health>().health = 100;
            player.GetComponent<Health>().isDead = false;
            player.transform.SetPositionAndRotation(playerRespawn.position, playerRespawn.rotation);
            player.SetActive(true);
            playerTimer = resetTimer;
        }
    }

    private void CompanionRespawn()
    {
        Debug.Log("Resurecting Companion...");
        companionTimer -= Time.deltaTime;
        if (companionTimer <= 0f)
        {
            Debug.Log("Companion Resurected!");
            companion.GetComponent<Health>().health = 100;
            companion.GetComponent<Health>().isDead = false;
            companion.transform.SetPositionAndRotation(companionRespawn.position, companionRespawn.rotation);
            companion.SetActive(true);
            companionTimer = resetTimer;
        }
    }
}
