namespace E_Commerce_api.Utils
{
    public class HelperClass
    {
        // using static method
        public static bool isInvalidName(string name) => string.IsNullOrEmpty(name) || name.Length < 3 || name.Length > 50;
    }
}
