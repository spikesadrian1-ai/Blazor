//using ApplicationLayer.Entities.FilingInformation;
using ApplicationLayer.Entities;
using ApplicationLayer.Entities.FilingInformation;
using ApplicationLayer.Entities.Filings;
using ApplicationLayer.Interfaces.ApiQueries._Generics;
using ApplicationLayer.Services.DropdownLists;
using ApplicationLayer.Services.Owners.Filings;
using Microsoft.AspNetCore.Components;
using BlazorWebApp.Components.Pages.OG_Forms.Properties.Tracts.TractTabs;
using System;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.Filing
{
    public class FilingBase : ComponentBase
    {
        #region INJECTIONS 
        [Inject]
        public IPopulateForm<Recordings> _populateForm { get; set; }
        [Inject]
        public FilingInformationServices _filingInformationServices { get; set; }
        private readonly IDropdownQueryServiceTasks _dropdownQueryServiceTasks;
        #endregion INJECTIONS 

        #region CONSTRUCTORS

        public FilingBase() { }

        public FilingBase
        (
            FilingInformationServices filingInformationServices
           , IDropdownQueryServiceTasks dropdownQueryServiceTasks
        )
        {
            _filingInformationServices = filingInformationServices;
            _dropdownQueryServiceTasks = dropdownQueryServiceTasks;
        }

        #endregion CONSTRUCTORS

        public Recordings? filings { get; set; } = new Recordings();
        [Parameter] public bool InitialState { get; set; } //= string.Empty;
        [Parameter] public string? IdFromListView { get; set; } = string.Empty;//"OK01-NOR-001";
        [Parameter] public string? EntityID { get; set; } = string.Empty;
        [Parameter] public string? FilingID { get; set; } = string.Empty;
        //public async Task OnInitializedAsync()
        //{
        //    if (string.IsNullOrWhiteSpace(filings.Id.ToString()))
        //    {
        //        await PopulateDropdown();
        //    }
        //    else 
        //    {
        //        await PopulateForm();
        //    }
        //}

        public async Task<Recordings> PopulateFormBasedOnEntityID(Recordings getList)
        {
            filings = await _populateForm
                .PopulateFormBasedOnEntityID(t: getList);
            return await Task
                .FromResult(filings);
        }


        #region USE CASES

        #region CREATE
        public async Task<Recordings> SaveOrUpdateNewFiling(Recordings filings) 
        {
            //return filings.
            throw new NotImplementedException();
        }
        #endregion CREATE

        #region UPDATE        
        public async Task<FilingInformation> UpdateExistingFilingAsync(FilingInformation filings)
           => await _filingInformationServices.SaveNewFilingAsync(filings);
        #endregion UPDATE

        #region DELETE

        #endregion DELETE

        #endregion USE CASES

        #region POPULATE FORM
        private async Task<FilingInformation> PopulateForm()
        {
            throw new NotImplementedException();
        }

        #endregion POPULATE FORM
    }
}
