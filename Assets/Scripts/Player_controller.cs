using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class Player_controller : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float movementJump;
    private bool canJump;
    private int count;
    public GameObject winTextObject;
    public GameObject Ground;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        Ground = GameObject.FindGameObjectWithTag("GroundContact");
        rb = GetComponent<Rigidbody>();
        canJump = true;
        SetCountText();
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 movement = new Vector3(movementX, movementJump , movementY);

        rb.AddForce(movement * speed);
       movementJump = 0;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject == Ground)
        {
            canJump = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Ground)
        {
            canJump = false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movement = movementValue.Get<Vector2>();
        movementX = movement.x;
        movementY = movement.y;
    }
    void OnJump(InputValue jumpValue)
    {
        if (canJump)
        {

        movementJump = 30;
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 4)
        {
            winTextObject.SetActive(true);
            // Llamamos a la función CambiarEscena con un retraso de 2 segundos (puedes ajustar el tiempo según tus necesidades)
            Invoke("CambiarEscena", 2.0f);
            
        }
    }
        void CambiarEscena()
    {
        SceneManager.LoadScene("Menu");
    }
    
    
}
