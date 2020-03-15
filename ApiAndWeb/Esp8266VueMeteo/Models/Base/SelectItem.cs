namespace Esp8266VueMeteo.Models.Base
{
    public class SelectItem<T>
    {
        public T Id { get; set; }
        public string Text { get; set; }
    }
    public class DefaultSelectItem<T> : SelectItem<T>
    {
        public bool IsDefault { get; set; }
    }

}
