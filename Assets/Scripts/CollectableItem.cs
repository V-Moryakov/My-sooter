using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public int item = 1;

    private void OnTriggerEnter(Collider other)
    {
        var spereItem = other.gameObject.GetComponent<PlayerProgress>();

        spereItem.AddCount(item);
        Destroy(gameObject);
    }

}

