using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCam : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField]
    private PlayerManager player;
    private Transform playerPos;
    private Vector3 camOffset;
    private int minZoom = 3;
    private int maxZoom = 8;
    private bool zoom;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GetComponent<Camera>();
        camOffset = new Vector3(player.gameObject.transform.position.x - this.transform.position.x, 0, player.gameObject.transform.position.z - this.transform.position.z);

        playerPos = player.gameObject.transform;
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
    private void LateUpdate()
    {
        this.gameObject.transform.position = new Vector3(playerPos.position.x - camOffset.x, this.gameObject.transform.position.y, playerPos.position.z - camOffset.z);
    }
}
