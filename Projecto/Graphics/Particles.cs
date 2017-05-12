using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projecto
{
    class Particle
    {
        public Vector2 Position;
        public Vector2 Direction;
        public byte Alpha;
        public TimeSpan TimeLeft;
        public bool isDone;

        //------------->CONSTRUCTORS<-------------//

        public Particle(Vector2 position, Vector2 direction, float timeLeft)
        {
            Position = position;
            Direction = direction;
            Alpha = byte.MaxValue;
            TimeLeft = TimeSpan.FromMilliseconds(timeLeft);
            isDone = false;
        }
        
        //------------->FUNCTIONS && METHODS<-------------//

        public void Update(GameTime time)
        {
            Position += Direction / time.ElapsedGameTime.Milliseconds;
        }
    }
    class ParticleSystem
    {
        public Vector2 Position;
        public Vector2 TextureSize;
        public Texture2D Texture;
        public Particle[] Particles;
        public int NumberOfParticles;
        public int MaxParticles;
        public int SpawnRate;
        public float SpawnRadius;
        public float ParticleLifeSpan;
        public byte ParticleAlphaDecreaseRate;
        public float TimeLeft;
        public bool isStopped;
        public bool isActive;

        private TimeSpan SpawnTimer;

        //------------->CONSTRUCTORS<-------------//

        public ParticleSystem(string texture, Vector2 position, Vector2 textureSize, int maxParticles, int spawnRate, float spawnRadius, float timeLeft, float particleLifeSpan, byte particleAlphaDecreaseRate)
        {
            Position = position;
            TextureSize = textureSize;
            Texture = Game1.content.Load<Texture2D>(texture);
            MaxParticles = maxParticles;
            Particles = new Particle[maxParticles];
            NumberOfParticles = 0;
            TimeLeft = timeLeft;
            SpawnRate = spawnRate;
            SpawnTimer = TimeSpan.FromMilliseconds(SpawnRate);
            SpawnRadius = spawnRadius;
            ParticleLifeSpan = particleLifeSpan;
            ParticleAlphaDecreaseRate = particleAlphaDecreaseRate;
            isActive = true;
            isStopped = true;
        }

        //------------->FUNCTIONS && METHODS<-------------//

        public void Start()
        {
            isStopped = false;
        }
        public void Stop()
        {
            isStopped = true;
            Particles = new Particle[MaxParticles];
            NumberOfParticles = 0;
        }
        public void Update(GameTime time, Vector2 position)
        {
            if (!isStopped)
            {
                SpawnTimer -= time.ElapsedGameTime;
                Position = position;

                for (int i = 0; i < MaxParticles; i++)
                {
                    if (Particles[i] == null)
                        break;
                    Particles[i].TimeLeft -= time.ElapsedGameTime;
                    Particles[i].Alpha -= ParticleAlphaDecreaseRate;
                    Particles[i].Update(time);
                    if (Particles[i].TimeLeft.Milliseconds <= 0)
                        Particles[i].isDone = true;
                    //POR O ALPHA AQUI
                }

                if (SpawnTimer <= TimeSpan.FromMilliseconds(0))
                {
                    SpawnTimer = TimeSpan.FromMilliseconds(SpawnRate);

                    int? freePostion = null;

                    for (int i = 0; i < MaxParticles; i++)
                    {
                        if (Particles[i] == null || Particles[i].isDone)
                        {
                            freePostion = i;
                            break;
                        }
                    }

                    if (freePostion != null)
                    {
                        Vector2 Pos = new Vector2(Game1.random.Next((int)SpawnRadius * 100) / 100, 0);
                        int rotation = Game1.random.Next(360);
                        Vector2 PosFinal;
                        PosFinal.X = Pos.X * (float)Math.Cos(rotation) - Pos.Y * (float)Math.Sin(rotation);
                        PosFinal.Y = Pos.X * (float)Math.Sin(rotation) + Pos.Y * (float)Math.Cos(rotation);
                        Vector2 Norm = PosFinal;
                        Norm.Normalize();
                        PosFinal += this.Position;

                        Particle aux = new Particle(PosFinal, Norm, ParticleLifeSpan);
                        Particles[(int)freePostion] = aux;
                        if (NumberOfParticles < MaxParticles)
                            NumberOfParticles++;

                    }
                }
            }
        }
        public void Draw(Camera camera)
        {
            if (isActive)
            {
                for (int i = 0; i < MaxParticles; i++)
                {
                    if (Particles[i] == null)
                        break;
                    Particle p = Particles[i];
                    try
                    {
                        Rectangle rectAux = camera.CalculatePixelRectangle(p.Position, this.TextureSize);
                        Color aux = Color.Green;
                        aux.A = p.Alpha;
                        Game1.spriteBatch.Draw(Texture, rectAux, aux);
                    }
                    catch
                    {
                        Debug.NewLine("Error.");
                    }
                }
            }
        }
    }
}

