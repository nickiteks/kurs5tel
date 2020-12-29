using System;
using System.Collections;
using System.Collections.Generic;
using Database;
using UnityEngine;
using System.Linq;

public class LoadSaveInventory : MonoBehaviour
{
    public Inventory InventoryUser;
    public Inventory InventoryWarrior;
    public Inventory InventoryMage;
    public Inventory InventoryRogue;
    public BaseStatsScript StatsWarrior;
    public BaseStatsScript StatsMage;
    public BaseStatsScript StatsRogue;

    private void Awake()
    {
        //if(Client.Instance.isNewGame)
        //{
        //    CreateNewInventory();
        //} else
        //{
        //    LoadFromDatabaseInventory();
        //}
    }

    private void LoadFromDatabaseInventory()
    {
        throw new NotImplementedException();
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
        model.InventoryPersons = SaveInventoryPersons();
        model.InventoryUsers = SaveInventoryUser();
        SentSave(model);
    }

    private void SentSave(ModelSave model)
    {
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
        items.Add(ConvertInventoryPersons(InventoryWarrior));
        items.Add(ConvertInventoryPersons(InventoryMage));
        items.Add(ConvertInventoryPersons(InventoryRogue));
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
