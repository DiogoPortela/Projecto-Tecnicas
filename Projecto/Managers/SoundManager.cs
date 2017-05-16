using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace Projecto.Managers
{
    class SoundManager
    {
        bool songStart = false;
        Song mainMenuTheme;
        Song mainGameTheme;
        // LoadContent method:
       /* void LoadContent()
        {
            mainMenuTheme = content.load<Song>("mainMenuTheme");
            mainGameTheme = content.load<Song>("mainGameTheme");
            MediaPlayer.IsRepeating = true;
        }*/

        // Update method:
        void UpdateSong()
        {
            switch (Game1.selectedScreen)
            {
                case ScreenSelect.MainMenu:
                    if ( !songStart)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(mainMenuTheme);
                        songStart = true;
                    }
                    break;
                case ScreenSelect.Playing:
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(mainGameTheme);
                        songStart = true;
                    }
                    break;
            }
        }
    }
}
