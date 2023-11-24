using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCam : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField]
    private PlayerManager player;

    private Vector3 camOffset;
    private int minZoom = 3;
    private int maxZoom = 20;
    private bool zoom;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GetComponent<Camera>();
        camOffset = new Vector3(player.gameObject.transform.position.x - this.transform.position.x, 0, player.gameObject.transform.position.z - this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if ((-scroll * 2) + mainCam.orthographicSize <= maxZoom && (-scroll * 2) + mainCam.orthographicSize >= minZoom)
        {
            mainCam.orthographicSize += -scroll * 2;
        }
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            player.GetRotation(hit.point);
        }

        float differenceX = player.gameObject.transform.position.x - this.transform.position.x;
        float differenceZ = player.gameObject.transform.position.z - this.transform.position.z;
        if (differenceX != 0 )
        {

            if (differenceX > 0)
            {
                this.transform.position = new Vector3(this.transform.position.x - differenceX, this.transform.position.y, this.transform.position.z);
            }
            else 
            {
                this.transform.position = new Vector3(this.transform.position.x + differenceX, this.transform.position.y, this.transform.position.z);

            }
        }
        if (differenceZ != 0)
        {
            if (differenceZ > 0)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - differenceZ);

            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + differenceZ);

            }

        }

    }

        public bool IsPosVisible(Vector3 position) {

        Vector3 point = mainCam.WorldToScreenPoint(position);

        if(point.z < 0)
        {
            return false;
        }

        if((point.x < 0 ) || (point.x > Screen.width) || (point.y < 0) || (point.y > Screen.height)){
            return false;

        }
        else
        {
            return true;
        }
    }
}
