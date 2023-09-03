namespace LLMJson_sample
{
    internal class Program
    {


        static async Task Main(string[] args)
        {

            var jsonSendReceive = new JsonSendReceive();
            //await jsonSendReceive.SendJson();
            //jsonSendReceive.TestParser();
            jsonSendReceive.TestWriter();
            //jsonSendReceive.TestProperty();

            //jsonSendReceive.ObjectToJsonDescription();
            Console.WriteLine("Hello, World!");
        }
    }
}