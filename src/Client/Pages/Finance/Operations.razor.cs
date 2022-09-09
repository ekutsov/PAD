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
                Dictionary<string, string> queryParams = tableDTO.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(tableDTO, null));

                Console.Log(queryParams);
                return await HttpService.GetCollectionAsync<ExpenseViewModel>("finance/expenses", queryParams);
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
            StartDate = dateRange.Start.Value.ToString("MM/dd/yyyy");
            EndDate = dateRange.End.Value.ToString("MM/dd/yyyy");
            Page = state.Page.ToString();
            PageSize = state.PageSize.ToString();
            SortLabel = state.SortLabel ?? string.Empty;
            SortDirection = ((int)state.SortDirection).ToString();
        }
        public string SearchString { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Page { get; set; }

        public string PageSize { get; set; }

        public string SortLabel { get; set; }

        public string SortDirection { get; set; }
    }
}