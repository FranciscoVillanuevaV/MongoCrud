using System.Text.Json;

namespace SampleMongoCrud.Services
{
    public class JsonCasting
    {
        public static Output ChangeType<Input, Output>(Input input)
        {
            string Json = JsonSerializer.Serialize(input);
            Output output = JsonSerializer.Deserialize<Output>(Json);
            return output;
        }
    }
}