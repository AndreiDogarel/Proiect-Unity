using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBorderScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Player has hit the border");
            collision.gameObject.GetComponent<PlayerStats>().OutOfBorder();
        }
    }
}
