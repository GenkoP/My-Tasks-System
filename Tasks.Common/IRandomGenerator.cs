namespace Tasks.Common
{
    public interface IRandomGenerator
    {

        string GenerateEmail();

        int RandomNumber(int min, int max);

        string RandomLowercaseLetters(int minLength, int maxLength);

        string RandomMixedString(int minLength = 5, int maxLength = 50);
    }
}
