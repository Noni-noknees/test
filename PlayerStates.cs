using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public Transform player;
    public float health = 50f;
    public float maxHealth = 50f;
    public float hpPerSecond = 1f; //for player enhanced state health regeneration
    public float power = 3f;
    public float starveTime = 3 * 60f; //time limit to starvation state if player hasnt collected any pages within 3 minutes
    public float enhanceDuration = 5 * 60f; //time limit of enhanced state
    private float enhanceTime;
    private int collectedPages = 0;
    private int numCollectedTreasures = 0;
    private float timeSinceLastPage = 0f; //time since player has collected a page;
    private PlayerState currentState;
    public enum PlayerState { Healthy, Starved, Enhanced };
    public bool Sprint = false;
    public bool DoubleJump = false;

    public float speed = 3.0f;
    public float jumpHeight = 1.0f;
    public float gravity = -9.81f;


    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.Healthy;
    }

    // Update is called once per frame
    void Update()
    {
        //tracks time since the last page was collected
        if (currentState != PlayerState.Starved)
        {
            timeSinceLastPage += Time.deltaTime;
        }

        switch (currentState)
        {
            case PlayerState.Healthy:
                Healthy();
                Debug.Log("Healthy state is running.");
                if (timeSinceLastPage >= starveTime)
                {
                    currentState = PlayerState.Starved;
                    Debug.Log("Player has entered starved state.");
                }

                else if (collectedTreasure() > 0)
                {
                    currentState = PlayerState.Enhanced;
                    Debug.Log("Player has entered enhanced state.");
                }
                break;

            case PlayerState.Starved:
                Starved();
                Debug.Log("Starved state is running.");
                if (CollectedPage() > 1)
                {
                    currentState = PlayerState.Healthy;
                    Debug.Log("Player has entered healthy state");
                }
                break;

            case PlayerState.Enhanced:
                Enhanced();
                Debug.Log("Enhanced state is running.");
                if (enhanceTime <= 0)
                {
                    if (timeSinceLastPage >= starveTime)
                    {
                        currentState = PlayerState.Starved;
                        Debug.Log("Player has entered starved state.");
                    }
                    else
                    {
                        currentState = PlayerState.Healthy;
                        Debug.Log("Player has entered healthy state.");
                    }
                }

                break;
        }
    }

    private void Healthy()
    {
        Sprint = true;

    }
    private int collectedTreasure()
    {
        numCollectedTreasures++;
        return numCollectedTreasures;
    }

    private void Starved()
    {
        Sprint = false;
        DoubleJump = false;

        health -= Time.deltaTime * 2f;
        power -= Time.deltaTime * 1f;
        if (power <= 0)
        {
            power = 0;
        }
        if (health <= 0)
        {
            health = 0;
            //game over screen
        }
    }

    private int CollectedPage()
    {
        collectedPages++;
        timeSinceLastPage = 0f;
        return collectedPages;
    }

    private void Enhanced()
    {
        DoubleJump = true;
        Sprint = true;
        health += hpPerSecond * Time.deltaTime;
        if (health > maxHealth)
        {
            health = 50f;
        }
        if (health < 0)
        {
            health = 0;
        }
    }
}
