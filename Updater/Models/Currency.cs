namespace Updater.Models
{
    internal class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Rate { get; set; }
    }
}
