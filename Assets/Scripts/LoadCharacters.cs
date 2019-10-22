using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCharacters: MonoBehaviour
{
    public Text Char;
    public Text En;
    // Start is called before the first frame update
    void Start()
    {
        CharacterClass newChar = new CharacterClass("C");
        CombatEntity me = new CombatEntity(newChar);
        Enemy newEn = new Enemy("B");
        CombatEntity him = new CombatEntity(newEn);
        test(me, him);
        /* 
        CharacterClass newChar = new CharacterClass("C");
        CombatEntity me = new CombatEntity(newChar);
        Char.text = "";
        Char.text += Weapons.getWeaponInfo(me, "ATK");
        */
    }

    public void test(CombatEntity me, CombatEntity him){
        Combat.elementVersus(me, him, 10);
        me.printValue(Char);
        him.printValue(En);
    }
}
