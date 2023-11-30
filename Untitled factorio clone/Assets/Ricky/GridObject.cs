using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField]
    protected GridManager gridMan;
    // Start is called before the first frame upd


    protected void Start()
    {
        gridMan = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();

    }


    private void LateUpdate()
    {
        Vector3 newPos = gridMan.GetNearestTile(this.gameObject.transform);
        this.transform.position = new Vector3(newPos.x, this.transform.position.y, newPos.z);

    }
}
