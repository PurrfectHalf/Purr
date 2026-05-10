
using UnityEngine;

public class pipeMiddleScript : MonoBehaviour
{
    public LogicScript logic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bir þey tetikleyiciye girdi!");

        if (collision.gameObject.layer == 3)
        {
            Debug.Log("Kuþ katmaný doðru, skor artýyor!");
            logic.addScore(1);
        }

        else
        {
            Debug.Log("Tetiklenen objenin katmaný: " + collision.gameObject.layer);
        }
    }
}

