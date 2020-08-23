using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _spawnTimeRange = Vector2.zero;

    private void OnEnable()
    {
        GameManager.StartTheGame += Activate;
    }

    private void OnDisable()
    {
        GameManager.StartTheGame -= Activate;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        StartCoroutine(SpawnCountDown());
    }

    IEnumerator SpawnCountDown()
    {
        yield return new WaitForSeconds(Random.Range(_spawnTimeRange.x, _spawnTimeRange.y));
        GameObject _pickUp = Instantiate(GameManager.Instance.fruit, transform.position, Quaternion.identity);
        _pickUp.GetComponent<FruitPickUp>().spawner = this;
    }
}
