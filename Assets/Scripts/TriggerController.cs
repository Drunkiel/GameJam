using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public bool isTriggered;
    public bool reverseReturn;
    public string[] objectsTag;
    public HashSet<string> objectsTagsSet;

    void Awake()
    {
        objectsTagsSet = new HashSet<string>(objectsTag);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CheckCollision(collider);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        CheckCollision(collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        CheckCollision(collider, false);
    }

    void CheckCollision(Collider2D collider, bool enter = true)
    {
        if (objectsTagsSet == null)
            return;

        if (objectsTagsSet.Contains(collider.tag))
        {
            isTriggered = reverseReturn ? !enter : enter;
            return;
        }
    }
}