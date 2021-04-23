using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
            Debug.Log("Molhadinho");
            ObjectPooler.Instance.SpawnFromPool("Splash", other.gameObject.transform.position, other.gameObject.transform.rotation);
        //}
    }
}
