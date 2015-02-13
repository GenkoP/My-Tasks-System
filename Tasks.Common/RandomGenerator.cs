namespace Tasks.Common
{
    using System;
    using System.Text;
    using System.Collections.Generic;


    public class RandomGenerator : IRandomGenerator
    {
        private const string LETTERS_UPPERCASE = "ABCDEFGHIJKLMNOPQRSTUWXVYZ";
        private const string LETTERS_LOWERCASE = "abcdefghijklmnopqrstuwxvyz";
        

        private Random random;

        public RandomGenerator()
        {
            this.random = new Random();
        }

        public string RandomMixedString(int minLength = 5, int maxLength = 50)
        {
            var letters = string.Format("{0}{1}", LETTERS_UPPERCASE, LETTERS_LOWERCASE); 

            var result = new StringBuilder();
            var length = this.random.Next(minLength, maxLength + 1);
            for (int i = 0; i <= length; i++)
            {
                result.Append(letters[this.random.Next(0, letters.Length)]);
            }

            return result.ToString();
        }

        public string RandomLowercaseLetters(int minLength , int maxLength)
        {
            var result = new StringBuilder();
            var length = this.random.Next(minLength, maxLength + 1);
            for (int i = 0; i <= length; i++)
            {
                result.Append(LETTERS_LOWERCASE[this.random.Next(0, LETTERS_LOWERCASE.Length)]);
            }

            return result.ToString();
        }

        public int RandomNumber(int min, int max)
        {
            return this.random.Next(min, max + 1);
        }

        public string GenerateEmail()
        {
            return string.Format("{0}@{1}.{2}", this.RandomLowercaseLetters(3, 6), this.RandomLowercaseLetters(4, 5), this.RandomLowercaseLetters(2, 3));
        }
    }
}
