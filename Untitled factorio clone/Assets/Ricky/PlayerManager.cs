using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 0.2f;
    [SerializeField]
    private int health = 100;
    [SerializeField]    
    private CharacterController character;
    // Start is called before the first frame update
    void Start()
    {
      character =  this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3 (horizontal * movementSpeed, 0, vertical * movementSpeed);
        character.Move(movement);
        Debug.Log("Horizontal: " + horizontal + " Vertical: " + vertical);

        
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
