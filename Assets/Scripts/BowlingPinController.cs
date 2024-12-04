using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinController : MonoBehaviour
{
    public float lifespan;
    public bool haveFallen = false;
    public Rigidbody rb;
    private PinsSpawnerController pinsSpawnerController;

    // Start is called before the first frame update
    void Start()
    {
        pinsSpawnerController = transform.parent.GetComponent<PinsSpawnerController>();
    }

    void OnEnable()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        lifespan = 5f;
        haveFallen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (haveFallen)
        {
            lifespan -= Time.deltaTime;
            if (lifespan < 0)
            {
                pinsSpawnerController.ReportPinStatus();
                gameObject.SetActive(false);
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
