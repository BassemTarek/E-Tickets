using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }
        //Configure sessions and shopping cart as a service
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId
            };
        }

        //Add Item
        public void AddItemToCart(Movie movie)
        {
            // movie already in the cart??
            var shoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            //If no, add it to cart items
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            //If yes, add 1 to amount
            else
            {
                shoppingCartItem.Amount++;
            }

            _context.SaveChanges();
        }
        //Remove Item
        public void RemoveItemFromCart(Movie movie)
        {
            //Check if item exists in the database
            var shoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            //If exists
            if (shoppingCartItem != null)
            {
                //if amount greater than one, decrease the amount by one
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                //if the amount equal to 1, remove the item from the database
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
        }

        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Movie.Price * n.Amount).Sum();
            return total;
        }
        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
