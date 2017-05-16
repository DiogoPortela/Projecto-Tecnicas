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
        public float WeaponMagicalDamage; //dano magico da arma
        public float WeaponPhysicalDamage; //dano fisico da arma
        public int WeaponAtackRange; // range de ataque da arma
        public float WeaponAtackSpeed; // velocidade da arma

        public Weapon(string nome, float mDamage, float pDamage, int aRange, float aSpeed)
        {
            this.Name = nome;
            this.WeaponMagicalDamage = mDamage;
            this.WeaponPhysicalDamage = pDamage;
            this.WeaponAtackRange = aRange;
            this.WeaponAtackSpeed = aSpeed;
        }
        public Weapon(string nome)
        {
            this.Name = nome;
            this.WeaponMagicalDamage = 50;
            this.WeaponPhysicalDamage = 50;
            this.WeaponAtackRange = 1;
            this.WeaponAtackSpeed = 1;
        }
    }
}
