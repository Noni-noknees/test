using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class relocation : MonoBehaviour
{
    public Transform player, location;
    public GameObject playerobj;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerobj.SetActive(false); //cannot  be active when moving
            player.position = location.position;
            playerobj.SetActive(true);
        }
    }
}
