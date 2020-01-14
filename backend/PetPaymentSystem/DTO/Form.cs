namespace PetPaymentSystem.DTO
{
    public class Form
    {
        public string Type { get; set; }
        public string Key { get; set; }
        public string CompositeKey => Type + Key;

        public string Html { get; set; }
    }
}
