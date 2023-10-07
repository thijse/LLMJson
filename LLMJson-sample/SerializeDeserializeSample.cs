using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using LLMJson;

namespace LLMJson_sample
{
    public class SerializeDeserializeSample
    {
        private readonly OpenAIService _openAiService;

        public SerializeDeserializeSample()
        {
            // Add your OpenAI key to a file called apikey.txt in the same folder as this project, and set the build action to "Copy if newer"
            string openAIkey = File.Exists("apikey.txt") ? File.ReadAllText("apikey.txt") : ""; // OpenAI key
            _openAiService   = new OpenAIService(new OpenAiOptions() { ApiKey = openAIkey });
        }

        public void SerializeAndDeserialize()
        {
            Console.WriteLine($"\n\n** Serializes a persona to JSON:");
            Console.WriteLine($"   next it desialized the JSON back into an object");
            Console.WriteLine($"   finally the received object is shown as JSON to confirm its correctness. \n\n");

            // Use Nigel persona as filling
            var personNigel     = new Person(); personNigel.SetNigel();
            // Serialize to JSON
            
            var personNigelJson = personNigel.ToJson(OutputModes.Value);
            Console.WriteLine($"** Serialized object with values\n\n{personNigelJson}\n\n");

            // Deserialize 
            Person person = personNigelJson.FromJson<Person>(new Person());
            // Show again as Json to compare
            var    personJson = person.ToJson(OutputModes.Value);
            Console.WriteLine($"** Deserialized & re-serialized object with values\n\n{personJson}\n\n");
        }

        public async Task SerializeUpdateDeserialize()
        {
            Console.WriteLine($"\n\n** Serializes a persona including descriptions of the fields :");
            Console.WriteLine($"   and sends it to OpenAI to update the persona.");
            Console.WriteLine($"   next it receives the result and extracts and deserializes the Json.");
            Console.WriteLine($"   finally the received object is shown as JSON to confirm its correctness.");
            // Use Nigel persona as filling
            var personNigel = new Person(); personNigel.SetNigel();
            // Serialize to JSON, add description for the LLM to make an informed update
            var personNigelJson = personNigel.ToJson(OutputModes.ValueAndDescription);
            // Create custom prompt in combination with prompt on how to process JSON
            var myPrompt = "Ideate and update at least two elements of this persona, make sure all aspects of the persona are consistent. \n\n " + JsonWriter.GetPrompt() + " \n\n " + personNigelJson;

            Console.WriteLine($"\n\n ** Prompt:");
            Console.WriteLine(myPrompt);
            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage> {
                    ChatMessage.FromSystem("You are a helpful assistant."),
                    ChatMessage.FromUser(myPrompt),
                },
                Model       = Models.Gpt_3_5_Turbo_0301,
                Temperature = 1
            });
            if (completionResult.Successful)
            {
                var llmResult = completionResult.Choices.First().Message.Content;
                Console.WriteLine($"\n\n ** LLM response:");
                Console.WriteLine(llmResult);

                // Parse Json returned by LLM 
                var personUpdate = llmResult.FromJson<Person>(new Person());


                // Show again as Json to compare
                var personJson = personUpdate.ToJson(OutputModes.Value);
                Console.WriteLine($"\n\n ** LLM result re-serialized \n\n{personJson}\n\n");

            }
            else
            { Console.WriteLine($"{(completionResult.Error != null ? completionResult.Error.Code : "Unknown Error")}: {completionResult.Error?.Message}"); }
        }

        public async Task SerializeFillDeserialize()
        {
            Console.WriteLine($"\n\n** Serializes the descriptions of the person class:");
            Console.WriteLine($"   and sends it to OpenAI to create a new persona.");
            Console.WriteLine($"   next it receives the result and extracts and deserializes the Json.");
            Console.WriteLine($"   finally the received object is shown as JSON to confirm its correctness.");
            // Use empty person as base
            var personNigel = new Person(); 
            // Serialize to JSON, add description for the LLM to make an informed update
            var personNigelJson = personNigel.ToJson(OutputModes.Description);
            // Create custom prompt in combination with prompt on how to process JSON
            var myPrompt = "Ideate and fill in a new persona, make sure all aspects of the persona are filled in and consistent. \n\n " + JsonWriter.GetPrompt() + " \n\n " + personNigelJson;

            Console.WriteLine($"\n\n ** Prompt:");
            Console.WriteLine(myPrompt);
            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage> {
                    ChatMessage.FromSystem("You are a helpful assistant."),
                    ChatMessage.FromUser(myPrompt),
                },
                Model = Models.Gpt_3_5_Turbo_0301,
                Temperature = 1
            });
            if (completionResult.Successful)
            {
                var llmResult = completionResult.Choices.First().Message.Content;
                Console.WriteLine($"\n\n ** LLM response:");
                Console.WriteLine(llmResult);

                // Parse Json returned by LLM 
                var personUpdate = llmResult.FromJson<Person>(new Person());

                // Show again as Json to compare
                var personJson = personUpdate.ToJson(OutputModes.Value);
                Console.WriteLine($"\n\n ** LLM result re-serialized \n\n{personJson}\n\n");

            }
            else
            { Console.WriteLine($"{(completionResult.Error != null ? completionResult.Error.Code : "Unknown Error")}: {completionResult.Error?.Message}"); }
        }

    }
}   
