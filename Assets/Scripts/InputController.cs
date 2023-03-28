using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController: MonoBehaviour
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private CombatSystem combatSystem;
    private Vector3 direction;
    private void Update()
    {
        var xInput = Input.GetAxis("Horizontal");
        var zInput = Input.GetAxis("Vertical");
        var inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("StartMove");
            movementController.Move(Vector3.zero);
        }
        if (inputDirection != Vector3.zero)
        {
            movementController.Move(new Vector3(transform.position.x + xInput, 0, transform.position.z + zInput));
            Debug.Log(inputDirection);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            combatSystem.Attack();
        }
    }
    public void Init(GameObject _character)
    {
        movementController = _character.GetComponent<MovementController>();
        combatSystem = _character.GetComponent<CombatSystem>();
    }
}
