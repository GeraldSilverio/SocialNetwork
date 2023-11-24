using System.Text;

namespace SocialNewtwork.Core.Application.Helpers
{
    public class GeneratePassword
    {
        public static string Generate()
        {
            int leghtOfPassword = 10;
            string allowedcharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@";
            Random random = new Random();
            StringBuilder aleatorie = new StringBuilder(leghtOfPassword);

            aleatorie.Append(allowedcharacters[random.Next(26)]);
            aleatorie.Append(allowedcharacters[26 + random.Next(26)]);
            aleatorie.Append(allowedcharacters[52 + random.Next(10)]);
            aleatorie.Append('@'); 

            for (int i = 4; i < leghtOfPassword; i++)
            {
                aleatorie.Append(allowedcharacters[random.Next(allowedcharacters.Length)]);
            }
            string cadenaAleatoria = Shuffle(aleatorie.ToString());
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

