using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public Transform target;
    public GameObject groundPrefab;
    public GameObject[] spawnables;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0, 0, -61), Time.deltaTime * 1000);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject newground = Instantiate(groundPrefab, other.transform.position + new Vector3(0, 0, 150), Quaternion.identity, transform.parent);
        Destroy(other.gameObject);

        int[] spawns = { Random.Range(0, 4), Random.Range(0, 4) };
        for (int i = 0; i < spawns.Length; i++)
        {
            int spawn_number = Random.Range(1, 4);
            if (spawns[i] < 2 || spawn_number == 1)
            {
                Transform spawn_location = newground.transform.GetChild(i).GetChild(Random.Range(0, 3));
                Instantiate(spawnables[spawns[i]], spawn_location.position, spawn_location.rotation, spawn_location);
            }
            else
            {
                ArrayList children = new ArrayList();
                foreach (Transform child in newground.transform.GetChild(i))
                    children.Add(child);

                if (spawn_number == 2)
                    children.RemoveAt(Random.Range(0, 3));

                foreach(Transform spawn_location in children)
                    Instantiate(spawnables[spawns[i]], spawn_location.position, spawn_location.rotation, spawn_location);
            }
        }
    }
}
