using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArena : MonoBehaviour
{
    [SerializeField] private BattleArenaData battleArenaData;
    [SerializeField] private int countEnemy;
    [SerializeField] private List<Vector3> spawnPoints;
    [SerializeField] private List<Vector3> freeSpawnPoints;
    [SerializeField] private InputController inputController;
    [SerializeField] private List<CharacterBehaviour> enemyBehaviours;
    [SerializeField] private Health playerCharacter;
    [SerializeField] private List<Health> enemyes;

    private void Awake()
    {
        countEnemy = 3;
        freeSpawnPoints = spawnPoints;
        battleArenaData = BattleArenaMenu.SelectedBattleArenaData;

        var playerGameobject = SpawnCharacter(CharactersManager.Instance.SelectedCharacter);
        playerGameobject.GetComponent<MovementController>().InitializeStat(CharactersManager.Instance.SelectedCharacter);
        playerGameobject.GetComponent<CombatSystem>().InitializeStat(CharactersManager.Instance.SelectedCharacter);
        playerGameobject.GetComponent<Health>().InitializeStat(CharactersManager.Instance.SelectedCharacter);
        inputController.Init(playerGameobject);

        Debug.Log("InstancePlayer");
        for (int i = 0; i < countEnemy; i++)
        {
            var enemy = new Character(battleArenaData.PossibleEnemys[Random.Range(0, battleArenaData.PossibleEnemys.Length)],
            Random.Range(battleArenaData.MinRank, battleArenaData.MaxRank));
            enemy.AbilityTree.RandomImprove();

            var enemyGameobject = SpawnCharacter(enemy);
            enemyBehaviours.Add(enemyGameobject.GetComponent<CharacterBehaviour>());
            enemyGameobject.GetComponent<MovementController>().InitializeStat(enemy);
            enemyGameobject.GetComponent<CombatSystem>().InitializeStat(enemy);
            //enemyGameobject.GetComponent<EffectManager>().InitializeStat(enemy);
            var health = enemyGameobject.GetComponent<Health>();
            health.InitializeStat(enemy);
            enemyes.Add(health);
            Debug.Log("InstanceEnemy: "+ i);
        }
        StartBattle();
    }
    private void Start()
    {
        
    }
    private void StartBattle()
    {
        Debug.Log("Turning on Behaviours");
        foreach (var item in enemyBehaviours)
        {
            item.enabled = true;
        }
        inputController.enabled = true;
    }
    private void win()
    {

    }
    private void Loose()
    {

    }
    private GameObject SpawnCharacter(Character character)
    {
        var randomPoint = freeSpawnPoints[Random.Range(0, freeSpawnPoints.Count)];
        freeSpawnPoints.Remove(randomPoint);
        var characterGameobject = (Instantiate(character.CharacterData.PrefabCharacter));
        characterGameobject.transform.position = randomPoint;
        return characterGameobject;
    }
}
