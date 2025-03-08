namespace WcfService
{
    public class Service : IService
    {
        public string GetMessage(string name)
        {
            return $"Hello, {name}! This is a SOAP WCF service running on .NET 9";
        }

        public UserData GetUserData(string userId)
        {
            // Sample implementation - in real scenario, this would fetch from database
            var data =new UserData
            {
                Name = userId,
                Email = $"{userId}@example.com"
            };

            Console.WriteLine($"User: {data.Name}, Email: {data.Email}");

            return data;
        }
    }
}
