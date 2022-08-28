using System.Linq.Dynamic.Core;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace PAD.Client.Finance
{
    public partial class Operations
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        public int count { get { return AllOrderDetails.Count(); }}

        public IEnumerable<OrderDetail> AllOrderDetails = new OrderDetail[]
        {
            new() {
                OrderID = Guid.NewGuid().ToString(),
                ProductID = Guid.NewGuid().ToString(),
                UnitPrice = 1500,
                Quantity = 6,
                Discount = 0.20
            },
            new() {
                OrderID = Guid.NewGuid().ToString(),
                ProductID = Guid.NewGuid().ToString(),
                UnitPrice = 2500,
                Quantity = 8,
                Discount = 0.40
            },
            new() {
                OrderID = Guid.NewGuid().ToString(),
                ProductID = Guid.NewGuid().ToString(),
                UnitPrice = 3000,
                Quantity = 3,
                Discount = 0.15
            },
            new() {
                OrderID = Guid.NewGuid().ToString(),
                ProductID = Guid.NewGuid().ToString(),
                UnitPrice = 400,
                Quantity = 20,
                Discount = 0.60
            },
            new() {
                OrderID = Guid.NewGuid().ToString(),
                ProductID = Guid.NewGuid().ToString(),
                UnitPrice = 800,
                Quantity = 10,
                Discount = 0.30
            },
            new() {
                OrderID = Guid.NewGuid().ToString(),
                ProductID = Guid.NewGuid().ToString(),
                UnitPrice = 1500,
                Quantity = 6,
                Discount = 0.20
            },
            new() {
                OrderID = Guid.NewGuid().ToString(),
                ProductID = Guid.NewGuid().ToString(),
                UnitPrice = 2500,
                Quantity = 8,
                Discount = 0.40
            },
            new() {
                OrderID = Guid.NewGuid().ToString(),
                ProductID = Guid.NewGuid().ToString(),
                UnitPrice = 3000,
                Quantity = 3,
                Discount = 0.15
            },
            new() {
                OrderID = Guid.NewGuid().ToString(),
                ProductID = Guid.NewGuid().ToString(),
                UnitPrice = 400,
                Quantity = 20,
                Discount = 0.60
            }
        };

        public IEnumerable<OrderDetail> OrderDetails;

        public IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 25 };

        protected override void OnInitialized()
        {
            OrderDetails = AllOrderDetails.Take(5).ToList();
        }

        public void LoadData(LoadDataArgs args)
        {
            var query = AllOrderDetails.AsQueryable();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                query = query.Where(args.Filter);
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                query = query.OrderBy(args.OrderBy);
            }

            OrderDetails = query.Skip(args.Skip.Value).Take(args.Top.Value).ToList();
        }
    }

    public class OrderDetail
    {
        public string OrderID { get; set; }

        public string ProductID { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public double Discount { get; set; }
    }
}