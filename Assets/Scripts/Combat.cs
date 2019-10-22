using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat
{
    enum Elements
    {
        FIRE = 0,
        EARTH = 1,
        METAL = 2,
        WATER = 3,
        WOOD = 4,
    }

    enum Class
    {
        ROGUE = 0,
        HEALER = 1,
        RANGER = 2,
        MAGE = 3,
        WARRIOR = 4,
    }

    /*DEF/ATK			 fire 	   earth	   metal		water 	   wood
    			fire	   0	     10		   -10			  5			-5				
   				earth	  -5 		  0			10		    -10			 5		
  				metal	   5		 -5			 0			 10		   -10		
   				water	 -10		  5		    -5			  0			10		
   				wood	  10		-10			 5			 -5			 0

    Class	Rogue   Healer  Ranger	  Mage	   Warrior
      IPP   Highest Lowest  Highest   Lowest   Next to Highest
 	   ID      0	  1		  2		    3	   4
   */
    static int[,] BONUS = new int[5, 5] { { 0, 10, -10, 5, -5 }, { -5, 0, 10, -10, 5 }, { 5, -5, 0, 10, -10 }, { -10, 5, -5, 0, 10 }, { 10, -10, 5, -5, 0 } };
    static List<Action<int, CombatEntity>> ElementMethods = new List<Action<int, CombatEntity>>()
    {
        BURNT, STUN, CRITICAL, SLIPPERY, VAMPIRE
    };
    
    public static List<CombatEntity> IPPOrder = new List<CombatEntity>();
    public static List<CombatEntity> EnemyIPPOrder = new List<CombatEntity>();
    public static List<CombatEntity> HIPPOrder = new List<CombatEntity>();
    public static List<CombatEntity> EnemyHIPPOrder = new List<CombatEntity>();

    public static void beginCombat(EnemyTeam enemyTeam)
    {
        
    }

    public static void BURNT(int bonus, CombatEntity Defender)
    {
        float percentage = 100f;

        int value = (int)Math.Round(bonus * percentage / 100);
        Defender.changeValue("DamageOverTime", value);
        Defender.changeValue("TurnBurnt", 3);
    }

    public static void STUN(int bonus, CombatEntity Defender)
    {
        float percentage = 100f;

        int stunChance = (int)Math.Round(bonus * percentage / 100);
        System.Random rand = new System.Random();
        if (rand.Next(1, 100) <= stunChance)
        {
            Defender.changeValue("IsStunned", 1);
        }
    }

    public static void CRITICAL(int bonus, CombatEntity Attacker)
    {
        float percentage = 100f;
        int value = (int) Math.Round(bonus * percentage / 100);
        if (((int)Attacker.getInfo("Crit") + value) < 100) {
            Attacker.changeValue("Crit", value);
        } else
        {
            Attacker.setValue("Crit", value);
        }
    }

    public static void SLIPPERY(int bonus, CombatEntity Defender)
    {
        float percentage = 100f;
        int value = (int) Math.Round(bonus * percentage / 100);
        if (((int) Defender.getInfo("Accuracy") - value) > 0)
        {
          Defender.changeValue("Accuracy",-value);
        }
        else
        {
          Defender.setValue("Accuracy", 0);
        }
    }

    public static void VAMPIRE(int bonus, CombatEntity Attacker)
    {
        float percentage = 100f;
        int value = (int) Math.Round(bonus * percentage / 100);
        if (((int)Attacker.getInfo("CHealth") + value) < (int) Attacker.getInfo("MHealth"))
        {
          Attacker.changeValue("CHealth", value);
        }
        else
        {
          Attacker.setValue("CHealth", (int)Attacker.getInfo("MHealth"));
        }
    }


    public static int elementVersus(CombatEntity Attacker, CombatEntity Defender, int damage)
    {
        int ATKE = (int)Attacker.getInfo("Element");
        int DEFE = (int)Defender.getInfo("Element");
        int bonus = damage * BONUS[ATKE, DEFE];
        Debug.Log(bonus);
        if (bonus > 0)
        {
            if (ATKE == (int) Elements.WOOD)
            {
                ElementMethods[ATKE](bonus, Attacker);
            }
            else
            {
                ElementMethods[ATKE](bonus, Defender);
            }
        }
        return bonus;
    }

    public static void QueueIPP(CombatEntity c, List<CombatEntity> l){
        //int IPP =  ((+Agi*x) + Class ID*y + Level*z + (1-Element ID*n) + IPPP (belong to weapon) )
        int IPP = (((int)c.getInfo("Agi")) * 1 + ((int)c.getInfo("Class")) * 1 + ((int)c.getInfo("Level")) * 1 + (1 - ((int)c.getInfo("Element") * 1)) + (int)Weapons.getWeaponInfo(c, "IPPP"));///(difference);
        int sortCompleted = 0;
        string name = (string) c.getInfo("Name");
        for(int i = 0; i<l.Count; i++){
            CombatEntity temp = l[i];
            if(name.Equals((string) temp.getInfo("Name"))){
                l.Remove(l[i]);
            }

        }
    }
}
