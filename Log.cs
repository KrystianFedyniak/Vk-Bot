using System.Collections.Generic;

namespace VkBot
{
    public class Log
    {
        public static Queue<string> tuPush = new Queue<string>();


        public static void Push(string msg) =>
            tuPush.Enqueue($"{msg}");
    }
}
