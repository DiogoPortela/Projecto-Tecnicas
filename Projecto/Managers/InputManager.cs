using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto
{
    static class InputManager
    {
        static public MovementInput1 MovementPlayerOne = new MovementInput1();
        static public MovementInput2 MovementPlayerTwo = new MovementInput2();
        static public AttackDefenseInput1 CombatInput = new AttackDefenseInput1();
    }
}
