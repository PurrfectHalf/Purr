using System.Linq.Expressions;
using System.Xml.Xsl;
using UnityEngine;

public class pipeSpawner : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOfsett = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }

        else
        {
            spawnPipe();
            timer = 0;
        }
           
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOfsett;
        float highestPoint = transform.position.y + heightOfsett;

        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
