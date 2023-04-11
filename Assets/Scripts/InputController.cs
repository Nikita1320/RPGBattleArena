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
    [SerializeField] private AttackButton attackButton;
    [SerializeField] private AbilityButton[] abilityButton;
    [SerializeField] private AbilityManager abilityManager;
    [SerializeField] private CharacterFollowerCamera followerCamera;
    private void Start()
    {
        abilityButton[0].Button.onClick.AddListener(() => TryUseAbility(0));
        abilityButton[1].Button.onClick.AddListener(() => TryUseAbility(1));
        abilityButton[2].Button.onClick.AddListener(() => TryUseAbility(2));
    }
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
        abilityManager = _character.GetComponent<AbilityManager>();

        for (int i = 0; i < abilityButton.Length; i++)
        {
            abilityButton[i].Init(abilityManager.ActiveAbilities[i]);
        }
        followerCamera.Init(character);
    }
    public void TryAttack()
    {
        if (combatSystem != null)
        {
            if (combatSystem.Attack())
            {
                attackButton.StartCoolDown(combatSystem.AttackSpeed.GetCurveValue());
            }
        }
    }
    public void TryUseAbility(int index)
    {
        abilityManager.UseAbility(index);
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
