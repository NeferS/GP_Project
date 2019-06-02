using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This simple class shows Text component in a Canvas for a given delta time.*/
[RequireComponent(typeof(Collider))]
public class DeltaTimeText : MonoBehaviour
{
    public string text;
    public GameObject target;
    public float deltaTime;
    public bool isOneShot = false;
    bool triggered = false;

    void Start()
    {
        target.SetActive(false);
    }

    void OnTriggerEnter()
    {
        if (isOneShot && triggered) return;
        target.SetActive(true);
        target.GetComponent<Text>().text = text;
        StartCoroutine(Disable());
        triggered = true;
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(deltaTime);
        target.GetComponent<Text>().text = "";
        target.SetActive(false);
    }
}
