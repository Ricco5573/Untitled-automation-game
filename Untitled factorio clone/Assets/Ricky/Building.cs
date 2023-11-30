using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Building : GridObject
{

    protected bool placing, canPlace;
    private PlayerCam pCam;
    [SerializeField]    
    protected GameObject indicator;
    [SerializeField]
    protected Material green, red;
    // Update is called once per frame
    //
    //
    //
 protected virtual void Update()
    {

        if (placing)
        {
           Vector3 position =  pCam.GetMousePos();
            this.gameObject.transform.position = new Vector3(position.x, this.transform.position.y, position.z);
        }

    }

    public bool SetPlacing(bool set)
    {
        if (set)
        {
            placing = set;
            return true;
        }
        else if(!set && canPlace)
        {
            placing = set;
            return true; 
        }

            return false;
    }

        public void SetpCam(PlayerCam cam)
    {
        pCam = cam;
    }
}
