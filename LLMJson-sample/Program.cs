using static LLMJson_sample.DeserializeSample;
using static System.Net.Mime.MediaTypeNames;

namespace LLMJson_sample
{
    internal class Program
    {


        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1) Serialize   to   JSON examples");
                Console.WriteLine("2) Deserialize from JSON examples");
                Console.WriteLine("3) Serialize, send to LLM, deserialize");
                Console.WriteLine("4) Exit");
                Console.Write("\r\nSelect an option: ");


                switch (Console.ReadLine())
                {
                    case "1":
                        var serializeSample = new SerializeSample();
                        serializeSample.ValueSerializer();
                        Console.WriteLine("\n\nPress any key to continue..."); Console.ReadKey();
                        serializeSample.DescriptionSerializer();
                        Console.WriteLine("\n\nPress any key to continue..."); Console.ReadKey();
                        serializeSample.ValueAndDescriptionSerializer();
                        Console.WriteLine("\n\nPress any key to continue..."); Console.ReadKey();
                        serializeSample.CustomSerializer();
                        Console.WriteLine("\n\nPress any key to continue..."); Console.ReadKey();
                        serializeSample.CustomPropertyDisabledSerializer();
                        Console.WriteLine("\n\nPress any key to continue..."); Console.ReadKey();
                        break;
                    case "2":
                        var deserializeSample = new DeserializeSample();
                        deserializeSample.ValueDeserializer();
                        Console.WriteLine("\n\nPress any key to continue..."); Console.ReadKey();
                        deserializeSample.MalformattedFieldsDeserializer();
                        Console.WriteLine("\n\nPress any key to continue..."); Console.ReadKey();
                        deserializeSample.CustomPropsDeserializer();
                        Console.WriteLine("\n\nPress any key to continue..."); Console.ReadKey();
                        break;
                    case "3":
                        var serializeDeserializeSample = new SerializeDeserializeSample();
                        serializeDeserializeSample      .SerializeAndDeserialize();
                        Console.WriteLine("\n\nPress any key to continue..."); Console.ReadKey();
                        await serializeDeserializeSample.SerializeUpdateDeserialize();
                        Console.WriteLine("\n\nPress any key to continue..."); Console.ReadKey();
                        await serializeDeserializeSample.SerializeFillDeserialize();
                        Console.WriteLine("\n\nPress any key to continue..."); Console.ReadKey();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }









            }
            Console.WriteLine("Tests done!");
        }
    }
}