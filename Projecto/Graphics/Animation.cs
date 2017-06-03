using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projecto
{
    class Animation
    {
        public string name;
        public Texture2D spriteTexture;
        Vector2 size;
        public Rectangle currentFrameRec;

        int maxFrames;
        int currentFrame;
        float frameRate;
        float LastFrameTime;

        //------------->CONSTRUCTORS<-------------//

        public Animation(string name, string texture, Vector2 size, int numberFrames, float frameRate)
        {
            this.name = name;
            if (Game1.textureList.ContainsKey(name))
                spriteTexture = Game1.textureList[name];
            else
                Debug.NewLine("FILE MISSING: " + name);
            this.size = size;
            maxFrames = numberFrames;
            this.frameRate = frameRate;
            currentFrame = 0;
            LastFrameTime = 0;
        }

        //------------->FUNCTIONS && METHODS<-------------//

        public void Play(GameTime gameTime)
        {
            LastFrameTime += gameTime.ElapsedGameTime.Milliseconds;
            if(LastFrameTime > frameRate)
            {
                LastFrameTime -= frameRate;

                currentFrameRec = new Rectangle(spriteTexture.Width / maxFrames * currentFrame, 0, (int)size.X, (int)size.Y);

                currentFrame++;
                LastFrameTime = 0;
                if(currentFrame == maxFrames)
                {
                    currentFrame = 0;
                }
            }
        }
        public void Stop()
        {
            currentFrame = 0;
            LastFrameTime = 0;
        }
    }
}
