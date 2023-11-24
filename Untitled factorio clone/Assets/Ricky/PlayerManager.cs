using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 0.2f;
    [SerializeField]
    private float sprintSpeed;
    [SerializeField]
    private int health = 100;
    [SerializeField]    
    private CharacterController character;
    private bool sprinting;
    // Start is called before the first frame update
    void Start()
    {
      character =  this.gameObject.GetComponent<CharacterController>();
        sprintSpeed = movementSpeed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Sprint") > 0.5f)
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

    public void GetRotation(Vector3 position)
    {

        Vector3 pos = new Vector3(position.x, transform.position.y, position.z);
        this.transform.LookAt(pos);
    }
}
