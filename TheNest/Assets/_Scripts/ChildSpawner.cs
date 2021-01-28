using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpawner : MonoBehaviour
{
    public GameObject child = null;

    [SerializeField] private float _spawnDelay = 0;

    private void OnEnable()
    {
        GameManager.EndTheGame += StartSpawnRoutine;
    }

    private void OnDisable()
    {
        GameManager.EndTheGame -= StartSpawnRoutine;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void StartSpawnRoutine()
    {
        if (!PlayerEnergyControl.PlayerIsDead && !NestControl.NestIsDead)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_spawnDelay);
        Instantiate(child, transform.position, Quaternion.identity);
    }

}
