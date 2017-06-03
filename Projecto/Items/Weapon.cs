using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto
{
    public class Weapon
    {
        public string Name; // nome
        public float MainMagicalDamage; //dano magico da arma
        public float MainPhysicalDamage; //dano fisico da arma
        public int MainAtackRange; // range de ataque da arma
        public float MainAtackSpeed; // velocidade da arma

        public float ModMagicalDamage;
        public float ModPhysicalDamage;
        public float ModAttackRange;
        public float ModAttackSpeed;

        public bool isRanged;

        public Weapon(  string nome, bool isRanged, float magicalDamage, float physicalDamage, int attackRange, float attackSpeed,
                        float modMagicalDamage, float modPhysicalDamage, float modAttackRange, float modAttackSpeed)
        {
            this.Name = nome;
            this.isRanged = isRanged;
            this.MainMagicalDamage = magicalDamage;
            this.MainPhysicalDamage = physicalDamage;
            this.MainAtackRange = attackRange;
            this.MainAtackSpeed = attackSpeed;

            this.ModAttackRange = modMagicalDamage;
            this.ModPhysicalDamage = modPhysicalDamage;
            this.ModAttackRange = modAttackRange;
            this.ModAttackSpeed = modAttackSpeed;
        }
        public Weapon(string nome)
        {
            this.Name = nome;
            this.MainMagicalDamage = 50;
            this.MainPhysicalDamage = 50;
            this.MainAtackRange = 1;
            this.MainAtackSpeed = 1;
        }
    }
}
