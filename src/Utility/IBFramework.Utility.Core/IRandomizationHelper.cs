namespace IBFramework.Utility.Core
{
    public interface IRandomizationHelper
    {
        string RandomString(int size = 10);

        int RandomInt(int min = 0, int max = 10000);

        double RandomDouble(double min = 0, double max = 10000);

        decimal RandomDecimal();
    }
}
