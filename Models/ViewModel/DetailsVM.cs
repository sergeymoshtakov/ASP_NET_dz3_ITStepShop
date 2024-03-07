namespace ITStepShop.Models.ViewModel
{
    public class DetailsVM
    {
        public DetailsVM() 
        {
            Product = new Product();
            Category = new Category();
        }

        public Product Product { get; set; }
        public Category Category { get; set; }
        public bool ExistsInCart { get; set; }
    }
}
