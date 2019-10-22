using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy
{
    private Dictionary<string, object> EnemyInfo;
    public Enemy(string Name)
    {
        DataTable Enemy;
        SqliteDatabase sqlDB = new SqliteDatabase("GameDB.db");
        string query = "SELECT * from Enemy Where Name = '" + Name + "'";
        Enemy = sqlDB.ExecuteQuery(query);
        EnemyInfo = Enemy.Rows[0];
    }

    public object getInfo(string name)
    {
        if (EnemyInfo.ContainsKey(name)){
            return EnemyInfo[name];
        }
        return null;
    }

    public void printValue(Text t)
    {
        t.text = "";
        foreach (var item in EnemyInfo)
        {
            t.text += item.Key;
            t.text += ": ";
            t.text += item.Value.ToString();
            t.text += "\n";
        }
    }
}
