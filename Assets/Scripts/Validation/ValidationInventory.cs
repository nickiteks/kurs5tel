using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidationInventory : MonoBehaviour
{

    public bool CheckIdInventory(int idInventory,int idDatabaseInventory)
    {
        if(idDatabaseInventory != idInventory)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
