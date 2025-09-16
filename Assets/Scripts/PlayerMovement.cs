using JetBrains.Annotations;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveInput;
    private PlayerControl controls;
    private void Awake()
    {
        controls = new PlayerControl();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
 
    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos += moveInput * speed * Time.deltaTime;
        transform.position = pos;

    }
}
