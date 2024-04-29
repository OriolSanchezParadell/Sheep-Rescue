using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float destroy = 0.5f;

    private PowerSpawner powerSpawner;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropSheep"))
        {
            powerSpawner.RemovePowerFromList(gameObject);
            Destroy(gameObject, destroy);
        }
    }

    public void SetSpawner(PowerSpawner spawner)
    {
        powerSpawner = spawner;
    }
}

