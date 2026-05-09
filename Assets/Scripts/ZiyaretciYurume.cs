using UnityEngine;

public class ZiyaretciYurume : MonoBehaviour
{
    public float hiz = 2f;
    private Vector3 hedef;
    private bool hareketEt = false;

    void Update()
    {
        if (hareketEt)
        {
            transform.position = Vector3.MoveTowards(transform.position, hedef, hiz * Time.deltaTime);
            if (Vector3.Distance(transform.position, hedef) < 0.1f) hareketEt = false;
        }
    }

    public void HedefeGit(Vector3 yeniHedef)
    {
        hedef = yeniHedef;
        hareketEt = true;
    }
}