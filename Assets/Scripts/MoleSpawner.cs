using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mole;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeDestroyMoleScene;
    [SerializeField] private Vector2 tempoSpawnMole;



    public void StartSpawn()
    {
        StartCoroutine(Spawning());
    }

    public void FinishSpawn()
    {
        StopAllCoroutines();
    }


    IEnumerator Spawning()
    {
        while (true)
        {
            int randomSpawn = Random.Range(0, spawnPoints.Length);
            GameObject newMole = Instantiate(mole, spawnPoints[randomSpawn].position, spawnPoints[randomSpawn].rotation);

            Destroy(newMole, timeDestroyMoleScene);

            yield return new WaitForSeconds(Random.Range(tempoSpawnMole.x, tempoSpawnMole.y));
        }
    }
}
