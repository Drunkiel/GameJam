using UnityEngine;
using UnityEngine.Events;

public class TriggerAction : MonoBehaviour
{
    public string tagName;
    public UnityEvent enterAction;
    public UnityEvent exitAction;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (tagName.Contains(collider.tag))
            enterAction.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (tagName.Contains(collider.tag))
            enterAction.Invoke();
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (tagName.Contains(collider.tag))
            exitAction.Invoke();
    }
}