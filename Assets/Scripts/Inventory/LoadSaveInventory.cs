using System;
using System.Collections;
using System.Collections.Generic;
using Database;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LoadSaveInventory : MonoBehaviour
{
    public DatabaseInventory data;
    public Inventory InventoryUser;
    public Inventory InventoryWarrior;
    public Inventory InventoryMage;
    public Inventory InventoryRogue;
    public BaseStatsScript StatsWarrior;
    public BaseStatsScript StatsMage;
    public BaseStatsScript StatsRogue;

    private void Start()
    {
        if(Client.Instance.isNewGame)
        {
            CreateNewInventory();
        } else
        {
            LoadFromDatabaseInventory();
        }
    }

    public void LoadFromDatabaseInventory()
    {
        ModelSave model = APIClient.GetRequest<ModelSave>("/api/Storage/Load?userId="+Client.Instance.user.Id);
        ConvertModelSave(model);
    }

    private void ConvertModelSave(ModelSave model)
    {
        LoadItemsPersons(model);
        LoadItemsToInventor(model.InventoryUsers, InventoryUser);
        LoadPersons(model);
    }

    private void LoadPersons(ModelSave model)
    {
        LoadStats(model.Persons[0], StatsWarrior);
        LoadStats(model.Persons[1], StatsMage);
        LoadStats(model.Persons[2], StatsRogue);
    }

    private void LoadStats(Person person, BaseStatsScript stats)
    {
        stats.Armor = person.Armor;
        stats.Damage = person.Damage;
        stats.id = person.Id.Value;
        stats.ManaMax = person.ManaMax;
        stats.Mana = person.Mana;
        stats.MaxHelth = person.Health;
        stats.Health = person.Health;
    }

    private void LoadItemsToInventor(List<ItemUser> items, Inventory inventory)
    {
        //inventory.items.Clear();
        int i = 0;
        foreach (var item in items)
        {
            inventory.AddItem(i, data.items[item.ItemId], item.ItemCount);
            i++;
        }
        inventory.UpdateInventiory();
    }



    private void LoadItemsPersons(ModelSave model)
    {
        if (InventoryWarrior) LoadItemsToInventor(model.InventoryPersons[0], InventoryWarrior);
        if (InventoryMage) LoadItemsToInventor(model.InventoryPersons[1], InventoryMage);
        if (InventoryRogue) LoadItemsToInventor(model.InventoryPersons[2], InventoryRogue);
    }

    private void LoadItemsToInventor(List<ItemPerson> items, Inventory inventory)
    {
        //inventory.items.Clear();
        int i = 0;
        foreach (var item in items)
        {
            inventory.AddItem(i, data.items[item.ItemId], item.ItemCount);
            i++;
        }
        inventory.UpdateInventiory();
    }

    private void CreateNewInventory()
    {
        return;
    }

    public void SaveInventory()
    {
        ModelSave model = new ModelSave();
        model.User = Client.Instance.user;
        model.Persons = SavePersons();

        var lists = SaveInventoryPersons();
        if (lists.Count != 0) model.InventoryPersons = SaveInventoryPersons();

        model.InventoryUsers = SaveInventoryUser();
        SentSave(model);
    }

    private void SentSave(ModelSave model)
    {
        APIClient.PostRequest("api/Storage/Save", model);
    }

    private List<ItemUser> SaveInventoryUser()
    {
        return InventoryUser.items.Select(x => new ItemUser
        {
            ItemCount = x.count,
            ItemId = x.id,
            UserId = Client.Instance.user.Id
        }).ToList();
    }

    private List<List<ItemPerson>> SaveInventoryPersons()
    {
        List<List<ItemPerson>> items = new List<List<ItemPerson>>();
        if (InventoryWarrior) items.Add(ConvertInventoryPersons(InventoryWarrior));
        if (InventoryMage) items.Add(ConvertInventoryPersons(InventoryMage));
        if (InventoryRogue) items.Add(ConvertInventoryPersons(InventoryRogue));
        return items;
    }

    private List<ItemPerson> ConvertInventoryPersons(Inventory inventory)
    {
        return inventory.items.Select(x => new ItemPerson() {
            ItemCount = x.count,
            ItemId = x.id,
        }).ToList();
    }

    private List<Person> SavePersons()
    {
        List<Person> persons = new List<Person>();
        persons.Add(ConvertStatsToPerson(StatsWarrior,"Warrior"));
        persons.Add(ConvertStatsToPerson(StatsMage, "Mage"));
        persons.Add(ConvertStatsToPerson(StatsRogue, "Rogue"));
        return persons;
    }

    private Person ConvertStatsToPerson(BaseStatsScript stats, string ClassName)
    {
        return new Person()
        {
            Armor = stats.Armor,
            ClassName = ClassName,
            Damage = stats.Damage,
            Health = stats.Health,
            Mana = stats.Mana,
            ManaMax = stats.ManaMax,
            UserId = Client.Instance.user.Id,        
            Id = stats.id
        };
    }
}
