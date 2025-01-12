    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AISpawner : MonoBehaviour
    {
        public GameObject[] AIPrefab;
        public int AIsToSpawn;


        private void Start()
        {
           StartCoroutine(Spawn());
        }
        IEnumerator Spawn()
        {
            int count = 0;
            while (count < AIsToSpawn)
            {
                int randomIndex = Random.Range(0, AIPrefab.Length);

                GameObject obj = Instantiate(AIPrefab[randomIndex]);

                Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
                obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<WayPoint>();

                obj.transform.position = child.position;

                yield return new WaitForSeconds(1f);

                count++;
            }
        }
    }
