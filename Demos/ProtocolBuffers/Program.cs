using Google.Protobuf;

namespace ProtocolBuffers
{
    internal class Program
    {
        static void Main()
        {
            var person = new Person
            {
                Name = "John Doe",
                Age = 30
            };

            var json = JsonFormatter.Default.Format(person);
            Console.WriteLine("Serialized JSON:");
            Console.WriteLine(json);

            var deserializedPerson = JsonParser.Default.Parse<Person>(json);
            Console.WriteLine("\nDeserialized Person:");
            Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
        }
    }
}
