using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Projecto
{
    static class SoundManager
    {
        static Dictionary<string, SoundEffect> listSounds;
        static Dictionary<string, SoundEffectInstance> listPlayingSounds;

        /// <summary>
        /// Starts the class, and loads all sounds.
        /// </summary>
        static public void Start()
        {
            listSounds = new Dictionary<string, SoundEffect>();
            listPlayingSounds = new Dictionary<string, SoundEffectInstance>();

            List<string> stringAux = new List<string>();

            //stringAux.Add("mainMenuTheme");
            stringAux.Add("mainGameTheme");
            stringAux.Add("attack_magico");
            stringAux.Add("attack1");
            stringAux.Add("morrer");
            stringAux.Add("troca_arma");
            foreach (string s in stringAux)
            {
                SoundEffect SoundFX = Game1.content.Load<SoundEffect>(s);
                listSounds.Add(s, SoundFX);
            }
        }
        /// <summary>
        /// Starts a sound.
        /// </summary>
        /// <param name="name"> Sound name. </param>
        /// <param name="isLoop"> True if the sound should be on loop. </param>
        static public void StartSound(string name, bool isLoop)
        {
            if (listSounds.ContainsKey(name))
            {
                SoundEffectInstance aux = listSounds[name].CreateInstance();
                aux.Play();
                aux.IsLooped = isLoop;
                if (!listPlayingSounds.ContainsKey(name))
                    listPlayingSounds.Add(name, aux);
            }
            else
            {
                Debug.NewLine("Sound:\"" + name + "\" doesn't exist.");
            }
        }
        /// <summary>
        /// Stops a sound.
        /// </summary>
        /// <param name="name">Sound name.</param>
        static public void StopSound(string name)
        {
            if (listPlayingSounds.ContainsKey(name))
            {
                listPlayingSounds[name].Stop();
                listPlayingSounds.Remove(name);
            }
            else
            {
                Debug.NewLine("Sound:\"" + name + "\" doesn't exist.");
            }
        }
        /// <summary>
        /// Stops all sounds.
        /// </summary>
        static public void StopAllSounds()
        {
            foreach (KeyValuePair<string, SoundEffectInstance> soundFX in listPlayingSounds)
            {
                soundFX.Value.Stop();
            }
            listPlayingSounds.Clear();
        }
    }
}