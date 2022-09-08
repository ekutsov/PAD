using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using PAD.Client.Services;
using MudBlazor;
using PAD.Client.Components.Dialogs;
using PAD.Client.Extensions;
using System.Reflection;

namespace PAD.Client.Finance
{
    public partial class Operations
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IHttpService HttpService { get; set; }

        [Inject]
        protected IConsoleService Console { get; set; }

        [Inject]
        protected IDialogService DialogService { get; set; }

        private MudTable<ExpenseViewModel> table;

        private string searchString = String.Empty;

        private DateRange _dateRange = new DateRange(DateTime.Now.FirstDayOfMonth(), DateTime.Now.LastDayOfMonth());

        private async Task<TableData<ExpenseViewModel>> ServerReload(TableState state)
        {
            TableStateDTO tableDTO = new(searchString, _dateRange, state);
            try
            {
                Console.Log(tableDTO.ToDictionary());
                return await HttpService.GetCollectionAsync<ExpenseViewModel>("finance/expenses", tableDTO.ToDictionary());
            }
            catch (Exception ex)
            {
                Console.Log(ex.Message);
                return null;
            }
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }

        private void OpenDialog()
        {
            DialogOptions closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };

            DialogService.Show<AddExpenseDialog>("Simple Dialog", closeOnEscapeKey);
        }
    }

    public class ExpenseViewModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }
    }

    public class TableStateDTO
    {
        public TableStateDTO(string searchString, DateRange dateRange, TableState state)
        {
            SearchString = searchString;
            StartDate = dateRange.Start.Value;
            EndDate = dateRange.End.Value;
            Page = state.Page.ToString();
            PageSize = state.PageSize.ToString();
            SortLabel = state.SortLabel;
            SortDirection = ((int)state.SortDirection).ToString();
        }
        public string SearchString { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Page { get; set; }

        public string PageSize { get; set; }

        public string SortLabel { get; set; }

        public string SortDirection { get; set; }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> result = new()
            {
                {"StartDate", this.StartDate.ToString("dd/MM/yyyy")},
                {"EndDate", this.EndDate.ToString("dd/MM/yyyy")},
                {"Page", this.Page},
                {"PageSize", this.PageSize},
                {"SortDirection", this.SortDirection}
            };

            if (!string.IsNullOrWhiteSpace(this.SearchString))
            {
                result.Add("SearchString", this.SearchString);
            }

            if (!string.IsNullOrWhiteSpace(this.SortLabel))
            {
                result.Add("SortLabel", this.SortLabel);
            }

            return result;
        }
    }
}