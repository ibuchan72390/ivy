namespace IBFramework.TestUtilities
{
    /*
     * Simply returns a new int, string, double, or decimal
     * from a static increasing int value to ensure that you
     * will always get a "new" id value.
     */

    public static class TestIncrementer
    {
        private static int id = 0;

        public static int IntVal
        {
            get
            {
                id++;
                return id;
            }
        }

        public static double DoubleVal
        {
            get
            {
                id++;
                return id;
            }
        }

        public static string StringVal
        {
            get
            {
                id++;
                return id.ToString();
            }
        }
    }
}
