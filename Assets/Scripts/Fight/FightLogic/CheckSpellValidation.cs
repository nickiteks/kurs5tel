using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CheckSpellValidation 
{
    // Start is called before the first frame update
    public static bool CheckSpellExists(string spell, Character character)
    {
        if (character.spellBook.Select(x => x.name == spell) != null)        
            return true;
        else
            return false;
    }
}
