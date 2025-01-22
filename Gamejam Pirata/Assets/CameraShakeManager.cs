using UnityEngine;
using Unity.Cinemachine;

public class CameraShakeManager : MonoBehaviour
{

    public static CameraShakeManager instance;

    [SerializeField]private float globalShakeForce = 1f;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }
}
