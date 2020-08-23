using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FruitPickUp : MonoBehaviour
{
    public UnityEvent additionalPickUpRoutine;
    public FruitSpawner spawner = null; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (spawner != null)
            {
                spawner.Activate();
            }

            if (additionalPickUpRoutine != null)
            {
                additionalPickUpRoutine.Invoke();
            }

            Destroy(this.gameObject);
        }
    }

}
