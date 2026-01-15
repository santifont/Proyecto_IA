using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Chomp : MonoBehaviour
{
    private Vector2 myMove;
    private float characterSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * myMove *  characterSpeed * Time.deltaTime);
    }

    public void MovementCallback(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
        }

        if (ctx.performed)
        {
            myMove = ctx.ReadValue<Vector2>();
            Debug.Log(myMove);
        }

        if (ctx.canceled)
        {
            myMove = ctx.ReadValue<Vector2>();
            Debug.Log(myMove);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            Debug.Log("BALL");
            Destroy(other);
        }

        if (other.CompareTag("cherry"))
        {
            Debug.Log("You touched a CHERRY");
        }
    }
}
