using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterClass
{
    private Dictionary<string, object> CharacterInfo;
    public CharacterClass(string Name)
    {
        DataTable Character;
        SqliteDatabase sqlDB = new SqliteDatabase("GameDB.db");
        string query = "SELECT * from Character Where Name = '" + Name + "'";
        Character = sqlDB.ExecuteQuery(query);
        if (Character.Rows.Count>0) {
            CharacterInfo = Character.Rows[0];
            Debug.Log("Successfully load character " + Name);
        } else
        {
            CharacterInfo = null;
            Debug.Log("Character not found in database!");
        }
    }

    public object getInfo(string name)
    {
        if (CharacterInfo != null && CharacterInfo.ContainsKey(name))
        {
            return CharacterInfo[name];
        }
        else
        {
            return null;
        }
    }

    public void changeValue(string name, int value)
    {
        if (CharacterInfo != null && CharacterInfo.ContainsKey(name))
        {
            int val = (int)CharacterInfo[name];
            val += value;
            CharacterInfo[name] = val;
        }
    }

    public void printValue(Text t)
    {
        t.text = "";
        foreach(var item in CharacterInfo)
        {
            t.text += item.Key;
            t.text += ": ";
            t.text += item.Value.ToString();
            t.text += "\n";
        }
    }
}
