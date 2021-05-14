using System;
using System.ComponentModel;
using System.IO;
using System.Media;

namespace ConsoleMusic
{
    class Program
    {
        private static SoundPlayer Player = new SoundPlayer();
        private static void LoadAsyncSound()
        {
            try
            {
                //You need to add your logic here to randomize the file path
                Player.Stream = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\music.wav");
                Player.LoadAsync();
            }
            catch (Exception ex)
            {

            }
        }
        private static void Player_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (Player.IsLoadCompleted)
            {
                try
                {
                    Player.PlaySync();
                    LoadAsyncSound();
                }
                catch (Exception ex)
                {
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Player.LoadCompleted += new AsyncCompletedEventHandler(Player_LoadCompleted);
            LoadAsyncSound();
            while (true)
            {
                Console.WriteLine("Enter key");
                Console.ReadLine();
                Console.WriteLine("My code is running");
            }
        }
    }
}
