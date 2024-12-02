using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinController : MonoBehaviour
{
    public float lifespan = 10f;
    public bool haveFallen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (haveFallen)
        {
            lifespan -= Time.deltaTime;
            if (lifespan < 0)
            {
                Destroy(gameObject);
            }
        } else
        {
            if (Mathf.Abs(transform.rotation.eulerAngles.x) > 89 || Mathf.Abs(transform.rotation.eulerAngles.z) > 89)
            {
                haveFallen = true;
            }
        }
    }
}
