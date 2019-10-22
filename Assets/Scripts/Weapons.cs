using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons
{
    private Dictionary<string, object> WeaponInfo;
    public Weapons(int id)
    {
        DataTable Weapon;
        SqliteDatabase sqlDB = new SqliteDatabase("GameDB.db");
        string query = "SELECT * from Weapons Where ID = '" + id + "'";
        Weapon = sqlDB.ExecuteQuery(query);
        if (Weapon.Rows.Count>0) {
            WeaponInfo = Weapon.Rows[0];
            Debug.Log("Successfully load Weapon " + id);
        } else
        {
            WeaponInfo = null;
            Debug.Log("Weapon not found in database!");
        }
    }

    public object getInfo(string name)
    {
        if (WeaponInfo != null && WeaponInfo.ContainsKey(name))
        {
            return WeaponInfo[name];
        }
        else
        {
            return null;
        }
    }

    public static object getWeaponInfo(CombatEntity c, string Name) 
    {
        if(c.getInfo("Weapon")==null)
        {
            return null; 
        }
        Weapons w = new Weapons((int) c.getInfo("Weapon"));
        return w.getInfo(Name);
    }

    public static object getWeaponInfo(int id, string Name){
        Weapons w = new Weapons(id);
        return w.getInfo(Name);
    }

    public void changeValue(string name, int value)
    {
        if (WeaponInfo != null && WeaponInfo.ContainsKey(name))
        {
            int val = (int)WeaponInfo[name];
            val += value;
            WeaponInfo[name] = val;
        }
    }

    public void printValue(Text t)
    {
        t.text = "";
        foreach(var item in WeaponInfo)
        {
            t.text += item.Key;
            t.text += ": ";
            t.text += item.Value.ToString();
            t.text += "\n";
        }
    }
}
