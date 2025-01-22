using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeDialogue : MonoBehaviour
{
    public bool start = false;
    [SerializeField]private AnimationCurve curve;
    [SerializeField]private float duration = 1f;
    [SerializeField]private Transform textPos;
    // Start is called before the first frame update
    void Start()
    {
        // RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        // rectTransform.LeanMoveLocalY(shakeQuantitiy, 1).setEaseInOutQuart().setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = textPos.position;
        // if(start) {
        //     start = false;
        //     StartCoroutine(Shaking());
        // }
    }

    public IEnumerator Shaking()
    {
        start = true;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (start == true)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }
}
