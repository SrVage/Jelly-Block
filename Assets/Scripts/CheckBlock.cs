using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBlock : MonoBehaviour
{

    public bool _onBackground = false;
    public bool otherBlock = false;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StayBlock")) otherBlock = true;//transform.parent.gameObject.GetComponent<DestroyGO>().OnBlock(true);
        if (collision.gameObject.CompareTag("Background")) _onBackground = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StayBlock")) otherBlock = false; //transform.parent.gameObject.GetComponent<DestroyGO>().OnBlock(false);
        if (collision.gameObject.CompareTag("Background")) _onBackground = false;
    }
}
