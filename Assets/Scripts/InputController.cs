using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController: MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private MovementController movementController;
    [SerializeField] private CombatSystem combatSystem;
    [SerializeField] private FixedJoystick fixedJoystick;
    [SerializeField] private GameObject controllPanel;
    private void Update()
    {
        var direction = fixedJoystick.Direction;

        movementController.Move(new Vector3(character.transform.position.x + direction.x * 3, character.transform.position.y, character.transform.position.z + direction.y * 3));
    }
    public void Init(GameObject _character)
    {
        character = _character;
        movementController = _character.GetComponent<MovementController>();
        combatSystem = _character.GetComponent<CombatSystem>();
    }
    public void TryAttack()
    {
        if (combatSystem != null)
        {
            combatSystem.Attack();
        }
    }
    public void OnEnable()
    {
        controllPanel.SetActive(true);
    }
    public void OnDisable()
    {
        if (controllPanel != null)
        {
            controllPanel.SetActive(false);
        }
    }
}
