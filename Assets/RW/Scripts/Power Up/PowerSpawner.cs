using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
    public bool canSpawn = true;

    public GameObject PowerUpPrefab;
    public List<Transform> powerSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns;

    private List<GameObject> powerList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = powerSpawnPositions[Random.Range(0, powerSpawnPositions.Count)].position;
        GameObject powerup = Instantiate(PowerUpPrefab, randomPosition, PowerUpPrefab.transform.rotation);
        powerList.Add(powerup);
        powerup.GetComponent<PowerUp>().SetSpawner(this);
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void RemovePowerFromList(GameObject powerup)
    {
        powerList.Remove(powerup);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject powerup in powerList)
        {
            Destroy(powerup);
        }

        powerList.Clear();
    }


}
