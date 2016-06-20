using UnityEngine;
using System.Collections;

public class LifeCounter : MonoBehaviour
{
    public int Life = 100;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        var damageInfo = coll.gameObject.GetComponent<DamageInfo>();
        if (damageInfo != null)
        {
            Life -= damageInfo.DamageHitPoints;
            if (Life <= 0)
                Destroy(gameObject);
        }
    }
}
