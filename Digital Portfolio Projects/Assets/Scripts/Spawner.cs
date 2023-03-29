using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject companion;

    float playerTimer = 0.0f; 
    float companionTimer = 0.0f; 

    void Update()
    {
        if (player.activeInHierarchy == false) PlayerRespawn(); 
        if (companion.activeInHierarchy == false) CompanionRespawn(); 
    }

    private void PlayerRespawn()
    {
        Debug.Log("Resurecting Player...");
        playerTimer += Time.deltaTime; 
        if (playerTimer >= 2.0f)
        {
            Debug.Log("Player Resurected!");
            player.GetComponent<Health>().health = 100; 
            player.SetActive(true);
            playerTimer = 0.0f; 
        }
    }

    private void CompanionRespawn()
    {
        Debug.Log("Resurecting Companion...");
        companionTimer += Time.deltaTime;
        if (companionTimer >= 2.0f)
        {
            Debug.Log("Companion Resurected!");
            companion.GetComponent<Health>().health = 100;
            companion.SetActive(true);
            companionTimer = 0.0f;
        }
    }
}
