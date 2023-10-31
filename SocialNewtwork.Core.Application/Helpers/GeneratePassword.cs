using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNewtwork.Core.Application.Helpers
{
    public class GeneratePassword
    {
        public static string Generate()
        {
            int longitud = 10;
            string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@";
            Random random = new Random();
            StringBuilder aleatoria = new StringBuilder(longitud);

            aleatoria.Append(caracteresPermitidos[random.Next(26)]); 
            aleatoria.Append(caracteresPermitidos[26 + random.Next(26)]); 
            aleatoria.Append(caracteresPermitidos[52 + random.Next(10)]);
            aleatoria.Append('@'); 

            for (int i = 4; i < longitud; i++)
            {
                aleatoria.Append(caracteresPermitidos[random.Next(caracteresPermitidos.Length)]);
            }
            string cadenaAleatoria = Shuffle(aleatoria.ToString());
            return cadenaAleatoria;
        }

        static string Shuffle(string input)
        {
            char[] array = input.ToCharArray();
            Random random = new Random();
            int n = array.Length;

            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                char value = array[k];
                array[k] = array[n];
                array[n] = value;
            }

            return new string(array);
        }
    }
}

