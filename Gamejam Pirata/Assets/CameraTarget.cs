using UnityEngine;

public class CameraTarget : MonoBehaviour
{


    [SerializeField]private Camera cam;
    [SerializeField]private Transform player;
    [SerializeField]private float threshold;
    
    void Update()
    {
        
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + mousePos);

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = targetPos;
    }
}
