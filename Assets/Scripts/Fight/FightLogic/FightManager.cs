using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : Singleton<FightManager>
{
    [SerializeField]
    private GameObject[] monstersPrefabs;
    [SerializeField]
    private GameObject[] monstersGameObjects;

    private bool IsStepFriendly { get; set; }

    private FightController CurrentController;
    [SerializeField]
    private FightController playerController;
    [SerializeField]
    private FightController enemyController;

    [Tooltip("индекс персонажа текущей команды")]
    private int indexCurrentCharacter = 0;

    [SerializeField]
    private LoadSaveInventory loadSave;

    private void Awake()
    {
        IsStepFriendly = true;

        // загрузка игрока
        Character warrior = new Character() { IsEnemy = true };
        Character mage = new Character() { IsEnemy = true };
        Character rogue = new Character() { IsEnemy = true };

        loadSave.StatsWarrior = warrior.BaseStatsScript;
        loadSave.StatsMage = mage.BaseStatsScript;
        loadSave.StatsRogue = rogue.BaseStatsScript;

        playerController.characters = new List<Character>() { warrior, mage, rogue };

        // загрузка монстров
        int randomMonsterIndex = Random.Range(0, monstersPrefabs.Length);
        GameObject monsterPrefab = monstersPrefabs[randomMonsterIndex];

        foreach (GameObject monster in monstersGameObjects)
        {
            monster.GetComponent<SpriteRenderer>().sprite = monsterPrefab.GetComponent<SpriteRenderer>().sprite;

            BaseStatsScript baseStatsScriptMonster = monster.GetComponent<BaseStatsScript>();
            baseStatsScriptMonster = monsterPrefab.GetComponent<BaseStatsScript>();

            enemyController.characters.Add(new Character() { IsEnemy = true, BaseStatsScript = baseStatsScriptMonster });
        }

        // запуск битвы
        CurrentController = playerController;
        CurrentController.StartStep(CurrentController.characters[indexCurrentCharacter]);
    }

    public void ControlEndStep()
    {
        ControlSwitchTurn();
        StartCoroutine(StartNewStep());
    }

    private void CreateScene()
    {
        //TODO:дописать
    }
    
    private void CloseScene()
    {
        //TODO:дописать
    }
    private void ControlSwitchTurn()
    {
        if (indexCurrentCharacter >= CurrentController.characters.Count)
        {
            IsStepFriendly = !IsStepFriendly;
            if (IsStepFriendly)
            {
                CurrentController = playerController;
                UIFightManager.Instance.OpenActionPanel();
            }
            else
            {
                CurrentController = enemyController;
                UIFightManager.Instance.CloseActionPanel();
            }

            indexCurrentCharacter = 0;
        }
        else
        {
            indexCurrentCharacter++;
        }
    }

    private IEnumerator StartNewStep()
    {
        yield return new WaitForSeconds(1);

        CurrentController.StartStep(CurrentController.characters[indexCurrentCharacter]);
    }
}
