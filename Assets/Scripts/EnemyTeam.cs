using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeam
{
    private List<CombatEntity> enemyTeam;
    private int numOfMem;

    public EnemyTeam()
    {
        enemyTeam = new List<CombatEntity>();
        numOfMem = 0;
    }
    // Start is called before the first frame update
    public void addMember(string name)
    {
        enemyTeam.Add(new CombatEntity(new Enemy(name)));
        numOfMem++;
    }
    public void removeMember(string name)
    {
        foreach (CombatEntity e in enemyTeam)
        {
            if (e.getInfo("Name").Equals(name))
            {
                enemyTeam.Remove(e);
                numOfMem--;
                return;
            }
        }
    }

    public List<CombatEntity> getTeam()
    {
        return enemyTeam;
    }
    public int getTeamCount()
    {
        return numOfMem;
    }
}
