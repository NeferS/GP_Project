using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This simple class shows Text component in a Canvas for a given delta time.*/
[RequireComponent(typeof(Collider))]
public class DeltaTimeText : MonoBehaviour
{
    public string text;
    public Text target;
    public float deltaTime;
    public bool isOneShot = false;
    bool triggered = false;

    void OnTriggerEnter()
    {
        if (isOneShot && triggered) return;
        target.enabled = true;
        target.text = text;
        StartCoroutine(Disable());
        triggered = true;
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(deltaTime);
        target.text = "";
        target.enabled = false;
    }
}
