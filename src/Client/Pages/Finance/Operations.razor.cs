using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace PAD.Client.Finance
{
    public partial class Operations
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Http.GetAsync("finance/weather");
        }

        public int count { get { return AllOrderDetails.Count(); } }

        public IEnumerable<ExpenseViewModel> AllOrderDetails = new ExpenseViewModel[]
        {
            new() {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                Description = "Test expense",
                Amount = 1000,
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Products"
            },
            new() {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                Description = "Test expense",
                Amount = 1000,
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Products"
            },
            new() {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                Description = "Test expense",
                Amount = 1000,
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Products"
            },
            new() {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                Description = "Test expense",
                Amount = 1000,
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Products"
            },
            new() {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                Description = "Test expense",
                Amount = 1000,
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Products"
            },
            new() {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                Description = "Test expense",
                Amount = 1000,
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Products"
            },
            new() {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                Description = "Test expense",
                Amount = 1000,
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Products"
            },
            new() {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                Description = "Test expense",
                Amount = 1000,
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Products"
            },
            new() {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                Description = "Test expense",
                Amount = 1000,
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Products"
            },
            new() {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                Description = "Test expense",
                Amount = 1000,
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Products"
            }
        };

        public IEnumerable<ExpenseViewModel> OrderDetails;

        public IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 25 };

        protected override void OnInitialized()
        {
            OrderDetails = AllOrderDetails.Take(5).ToList();
        }
    }

    public class ExpenseViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}