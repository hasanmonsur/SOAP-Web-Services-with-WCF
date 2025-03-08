// See https://aka.ms/new-console-template for more information
using System.ServiceModel;
using WcfClient;

Console.WriteLine("Hello, World!");

    var binding = new BasicHttpBinding();
    var endpoint = new EndpointAddress("http://localhost:8800/Service");

    using (var channel = new ChannelFactory<IService>(binding, endpoint))
    {
        var client = channel.CreateChannel();

        // Test the service
        string message = client.GetMessage("John");
        Console.WriteLine(message);

        UserData user = client.GetUserData("John");
        Console.WriteLine($"User: {user.Name}, Email: {user.Email}");


    }
