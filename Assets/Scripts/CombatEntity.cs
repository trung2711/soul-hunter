using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatEntity
{
    private Dictionary<string, object> EntityInfo = new Dictionary<string, object>()
    {
        {"Name", null},
        {"Str", null},
        {"Agi", null},
        {"Int", null},
        {"MHealth", null},
        {"CHealth", null},
        {"ATK", null},
        {"DEF", null},
        {"Crit", null},
        {"Accuracy", null},
        {"Element", null},
        {"Element2", null},
        {"Soul", null},
        {"UltBar", null},
        {"Weapon", null},
        {"DamageOverTime", null},
        {"TurnBurnt", null},
        {"IsStunned", null},
        {"Class", null},
        {"IPP", null},
        {"HIPP", null}
    };

    public CombatEntity(CharacterClass c)
    {
        Dictionary<string, object> copy = new Dictionary<string, object>(EntityInfo);
        foreach (var item in copy.Keys) {
            EntityInfo[item] = c.getInfo(item);
        }
        EntityInfo["CHealth"] = EntityInfo["MHealth"];
        copy = null;
    }

    public CombatEntity(Enemy e)
    {
        Dictionary<string, object> copy = new Dictionary<string, object>(EntityInfo);
        foreach (var item in copy.Keys)
        {
            EntityInfo[item] = e.getInfo(item);
        }
        EntityInfo["CHealth"] = EntityInfo["MHealth"];
        copy = null;
    }

    public object getInfo(string name)
    {
        if (EntityInfo != null && EntityInfo.ContainsKey(name))
        {
            return EntityInfo[name];
        }
        else
        {
            return null;
        }
    }

    public void changeValue(string name, int value)
    {
        if (EntityInfo != null && EntityInfo.ContainsKey(name))
        {
            if(EntityInfo[name] == null){
                EntityInfo[name] = 0;
            }
            int val = (int)EntityInfo[name];
            val += value;
            EntityInfo[name] = val;
        }
    }

    public void setValue(string name, int value)
    {
        if (EntityInfo != null && EntityInfo.ContainsKey(name))
        {
            EntityInfo[name] = value;
        }
    }

    public void printValue(Text t)
    {
        t.text = "";
        foreach (var item in EntityInfo)
        {
            t.text += item.Key;
            t.text += ": ";
            if (item.Value != null)
            {
                t.text += item.Value.ToString();
            }
            else
            {
                t.text += "null";
            }
            t.text += "\n";
        }
    }
}
