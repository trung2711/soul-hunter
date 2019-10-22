using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    private static List<CombatEntity> myTeam = new List<CombatEntity>();
    private static int numOfMem = 0;
    // Start is called before the first frame update
    public static void addMember(string name)
    {
        CombatEntity c = new CombatEntity(new CharacterClass(name));
        if (c != null)
        {
            myTeam.Add(c);
            numOfMem++;
        }
    }
    public static void removeMember(string name)
    {
        foreach(CombatEntity c in myTeam)
        {
            if (c.getInfo("Name").Equals(name))
            {
                myTeam.Remove(c);
                numOfMem--;
                return;
            }
        }
    }

    public static List<CombatEntity> getTeam()
    {
        return myTeam;
    }
    public static int getTeamCount()
    {
        return numOfMem;
    }
}
