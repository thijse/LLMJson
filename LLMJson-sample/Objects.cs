using LLMJson;

namespace LLMJson_sample
{
    public enum Sex { Male, Female, Other}

    public class Person
    {
        [DescriptionAttribute("Firstname and last name")]
        public string               Name                                  { get; set; } = "Jan B Fuhrmann";

        [DescriptionAttribute("Age of person. Range between 0 and 100")]
        public int                  Age   = 47;
        public Sex                  Sex                                   { get; set; } = Sex.Male;
        public JsonProp<int>        Iq                                    { get; set; } = new(130,"Intelligence coefficient");
        public DateTime             Birthday                              { get; set; } = DateTime.Today;

        [DescriptionAttribute("A list of character traits, e.g. [\"optimistic\", \"smart\"]")]
        public List<string>         Traits                                { get; set; } = new List<string>() { "optimistic", "smart" };

        [DescriptionAttribute("A dictionary of character statistics, e.g. {{\"bravery\", 125}, { \"nimbleness\", 70}}")]
        public Dictionary<string,int> Stats                               { get; set; } = new Dictionary<string, int> { {"bravery", 125}, { "nimbleness", 70} };
    }

    public class PropertyTest
    {
        public int           IntNumber   { get; set; } = 20;
        public JsonProp<int> IntProperty { get; set; } = new(130, "Intelligence coefficient");

    }

}
