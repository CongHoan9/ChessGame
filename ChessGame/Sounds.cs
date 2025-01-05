using System.IO;
using System.Media;
namespace ChessGame
{
    public class Sounds
    {
        public static void PlaySound(Stream soundStream)
        {
            using SoundPlayer player = new(soundStream);
            player.Play();
        }
    }
}
