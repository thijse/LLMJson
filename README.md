# LLMJson

JSON, but for Large Language Model interaction

LLMS such as OpenAI GPT are  notoriously bad in consistently generating well-formed datastructures, even as simple as JSON. LLMJson aims to make your life a lot easier

### What LLMJson cannot do:
This library is based on the TinyJson library, and while great, it comes with the same shortcomings and adds a few:

- Limited to parsing <2GB JSON files add most
- It will not parse abstract classes or interfaces 
- It's slow: it might very well be the *slowest JSON library in the world*.

### What LLMJson can do:
However, all that is less relevant for the intended use of this library, namely sharing data structures with an LLM in an understandable manner. And interpreting data structures returned by the LLM.
- LLMJson can serialize its values, but it can also serialize field descriptions in a way that LLMs understand
@ example
- It is robust against leading and trailing text, as LLMs tend to add introductions and explanations
- It is robust against missing fields and additional fields and comments
- It is (often) robust against errors such as missing quotes,   missing escape characters,  missing commas and  missing closing brackets and more
- It is very good at interpreting values that are technically incorrect @examples

And even if a field cannot be parsed, the parser will ignore it and continue!

# Example
Serializes a persona including descriptions of the fields and sends it to OpenAI to update the persona. Next, it receives the result and extracts and deserializes the Json. finally the received object is shown as JSON to confirm its correctness.

```json
 ** Prompt:
Ideate and update at least two elements of this persona, make sure all aspects of the persona are consistent.

 Update the data in the described format below. Comment are added for your understanding, remove in updated response. provide a RFC8259 compliant JSON response, following this format without deviation, but values may be changed, lists and dictionaries may change in length. Add additional explanation of the changes made after the json structure.

 {"Name":"Nigel Thornberry" \\ Firstname only. Is of type string
,"Age":47 \\ Age of person. Range between 0 and 100. Is of type 32-bit integer
,"Sex":"Male" \\ Is of type enum. Possible values are Male,Female,Other
,"Iq":130 \\ Intelligence coefficient. Is of type 32-bit integer
,"Birthday":"09/16/2023 00:00:00" \\ Is of type date and time in format dddd, dd MMMM yyyy HH:mm:ss
,"Traits":["sneaky"
,"funny"
] \\ A list of character traits, e.g. ["optimistic", "smart"]. Is of type List, items are of type string
,"Stats":{"bravery":80
,"nimbleness":70
} \\ A dictionary of character statistics with a percentage between 0 and 100, e.g. {{"bravery", 100}, { "quick thinking", 100}}. Is of type Dictionary . The key is of type string, the value of 32-bit integer
}


 ** LLM response:
{"Name":"Nigel Thornberry"
,"Age":47
,"Sex":"Male"
,"Iq":130
,"Birthday":"09/16/1974 00:00:00"
,"Traits":["adventurous"
,"charismatic"
,"outgoing"
]
,"Stats":{"bravery":80
,"nimbleness":70
,"empathy":60
}
}

In this updated persona, we have added two new elements and modified one existing element.
Firstly, we have updated the "Birthday" to reflect a more accurate date of birth for the persona, changing it from 2023 to 1974 to ensure the age of 47 makes sense.
Secondly, we have added two new traits to the persona; "adventurous" and "charismatic", which align with the existing trait of "outgoing".
Finally, we have added a new statistic to the existing "Stats" dictionary, which is "empathy" with a value of 60 out of 100. This helps to provide a more well-rounded understanding of the persona's character attributes.

 ** LLM result re-serialized
{"Name":"Nigel Thornberry","Age":47,"Sex":"Male","Iq":130,"Birthday":"09/16/1974 00:00:00","Traits":["adventurous","charismatic","outgoing"],"Stats":{"bravery":80,"nimbleness":70,"empathy":60}}
```

# Serializing JSON

```cs
    public class Person
    {
        [DescriptionAttribute("Firstname only")]
        public string                  Name     { get; set; }
        [DescriptionAttribute("Age of person. Range between 0 and 100")]
        public int                     Age      { get; set; }
        public Sex                     Sex      { get; set; }
        public JsonProp<int>           Iq       { get; set; }
        public DateTime                Birthday { get; set; }
        [DescriptionAttribute("A list of character traits, e.g. [\"optimistic\", \"smart\"]")]
        public List<string>            Traits   { get; set; }
        [DescriptionAttribute("A dictionary of character statistics with a percentage between 0 and 100, e.g. {{\"bravery\", 100}, { \"quick thinking\", 100}}")]
        public Dictionary<string, int> Stats    { get; set; }
    }
```

```json
{
	"Name": "Nigel Thornberry",
	"Age": 47,
	"Sex": "Male",
	"Iq": 130,
	"Birthday": "09/16/2023 00:00:00",
	"Traits": ["sneaky", "funny"],
	"Stats": {
		"bravery": 80,
		"nimbleness": 70
	}
}```

When outputted as descriptions it will give back the following

```json
{
	"Name": "Firstname only. Is of type string",
	"Age": "Age of person. Range between 0 and 100. Is of type 32-bit integer",
	"Sex": "Is of type enum. Possible values are Male,Female,Other",
	"Iq": "Intelligence coefficient. Is of type 32-bit integer",
	"Birthday": "date and time in format dddd, dd MMMM yyyy HH:mm:ss",
	"Traits": [] \\ A list of character traits, e.g.["optimistic", "smart"].Is of type List, items are of type string,
	"Stats": {}  \\	A dictionary of character statistics with a percentage between 0 and 100, e.g. {{"bravery",100}, {"quick thinking",100}}.Is of type Dictionary. The key is of type string, the value of is type string
}
```

Note that this is not a valid schema or even valid JSON, but a pseudo-JSON that LLMs understand much better. Types are automatically extracted from the class definition, and comments are added through attributes.

Now, if you want to share with an LLM both values and descriptions, you will get

```json
{
	"Name": "Nigel Thornberry      "  \\ Firstname only.Is of type string,
	"Age": 47                         \\ Age of person.Range between 0 and 100. Is of type 32 - bit integer,
	"Sex": "Male"                     \\ Is of type enum.Possible values are Male, Female, Other,
	"Iq": 130                         \\ Intelligence coefficient.Is of type 32 - bit integer,
	"Birthday": "09/16/2023 00:00:00" \\ Is of type date and time in format dddd, dd MMMM yyyy HH: mm: ss,
	"Traits": ["sneaky", "funny"]     \\ A list of character traits, e.g.["optimistic", "smart"].Is of type List, items are of type string,
	"Stats": {
	  "bravery"   : 80,
	  "nimbleness": 70
	} \\	A dictionary of character statistics with a percentage between 0 and 100, e.g. {{"bravery",100}, {"quick thinking",100}}.Is of type Dictionary. The key is of type string, the value of is type string
}
```

Again invalid JSON, but structured such that LLMs understand it very well.

Based on the different formats, different prompts are created:
@name all 3


# Deserializing JSON

### JsomProp

### Extensive field parsing