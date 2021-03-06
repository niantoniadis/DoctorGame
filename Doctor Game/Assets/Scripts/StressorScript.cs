using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StressorScript : MonoBehaviour
{

    public GameObject player;
    public GameObject trigger;
    public GameObject pointer;
    public GameObject pillModel;
    public GameObject patientModel;

    [HideInInspector]
    public bool active;
    private int counter;

    void Start()
    {

        active = false;
        counter = 0;
        trigger.SetActive(false);

    }

    void FixedUpdate()
    {
        if (patientModel != null)
        {
            if (!active)
            {
                counter++;
                int rand = Random.Range(0, 1000000);
                if (rand < counter)
                {
                    counter = 0;
                    active = true;
                    trigger.SetActive(true);
                }
            }

            pointer.SetActive(active);
            pointer.transform.position = new Vector3(player.transform.position.x,
                                             player.transform.position.y,
                                             player.transform.position.z)
                                         - DirectionToPlayer();

            pillModel.SetActive(active);
        }
        else
        {
            pillModel.SetActive(false);
            pointer.SetActive(false);
        }
    }

    public void stress()
    {

        if (distanceToPlayer() < 3.0f)
        {
            Stats.UpdateTime(30, false);
            SceneManager.LoadScene("Minigame1");
        }
    }

    public float distanceToPlayer()
    {

        return Mathf.Sqrt(
            Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) * Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) + // a^2
            Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) * Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) + // b^2
            Mathf.Abs(player.transform.position.z - gameObject.transform.position.z) * Mathf.Abs(player.transform.position.z - gameObject.transform.position.z) // a^2
        );
    }

    private Vector3 DirectionToPlayer()
    {

        Vector3 direction = new Vector3(player.transform.position.x - gameObject.transform.position.x,
            0,
            player.transform.position.z - gameObject.transform.position.z);

        return direction.normalized;
    }
}
