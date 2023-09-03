
namespace LLMJson
{
    public class JsonProp<T> 
    {
        private T _value;

        public enum UpdateStates
        {
            Unchanged,
            InvalidUpdate,
            Updated
        };

        public string Description       { get; set; } = "";
        public UpdateStates UpdateState { get; set; } = UpdateStates.Unchanged;
        public bool Visible             { get; set; } = true;
        public bool Immutable           { get; set; } = false;

        public T Value {
            get { return _value;  }
            set { _value = value; }
        }

        public JsonProp(T value)
        {
            _value = value;
        }

        public override string ToString() { try { return Value?.ToString() ?? ""; }catch {return "";} }

        public static implicit operator T(JsonProp<T> instance)
        {
            //if (instance._value == null) { return default(T)! ; }
            return instance._value;
        }
    }
}
