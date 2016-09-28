using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public float spawnTime;
    public float spawnDelay;
    public GameObject[] enmeies;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}
	
	// Update is called once per frame
    void Spawn()
    {
        int enemyIndex = Random.Range(0, enmeies.Length);
        Instantiate(enmeies[enemyIndex], transform.position, transform.rotation);
    }
	void Update () {
	
	}
}
