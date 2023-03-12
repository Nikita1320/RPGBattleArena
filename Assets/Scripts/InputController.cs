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
        direction = new Vector3(Input.GetAxis("Horizontal") + movementController.transform.position.x, 0, Input.GetAxis("Vertical") + movementController.transform.position.z);
        movementController.Move(direction);
        Debug.Log(direction);
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
