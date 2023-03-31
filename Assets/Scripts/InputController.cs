using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController: MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private MovementController movementController;
    [SerializeField] private CombatSystem combatSystem;
    private void Update()
    {
        var xInput = Input.GetAxis("Horizontal");
        var zInput = Input.GetAxis("Vertical");
        var inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (inputDirection != Vector3.zero && movementController != null)
        {
            movementController.Move(new Vector3(character.transform.position.x + xInput * 3, character.transform.position.y, character.transform.position.z + zInput * 3));
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && combatSystem != null)
        {
            combatSystem.Attack();
        }
    }
    public void Init(GameObject _character)
    {
        character = _character;
        movementController = _character.GetComponent<MovementController>();
        combatSystem = _character.GetComponent<CombatSystem>();
    }
}
