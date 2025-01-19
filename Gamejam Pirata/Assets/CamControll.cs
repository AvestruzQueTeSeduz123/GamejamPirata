using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CamControll : MonoBehaviour
{
    private Transform centerCam;
    private CinemachineCamera cinemachineCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject machineCamera = GameObject.FindWithTag("MachineCamera");
        cinemachineCamera = machineCamera.GetComponent<CinemachineCamera>();

        GameObject centerCamObject = GameObject.FindWithTag("CenterCam");
        centerCam = centerCamObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BackCamera()
    {
        yield return new WaitForSeconds(2f);
        cinemachineCamera.Follow = centerCam;
    }
}
