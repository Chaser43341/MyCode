using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public float camSpeed = 5;
    public ForceManager forceManager;
    // Update is called once per frame
    void Update()
    {
        float hor=
        Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(hor, ver, 0)* camSpeed*Time.deltaTime);
        forceManager.SetSpawnPoint(transform.position);
    }
}
