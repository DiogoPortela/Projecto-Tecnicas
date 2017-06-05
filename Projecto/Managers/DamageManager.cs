
<<<<<<< Updated upstream
=======

        /// Devolve o tipo de dano baseado nas armas que o player usa
        /// </summary>
        public void GetDamageValues()
        {
            if (mainHandWeapon != null)
            {
                this.PhysDmg = this.mainHandWeapon.MainPhysicalDamage;
                this.MagicDmg = this.mainHandWeapon.MainMagicalDamage;
                this.Range = this.mainHandWeapon.MainAtackRange;
                this.AtackSpeed = this.mainHandWeapon.MainAtackSpeed;
            }

            if (offHandWeapon != null)
            {
                this.PhysDmg *= offHandWeapon.ModPhysicalDamage;
                this.MagicDmg *= offHandWeapon.ModMagicalDamage;
                this.Range = (int)(this.Range * offHandWeapon.ModAttackRange);
                this.AtackSpeed *= offHandWeapon.ModAttackSpeed;
            }

            this.MagicDmgRes = (float)Constants.MagicalResistance + (MagicDmg * 0.2f); // resistencia a magic
            this.PhysDmgRes = (float)Constants.PhysicalResistance + (PhysDmgRes * 0.2f); // resistencia a fisico

            //if (this.MHweapon == GameState.AllWeapons[1] && this.OHweapon == GameState.AllWeapons[0]) // Staff + Sword
            //{
            //    //this.PhysDmg = PhysDmg + MHweapon.WeaponPhysicalDamage+(OHweapon.WeaponPhysicalDamage * 0.5f); // dano fisico
            //    this.PhysDmg = 0f;
            //    this.MagicDmg = (float)Cons.MagicalDamage + MHweapon.WeaponMagicalDamage + (OHweapon.WeaponMagicalDamage * 0.5f); // dano magico
            //    this.MagicDmgRes = (float)Cons.MagicalResistance + (MagicDmg * 0.2f); // resistencia a magic
            //    this.PhysDmgRes = (float)Cons.PhysicalResistance + (PhysDmgRes * 0.2f); // resistencia a fisico
            //    this.Range = MHweapon.WeaponAtackRange + (int)Cons.AtackRange; // Range
            //    this.AtackSpeed = MHweapon.WeaponAtackSpeed * (float)Cons.AtackSpeed; // AtackSpeed
            //    Debug.NewLine(this.MagicDmg.ToString());
            //    Debug.NewLine(this.PhysDmg.ToString());
            //}
            //else if (this.MHweapon == GameState.AllWeapons[0] && this.OHweapon == GameState.AllWeapons[1]) // sword+ staff
            //{
            //    this.PhysDmg = (float)Cons.PhysicalDamage + MHweapon.WeaponPhysicalDamage + (OHweapon.WeaponPhysicalDamage * 0.5f); // dano fisico
            //    //this.MagicDmg = MagicDmg + MHweapon.WeaponMagicalDamage + (OHweapon.WeaponMagicalDamage * 0.5f); // dano magico
            //    this.MagicDmg = 0f;
            //    this.MagicDmgRes = (float)Cons.MagicalResistance + (MagicDmg * 0.2f); // resistencia a magic
            //    this.PhysDmgRes = (float)Cons.PhysicalResistance + (PhysDmgRes * 0.2f); // resistencia a fisico
            //    this.Range = MHweapon.WeaponAtackRange + (int)Cons.AtackRange; // Range
            //    this.AtackSpeed = MHweapon.WeaponAtackSpeed * (float)Cons.AtackSpeed; // Atack Speed
            //    Debug.NewLine(this.MagicDmg.ToString());
            //    Debug.NewLine(this.PhysDmg.ToString());
            //}
        }
        /// <summary>
        /// Switches player weapons
        /// </summary>
        public void SwitchWeapons()
        {
            //if (InputManager.PressedLastFrame.K == ButtonState.Pressed && this.MHweapon == GameState.AllWeapons[0]
            //    && this.OHweapon == GameState.AllWeapons[1])
            //{
            //    this.MHweapon = GameState.AllWeapons[1];
            //    this.OHweapon = GameState.AllWeapons[0];
            //}else if(InputManager.PressedLastFrame.K == ButtonState.Pressed && this.MHweapon == GameState.AllWeapons[1]
            //    && this.OHweapon == GameState.AllWeapons[0])
            //{
            //    this.MHweapon = GameState.AllWeapons[0];
            //    this.OHweapon = GameState.AllWeapons[1];
            //}
            Weapon aux = this.mainHandWeapon;
            this.mainHandWeapon = this.offHandWeapon;
            this.offHandWeapon = aux;
            SoundManager.StartSound("troca_arma", false);

        }
        /// <summary>
        /// Changes a player weapon by a new one.
        /// </summary>
        /// <param name="hand">Hand of the player.</param>
        /// <param name="newWeapon">New weapon to replace old one.</param>
        public void ChangeWeapon(WeaponHand hand, Weapon newWeapon)
        {
            if (hand == WeaponHand.MainHand)
            {
                this.mainHandWeapon = newWeapon;
                
            }
            else
            {
                this.offHandWeapon = newWeapon;

                
            }
           
            GetDamageValues();
            
        }

        public void Attack()
        {
            if (mainHandWeapon == null)
                return;
            else if (mainHandWeapon.isRanged)
            {
                Bullet bullet = new Bullet(this.Center, this.objectDiretion, mainHandWeapon.MainAtackRange, this);
                GameState.BulletList.Add(bullet);
                SoundManager.StartSound("attack_magico", false);

            }
            else
            {
                List<Enemy> auxEnemy = new List<Enemy>();

                foreach (Enemy enemy in GameState.EnemyList)
                {
                    if (this.IsInRange(enemy) == true)
                    {
                        auxEnemy.Add(enemy);
                        
                    }
                    
                }

                foreach (Enemy enemy in auxEnemy)
                {
                    ParticleSystem p = new ParticleSystem(ParticleType.Smoke, "DebugPixel", enemy.Center, Vector2.One * 0.5f, 4, 0, 3, 500, 500, 10);
                    p.Start();
                    GameState.ParticlesList.Add(p);
                    EnemyTakeDamage(enemy);
                    if (enemy.HP <= 0)
                    {
                        GameState.EnemyList.Remove(enemy);
                        SoundManager.StartSound("morrer", false);
                    }
                }
                SoundManager.StartSound("attack1", false);
            }
            
        }
>>>>>>> Stashed changes
        /// <summary>