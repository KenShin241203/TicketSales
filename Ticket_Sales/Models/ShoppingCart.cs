namespace Ticket_Sales.Models
{
    public class ShoppingCart
    {
        public Event Event {  get; set; }
        public int EventId {  get; set; }
        public string EventName {  get; set; }
        public Types types { get; set; }
        public int TypeId {  get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => (decimal)Price*Quantity;
        public List<CartItem> Items{ get; set; } = new List<CartItem>();
        public IEnumerable<Types> Types { get; set; }

        public void AddEvent(CartItem item, IEnumerable<Types> types)
        {
            Types = types;
            EventName = item.EventName;
            Items.Add(item);
        }

    }
}
