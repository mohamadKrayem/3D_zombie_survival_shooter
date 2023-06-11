using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoreBlast : MonoBehaviour
{
   // gore array contains different forms of gore objects
    public GameObject[] gores;
    public int count;
    void Start()
    {
      // Generate a random number of gore objects, and instantiate them at random positions 
      // within a specific range, and add a random force to each gore object.
      // This is called when the enemy is destroyed.
        for (int i=0;i<count; i++)
        {
            GameObject gore = Instantiate(gores[Random.Range(0, gores.Length)], transform);
            float scale = Random.Range(1f, 3f);
            gore.transform.localScale = Vector3.one * scale;
            gore.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-140, 140), Random.Range(-140, 140), Random.Range(-140, 140)));
        }
        
    }
}
