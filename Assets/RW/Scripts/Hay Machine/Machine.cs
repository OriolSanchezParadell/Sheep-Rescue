using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public float speed;
    public float horizontalBoundary;
    public GameObject hayBalePrefab; 
    public Transform haySpawnpoint; 
    public float shootInterval; 
    private float shootTimer;

    public Transform modelParent; 

    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;

    private float Time_PowerUp = 10f;
    private float EndTime_PowerUp;
    private bool b_PowerUp = false;

    // Start is called before the first frame update
    void Start()
    {
        LoadModel();
    }

    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject); 

        switch (GameSettings.hayMachineColor) 
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
                break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
                break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateShooting();
        UpdatePowerUp();

    }

    void UpdateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        print(horizontalInput);

        //Moving to the left
        if(horizontalInput < 0 && transform.position.x > -horizontalBoundary)
        {
            transform.Translate(transform.right * (-1) * Time.deltaTime * speed);
        }

        //Moving to the right
        else if (horizontalInput > 0 && transform.position.x < horizontalBoundary) 
        {
            transform.Translate(transform.right * (+1) * Time.deltaTime * speed);
        }

    }

    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime; 

        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space)) 
        {
            shootTimer = shootInterval; 
            ShootHay(); 
        }
    }

    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
        SoundManager.Instance.PlayShootClip();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            PowerUp();
            EndTime_PowerUp = Time.time + Time_PowerUp;
            b_PowerUp = true;
        }
    }

    private void PowerUp()
    {
        shootTimer = shootTimer - 5;
        speed = speed + 5;
    }

    private void UpdatePowerUp()
    {
        if (b_PowerUp==true) 
        {
            if (Time.time >= EndTime_PowerUp)
            {
                shootTimer = shootTimer + 5;
                speed = speed - 5;
                b_PowerUp=false;
            }
        }
    }
}
