using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 0.2f;
    [SerializeField]
    private float sprintSpeed;
    [SerializeField]
    private int health = 100, fireAmount = 900;
    [SerializeField]    
    private CharacterController character;
    [SerializeField]
    private PlayerCam cam;
    private bool sprinting, firing;
    [SerializeField]
    private GameObject miner, pump, furnace, generator, refinery, belt, pipe, sprinkler, place, fireCol;
    private bool placing = false;
    // Start is called before the first frame update
    void Start()
    {
      character =  this.gameObject.GetComponent<CharacterController>();
        sprintSpeed = movementSpeed * 2;
    }

    // Update is called once per frame
    void Update()
    {

        if(placing && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (place.GetComponent<Building>().SetPlacing(false))
            {
                placing = false;
            }
        }
        if(!placing && Input.GetKeyDown(KeyCode.Mouse0))
        {
            fireCol.SetActive(true);
            firing = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) || fireAmount <= 0)
        {
            fireCol.SetActive(false);
            firing = false;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1) && placing) {

            placing = false;
            Destroy(place.gameObject);
        }
        if(placing && Input.GetKeyDown(KeyCode.R))
        {
            place.gameObject.transform.Rotate(place.transform.rotation.x, place.transform.rotation.y + 90, place.transform.rotation.z);

        }
        if (firing)
        {
            fireAmount -= 5;
        }
        else
        {
            fireAmount++;
        }

        if (!placing)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                place = Instantiate(miner, new Vector3(0, 0.5f, 0), Quaternion.identity);
                placing = true;
                Building build = place.GetComponent<Building>();
                build.SetpCam(cam);
                build.SetPlacing(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                place = Instantiate(pump, new Vector3(0, 0.5f, 0), Quaternion.identity);
                placing = true;
                Building build = place.GetComponent<Building>();
                build.SetpCam(cam);
                build.SetPlacing(true);
            }

        }

            if (Input.GetAxis("Sprint") > 0.5f)
        {
            sprinting = true;
            Debug.Log("Sprinting");
        }
        else
        {
            sprinting= false;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3 (sprinting ? horizontal * sprintSpeed : horizontal * movementSpeed , 0, sprinting ? vertical * sprintSpeed : vertical * movementSpeed);
        character.Move(movement);
        Vector3 position = cam.GetMousePos();
        Vector3 pos = new Vector3(position.x, transform.position.y, position.z);
        this.transform.LookAt(pos);
        //Debug.Log("Horizontal: " + horizontal + " Vertical: " + vertical);


    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            //death
        }
    }

}
