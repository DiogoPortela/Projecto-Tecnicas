using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projecto
{
    internal enum ParticleType
    {
        Explosion, Random, Smoke
    }
    class Particle
    {
        public Vector2 Position;
        public Vector2 Direction;
        public Rectangle Rectangle;
        public Color Color;
        public TimeSpan TimeLeft;
        public bool isDone;
        public ParticleSystem Parent;


        //------------->CONSTRUCTORS<-------------//

        public Particle(Vector2 position, Vector2 direction, float timeLeft, ParticleSystem parent)
        {
            Position = position;
            Direction = direction;
            this.Color = Color.Green;
            TimeLeft = TimeSpan.FromMilliseconds(timeLeft);
            Parent = parent;
            isDone = false;
        }

        //------------->FUNCTIONS && METHODS<-------------//

        public void Update(GameTime time)
        {
            Position += Direction / time.ElapsedGameTime.Milliseconds;
            TimeLeft -= time.ElapsedGameTime;
            if (TimeLeft.Milliseconds <= 0)
                isDone = true;
            if (Parent.ParticleAlphaDecreaseRate > 0 && this.Color.A > Parent.ParticleAlphaDecreaseRate)
                this.Color.A -= Parent.ParticleAlphaDecreaseRate;
        }
    }
    class ParticleSystem
    {
        public ParticleType Type;
        public Vector2 Position;
        public Vector2 TextureSize;
        public Texture2D Texture;
        public Queue<Particle> Particles;
        public int MaxParticles;
        public int SpawnRate;
        public float SpawnRadius;
        public float ParticleLifeSpan;
        public byte ParticleAlphaDecreaseRate;
        public TimeSpan TimeLeft;
        public bool isLoop;
        public bool isStopped;
        public bool isActive;

        private int aux;
        private TimeSpan SpawnTimer;

        //------------->CONSTRUCTORS<-------------//
        /// <summary>
        /// Construct a particle System.
        /// </summary>
        /// <param name="type">Type of the system. Explosion goes around in every direction. Smoke goes up. Random is random.</param>
        /// <param name="texture">Texture for the particles. </param>
        /// <param name="position">Position of the particles. </param>
        /// <param name="textureSize">Size of each particle. </param>
        /// <param name="maxParticles">Maximum particles on scene. </param>
        /// <param name="spawnRate">Number of particles spawning per second. </param>
        /// <param name="spawnRadius">Radius around the position to spawn to. </param>
        /// <param name="timeLeft">Time left until the particle system is destroyed. </param>
        /// <param name="particleLifeSpan">Time left for each particle. </param>
        /// <param name="particleAlphaDecreaseRate">Velocity the alpha decresases of each particle. </param>
        public ParticleSystem(ParticleType type, string texture, Vector2 position, Vector2 textureSize, int maxParticles, int spawnRate, float spawnRadius, float timeLeft, float particleLifeSpan, byte particleAlphaDecreaseRate)
        {
            this.Type = type;
            Position = position;
            TextureSize = textureSize;
            if (Game1.textureList.ContainsKey(texture))
                Texture = Game1.textureList[texture];
            MaxParticles = maxParticles;
            Particles = new Queue<Particle>();

            if (timeLeft > 0)
            {
                TimeLeft = TimeSpan.FromMilliseconds(timeLeft);
                isLoop = false;
            }
            else
                isLoop = true;

            if (spawnRate > 0)
                SpawnRate = 1000 / spawnRate;
            else          
                SpawnRate = 0;            
            SpawnTimer = TimeSpan.FromMilliseconds(0);
            SpawnRadius = spawnRadius;
            ParticleLifeSpan = particleLifeSpan;
            ParticleAlphaDecreaseRate = particleAlphaDecreaseRate;
            aux = 0;
            isActive = true;
            isStopped = true;
        }

        //------------->FUNCTIONS && METHODS<-------------//
        
        /// <summary>
        /// Start a particle system.
        /// </summary>
        public void Start()
        {
            isStopped = false;
        }
        /// <summary>
        /// Stops a particle system.
        /// </summary>
        public void Stop()
        {
            aux = 0;
            isStopped = true;
            Particles = new Queue<Particle>();
        }
        /// <summary>
        /// Updates the particle system.
        /// </summary>
        /// <param name="time">Game time. </param>
        public void Update(GameTime time)
        {
            if (!isStopped)
            {
                TimeLeft -= time.ElapsedGameTime;

                foreach (Particle p in Particles)
                {
                    p.Update(time);
                }

                if (SpawnRate == 0)
                {
                    if (Particles.Count == 0)
                    {
                        for (int i = 0; i < MaxParticles; i++)
                        {
                            Particles.Enqueue(GenerateParticle());
                        }
                    }                    
                }
                else
                {
                    SpawnTimer -= time.ElapsedGameTime;
                    if (SpawnTimer.Milliseconds <= 0)
                    {
                        SpawnTimer = TimeSpan.FromMilliseconds(SpawnRate);
                        if (Particles.Count < MaxParticles)
                            Particles.Enqueue(GenerateParticle());
                    }
                }

                while (Particles.Count > 0 && Particles.Peek().isDone == true)
                {
                    Particles.Dequeue();
                }
            }
        }
        private Particle GenerateParticle()
        {
            Vector2 PosFinal = Vector2.Zero;
            Vector2 DirFinal = Vector2.Zero;
            if (Type == ParticleType.Random)
            {
                int randomFeed = (int)SpawnRadius * 100;
                Vector2 Pos = new Vector2(Game1.random.Next(randomFeed) / 100, 0);
                int rotation = Game1.random.Next(360);
                PosFinal.X = Pos.X * (float)Math.Cos(rotation) - Pos.Y * (float)Math.Sin(rotation);
                PosFinal.Y = Pos.X * (float)Math.Sin(rotation) + Pos.Y * (float)Math.Cos(rotation);
                DirFinal = PosFinal;
                DirFinal.Normalize();
                PosFinal += this.Position;
            }
            else if (Type == ParticleType.Explosion)
            {
                Vector2 Pos = new Vector2(SpawnRadius, 0);
                float rotation = ((float)Math.PI * 2 / MaxParticles) * aux;
                PosFinal.X = Pos.X * (float)Math.Cos(rotation) - Pos.Y * (float)Math.Sin(rotation);
                PosFinal.Y = Pos.X * (float)Math.Sin(rotation) + Pos.Y * (float)Math.Cos(rotation);
                DirFinal = PosFinal;
                DirFinal.Normalize();
                PosFinal += this.Position;
                aux++;
            }
            else if(Type == ParticleType.Smoke)
            {
                PosFinal = new Vector2( Game1.random.Next((int)-SpawnRadius * 100, (int)SpawnRadius * 100 + 1) /100, Game1.random.Next((int)SpawnRadius * 100) / 100);
                DirFinal = Vector2.UnitY;
                PosFinal += this.Position;
            }


            return (new Particle(PosFinal, DirFinal, ParticleLifeSpan, this));
        }
        /// <summary>
        /// Draws the particle system.
        /// </summary>
        /// <param name="camera">Camera to draw to. </param>
        public void Draw(Camera camera)
        {
            if (isActive)
            {
                foreach (Particle p in Particles)
                {
                    p.Rectangle = camera.CalculatePixelRectangle(p.Position, this.TextureSize);
                    Game1.spriteBatch.Draw(Texture, p.Rectangle, p.Color);
                }
            }
        }
    }
}

