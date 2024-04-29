using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed; 
    public float gotHayDestroyDelay; 
    private bool hitByHay; 

    public float dropDestroyDelay; 
    private Collider myCollider; 
    private Rigidbody myRigidbody; 

    private SheepSpawner sheepSpawner;

    public float heartOffset; 
    public GameObject heartPrefab; 

    private int destroy_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

    }

    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);

        hitByHay = true; 
        runSpeed = 0; 

        Destroy(gameObject, gotHayDestroyDelay);

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);

        TweenScale tweenScale = gameObject.AddComponent<TweenScale>(); ; 
        tweenScale.targetScale = 0; 
        tweenScale.timeToReachTarget = gotHayDestroyDelay;
        SoundManager.Instance.PlaySheepHitClip();

        GameStateManager.Instance.SavedSheep();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Hay") && !hitByHay) 
        {
            Destroy(other.gameObject); 
            HitByHay(); 
        }

        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }

        else if (other.CompareTag("Machine"))
        {
            Die();
        }

    }

    private void DestroySheep()
    {
        destroy_count++;
        Destroy(gameObject, dropDestroyDelay);
        if (destroy_count % 2 != 0)
        {
            GameStateManager.Instance.DroppedSheep();
        }
    }

    private void Drop()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        DestroySheep();
        myRigidbody.isKinematic = false; 
        myCollider.isTrigger = false; 
        SoundManager.Instance.PlaySheepDroppedClip();
    }

    private void Die()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        Destroy(gameObject, 0);
        SoundManager.Instance.PlaySheepDroppedClip();
        GameStateManager.Instance.DroppedSheep();
    }


    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }
}
