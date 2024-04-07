using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCameras : MonoBehaviour {

    public static ArenaCameras instance;

    private void Awake()
    {
        instance = this;
    }

    public List<GameObject> cameras;
    private bool isActive = false;
    private int indexOfActive = 0;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cameras[indexOfActive].SetActive(false);
                indexOfActive = (indexOfActive + 1) % cameras.Count;
                cameras[indexOfActive].SetActive(true);
            }
        }
    }

    public void ActivateCameras()
    {
        isActive = true;
        cameras[indexOfActive].SetActive(true);
    }

}
