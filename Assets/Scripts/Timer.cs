using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
      //  DataCollection.ME.
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        // Does the ray intersect any objects
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, 100f))
        {
            if (DataCollection.ME.timeSpentPerObject.ContainsKey(hit.collider.gameObject.name))
            {
                DataCollection.ME.timeSpentPerObject[hit.collider.gameObject.name] += Time.deltaTime;
            }
            else
            {
                DataCollection.ME.timeSpentPerObject.Add(hit.collider.gameObject.name, Time.deltaTime);
            }
            Debug.Log(hit.collider.gameObject.name + " " + DataCollection.ME.timeSpentPerObject[hit.collider.gameObject.name].ToString());
        }
    }

    
}
