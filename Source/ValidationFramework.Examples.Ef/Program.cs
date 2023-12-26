namespace ValidationFramework.Examples.Ef
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (EntityContext ec = new EntityContext())
            {
                Order order;
                Item item;

                order = new Order();
                order.Price = 99.99m;

                item = new Item();
                item.Name = "Item 1234567890";
                item.Price = -5;
                item.Order = order;
                order.Items.Add(item);

                item = new Item();
                item.Price = 30.999m;

                ec.Items.Add(item);
                ec.Orders.Add(order);

                try
                {
                    ec.SaveChanges();
                }
                catch (ValidationException ex)
                {
                    var validaitonMessages = ex.ValidaitonMessages;

                    foreach (var message in validaitonMessages)
                    {
                        Console.WriteLine(message);
                    }

                    Console.ReadLine();
                }
            }
        }
    }
}
