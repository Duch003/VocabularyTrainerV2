using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace UI.Models
{
    public static class RandomPick
    {
        //Source for shuffles: https://stackoverflow.com/questions/273313/randomize-a-listt

        public static List<T> Shuffle<T>(List<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do
                    provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        public static List<T> SimpleShuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        public static T Draw<T>(List<T> q)
        {
            var pool = q.Count() > 5 ? Enumerable.Range(0, 5) : Enumerable.Range(0, q.Count());
            pool = RandomPick.Shuffle(pool.ToList());
            var id = pool.First();
            return q.ElementAt(id);
        }
    }
}
