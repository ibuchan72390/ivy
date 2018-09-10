namespace Ivy.TestHelper.TestValues
{
    public static class MySqlTestValues
    {
        public const string TestDataDbConnectionString = "Data Source=localhost;Initial Catalog=framework_test;Uid=root;Pwd=Password00!;SslMode=none;";

        public const string TestDbConnectionString = TestDataDbConnectionString + "IgnoreCommandTransaction=true;";
    }
}
