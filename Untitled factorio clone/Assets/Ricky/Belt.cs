using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : Building
{
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        
        if(collision.gameObject.tag == "Item")
        {
            Debug.Log("adding force");
            collision.gameObject.GetComponent<Rigidbody>().velocity = (transform.right * 2);
        }
    }
}
