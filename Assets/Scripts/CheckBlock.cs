using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBlock : MonoBehaviour
{
    private bool _onBackground = false;
    private bool otherBlock = false;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
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
