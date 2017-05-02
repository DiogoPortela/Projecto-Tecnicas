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
        public float Alpha;
        public TimeSpan TimeLeft;
        public bool isDone;

        public Particle(Vector2 position, Vector2 direction, float timeLeft)
        {
            Position = position;
            Direction = direction;
            Alpha = 100;
            TimeLeft = TimeSpan.FromMilliseconds(timeLeft);
            isDone = false;
        }

        public void Update()
        {
            Position += Direction;
        }
    }
    class ParticleSystem
    {
        public Vector2 Position;
        public Vector2 TextureSize;
        public Texture2D Texture;
        public Particle[] Particles;
        public int MaxParticles;
        public int SpawnRate;
        public float SpawnRadius;
        public float ParticleLifeSpan;
        public float ParticleAlphaDecreaseRate;
        public float TimeLeft;
        public bool isStopped;
        public bool isActive;

        private TimeSpan SpawnTimer;

        public ParticleSystem(string texture, Vector2 position, Vector2 textureSize, int maxParticles, int spawnRate, float spawnRadius, float timeLeft, float particleLifeSpan, float particleAlphaDecreaseRate )
        {
            Position = position;
            TextureSize = textureSize;
            Texture = Game1.content.Load<Texture2D>(texture);
            MaxParticles = maxParticles;
            Particles = new Particle[maxParticles];
            TimeLeft = timeLeft;
            SpawnRate = spawnRate;
            SpawnRadius = spawnRadius;
            ParticleLifeSpan = particleLifeSpan;
            ParticleAlphaDecreaseRate = particleAlphaDecreaseRate;
            isActive = true;
            isStopped = true;
        }

        public void Start()
        {
            isStopped = false;
        }
        public void Stop()
        {
            isStopped = true;
        }
        public void Update(GameTime time, Vector2 deltaMovement)
        {
            if(!isStopped)
            {
                SpawnTimer -= time.ElapsedGameTime;
                Position = Position + deltaMovement;

                foreach(Particle p in Particles)
                {
                    p.TimeLeft -= time.ElapsedGameTime;
                    if (p.TimeLeft.Milliseconds <= 0)
                        p.isDone = true;
                    //POR O ALPHA AQUI
                }

                if(SpawnTimer <= TimeSpan.FromMilliseconds(0))
                {
                    SpawnTimer = TimeSpan.FromMilliseconds(SpawnRate);

                    if (Particles.Length < MaxParticles)
                    {
                        int? freePostion = null;

                        for (int i = 0; i > MaxParticles; i++)
                        {
                            if (Particles[i].isDone)
                            {
                                freePostion = i;
                                break;
                            }
                        }

                        if(freePostion != null)
                        {

                            Particle aux = new Particle(,, ParticleLifeSpan);
                        }
                    }
                }
            }
        }
        public void Draw(Camera camera)
        {
            if(isActive)
            {
                foreach (Particle p in Particles)
                {
                    Rectangle rectAux = camera.CalculatePixelRectangle(p.Position, this.TextureSize);
                    Game1.spriteBatch.Draw(Texture, rectAux, Color.White);
                }
            }            
        }
    }
}
