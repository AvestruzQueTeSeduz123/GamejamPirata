using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    public bool hasHit;
    private Bow bow;
    private CinemachineCamera cinemachineCamera;
    private Transform centerCam;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject bowObject = GameObject.FindWithTag("Bow");
        bow = bowObject.GetComponent<Bow>();
        GameObject machineCamera = GameObject.FindWithTag("MachineCamera");
        cinemachineCamera = machineCamera.GetComponent<CinemachineCamera>();

        GameObject centerCamObject = GameObject.FindWithTag("CenterCam");
        centerCam = centerCamObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasHit == false)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            cinemachineCamera.Follow = this.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D  col)
    {
        hasHit = true;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        bow.isShotting = false;
        cinemachineCamera.Follow = centerCam;
    }
}
