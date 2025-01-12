namespace WebApp.Models
{
    public class ModelResult
    {
        public string RestaurantName { get; set; } = string.Empty;
        public double Utility { get; set; }

        public ModelResult()
        {
            
        }
        
        public ModelResult(string restaurant, double utility)
        {
            RestaurantName = restaurant;
            Utility = utility;
        }
    }
}