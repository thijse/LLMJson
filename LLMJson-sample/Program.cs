namespace LLMJson_sample
{
    internal class Program
    {


        static async Task Main(string[] args)
        {
            var deserializeSample = new DeserializeSample();
            deserializeSample.ValueDeserializer();

            /*
            var serializeSample = new SerializeSample();
            serializeSample.ValueSerializer();
            serializeSample.DescriptionSerializer();
            serializeSample.ValueAndDescriptionSerializer();
            serializeSample.CustomSerializer();
            serializeSample.CustomPropertySerializer();
            */

            //var jsonSendReceive = new JsonSendReceive();
            //await jsonSendReceive.SendJson();
            //jsonSendReceive.TestParser();
            //jsonSendReceive.TestWriter();
            //jsonSendReceive.TestProperty();

            //jsonSendReceive.ObjectToJsonDescription();
            Console.WriteLine("Hello, World!");
        }
    }
}