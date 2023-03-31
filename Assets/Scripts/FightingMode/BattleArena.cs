using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleArena : MonoBehaviour
{
    [SerializeField] private BattleArenaData battleArenaData;
    [SerializeField] private InputController inputController;
    [SerializeField] private List<CharacterBehaviour> enemyBehaviours;
    [SerializeField] private Health playerCharacter;
    [SerializeField] private List<Health> enemyes;

    [Header("SpawnCharactersSettings")]
    [SerializeField] private int countEnemy;
    [SerializeField] private List<Vector3> spawnPoints;
    private List<Vector3> freeSpawnPoints;

    [Header("CoutDownPanelSettings")]
    [SerializeField] private float timeToStart = 6;
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private Text countdownText;
    [SerializeField] private Image clock;
    [SerializeField] private Coroutine clockAnimationCororutine;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private List<ResourceRewardCell> rewardCells = new();
    [SerializeField] private ResourceRewardCell equipmentRewardPrefab;


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
        StartCoroutine(CountdownToStart());
    }
    private IEnumerator CountdownToStart()
    {
        countdownPanel.SetActive(true);
        var remainingTime = timeToStart;
        while (true)
        {
            remainingTime--;
            if (clockAnimationCororutine != null)
            {
                StopCoroutine(clockAnimationCororutine);
            }
            clockAnimationCororutine = StartCoroutine(ClockAnimation());
            countdownText.text = remainingTime.ToString();
            if (remainingTime == 0)
            {
                countdownPanel.SetActive(false);
                ActivateCharacters();
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }
    private IEnumerator ClockAnimation()
    {
        if (clock.fillClockwise == true)
            clock.fillAmount = 1;
        else
            clock.fillAmount = 0;

        clock.fillClockwise = !clock.fillClockwise;

        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (clock.fillClockwise == true)
                clock.fillAmount += Time.deltaTime;
            else
                clock.fillAmount -= Time.deltaTime;
        }
    }
    private void ActivateCharacters()
    {
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
