using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto
{
    //public class CombatMod
    //{

    //    public bool IncHP { get; set; }

    //    public bool IncPhysDmg { get; set; }

    //    public bool IncMagicDmg { get; set; }

    //    public bool IncMS { get; set; }

    //    public bool SlowAoE { get; set; }

    //    public bool LowerRes { get; set; }

    //    public bool MagicDmgRes { get; set; }

    //    public bool PhysDmgRes { get; set; }
        
    //    public CombatMod()
    //    {
    //        Random rand = new Random();

    //        this.IncHP = false;
    //        this.IncPhysDmg = false;
    //        this.IncMagicDmg = false;
    //        this.IncMS = false;
    //        this.SlowAoE = false;
    //        this.LowerRes = false;
    //        this.MagicDmgRes = false;
    //        this.PhysDmgRes = false;


    //    }

    //    private readonly Player player;
    //    private readonly List<Enemy> enemies;

    //    // When we construct the CombatManager class we want to pass in references
    //    // to the player and the list of enemies.
    //    public CombatMod(Player player, List<Enemy> enemies)
    //    {
    //        player = PlayerNumber.playerOne;
    //        enemies = new List<Enemy>();
    //    }

    //    // Use this method to resolve attacks between Figures
    //    public void Attack(Player playerone , Enemy defender)
    //    {
    //        if (IncPhysDmg + IncMagicDmg >= defender.PhysDmgRes + MagicDmgRes)
    //        {
    //            // Lower the defender's health by the amount of damage
    //            defender.HP -=(IncPhysDmg + IncMagicDmg)% (defender.PhysDmgRes + MagicDmgRes);
    //            // Write a combat message to the debug log.ideia
    //           /* Debug.WriteLine("{0} hit {1} for {2} and he has {3} health remaining.",
    //              attacker.Name, defender.Name, damage, defender.Health);*/
    //           if (defender.HP <= 0)
    //            {
    //                if (defender is enemies)
    //                {
    //                    var enemy = defender as enemies;
    //                    // When an enemies health dropped below 0 they died
    //                    // Remove that enemy from the game
    //                    enemies.Remove(enemy);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            // Show the miss message in the Debug log for now
    //           // Debug.WriteLine("{0} missed {1}", attacker.Name, defender.Name);
    //        }
    //    }

    //    public void Defense(Player defensive)
    //    {
    //        defensive.HP = defensive.HP;
    //    }

    
    //}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto
{
    public class CombatMod
    {

        public bool IncHP { get; set; }

        public bool IncPhysDmg { get; set; }

        public bool IncMagicDmg { get; set; }

        public bool IncMS { get; set; }

        public bool SlowAoE { get; set; }

        public bool LowerRes { get; set; }

        public bool MagicDmgRes { get; set; }

        public bool PhysDmgRes { get; set; }

        public CombatMod()
        {
            Random rand = new Random();

            this.IncHP = false;
            this.IncPhysDmg = false;
            this.IncMagicDmg = false;
            this.IncMS = false;
            this.SlowAoE = false;
            this.LowerRes = false;
            this.MagicDmgRes = false;
            this.PhysDmgRes = false;


        }
    }
}