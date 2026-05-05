using ApplicationLayer.Entities;
using ApplicationLayer.Interfaces.ApiQueries._Generics;
using ApplicationLayer.Services.Assets.Tracts.TractStatus;
using ApplicationLayer.Services.DropdownLists;
using ApplicationLayer.Services.Locations.CountyServices;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.Properties.Tracts.TractTabs.SubTabInternals.TractMainPage
{
    public class TractMainPageBase : ComponentBase
    {
        #region DI

        [Inject]
        public IStatusOfTracts _statusOfTracts { get; set; }
        [Inject]
        public IDropdownQueryServiceTasks _dropdownQueryServiceTasks { get; set; }
        [Inject]
        public ICountyQueryServiceTasks _countyQueryServiceTasks { get; set; }
        [Inject]
        public IPopulateForm<TractMainForm> _populateForm { get; set; }

        //[Inject] public ITractRepository _leaseMainServices { get; set; }

        #endregion DI

        #region CONSTRUCTORS

        public TractMainPageBase()
        {

            //_LeaseModel.Lease_ID = string.Empty;
            //_LeaseModel.Lease_ID = LeaseID.ToString();
        }

        public TractMainPageBase
        (
                TractMainForm _leaseMainForm2
        )
        {
            this.TractModel = _leaseMainForm2;
        }

        #endregion CONSTRUCTORS

        /*[Parameter]*/// public bool initialState { get; set; }
        public bool leaseIdFromListOfLeases = false;

        public TractMainForm? TractModel { get; set; } = new TractMainForm();
        [Parameter] public bool InitialState { get; set; } //= string.Empty;
        [Parameter] public bool LeaseRedirectTractIdNotNullRememberToMakeNullAndReplace { get; set; } //= string.Empty;
        [Parameter] public string? TractId { get; set; } = string.Empty;
        [Parameter] public string? IdFromListView { get; set; } = string.Empty;//"OK01-NOR-001";
        [Parameter] public string? EntityID { get; set; } = string.Empty;

        public bool dropdonboxNameSelected;



        public void Add()
        {
            string active = TractModel.Active;
        }

        #region DROPDOWNS

        //public async Task<List<string>> PopulateDropdowns()
        //{
        //    //var getAlltractStatusesTasks = RetrieveAllTractStatusAsync();
        //    var getAllcountyTasks = RetreiveAllCountiesAsync();
        //    var getAllstateTasks = RetrieveAllStatesAsync();
        //    var getAllDistrictTasks = RetrieveAllDistrictsAsync();
        //    var getAllregionTasks = RetrieveAllRegionsAsync();

        //    await Task.WhenAll(/* getAlltractStatusesTasks, */ getAllcountyTasks, getAllstateTasks, getAllDistrictTasks, getAllregionTasks); await Task.WhenAll(RetreiveAllCountiesAsync(), RetrieveAllStatesAsync());

        //    var report = //$"TractStatuses: {getAlltractStatusesTasks.Result}," +
        //                 $"County: {getAllcountyTasks.Result}," +
        //                 $"States: {getAllstateTasks.Result}," +
        //                 $"Districts: {getAllDistrictTasks.Result}," +
        //                 $"Regions: {getAllregionTasks.Result},";

        //    return new List<string> { report };
        //}

        //public List<DropDownLists> StatusOfTract = new List<DropDownLists>();
        //public async Task<List<DropDownLists>> RetrieveAllTractStatusAsync()
        //// => await _statusOfTracts.RetrieveAllTractStatusAsync();
        //{
        //    return StatusOfTract = await _statusOfTracts
        //    .RetrieveAllTractStatusAsync();
        //}

        //public List<StateMainForm> ListOfStates { get; set; } = new List<StateMainForm>();
        //public async Task<List<StateMainForm>> RetrieveAllStatesAsync()
        //{
        //    return ListOfStates = await _dropdownQueryServiceTasks
        //    .RetrieveAllStatesAsync();
        //}

        //public List<CountyMasterMainForm> ListAllCounties { get; set; } = new List<CountyMasterMainForm>();
        //public async Task</*IReadOnly*/List<CountyMasterMainForm>> RetreiveAllCountiesAsync()
        //{
        //    //try {
        //    return ListAllCounties = await _countyQueryServiceTasks
        //    //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST
        //    .RetrieveAllCountiesAsync((List<CountyMasterMainForm>)ListAllCounties);

        //}

        //public List<RegionMasterMainForm> ListOfRegions { get; set; } = new List<RegionMasterMainForm>();
        //public async Task<List<RegionMasterMainForm>> RetrieveAllRegionsAsync()
        //{
        //    return ListOfRegions = await _dropdownQueryServiceTasks
        //    //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST
        //    .RetrieveAllRegionsAsync(ListOfRegions);

        //}

        //public List<DistrictMasterModel> ListOfDistricts = new List<DistrictMasterModel>();
        //public async Task<List<DistrictMasterModel>> RetrieveAllDistrictsAsync()
        //{
        //    return ListOfDistricts = await _dropdownQueryServiceTasks
        //    //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST
        //    .RetrieveAllDistrictsAsync(ListOfDistricts);
        //}

        #endregion DROPDOWNS

        #region POPULATE FORM

        public async Task<TractMainForm> PopulateFormBasedOnEntityID(TractMainForm getList)
        {
            TractModel = await _populateForm
                .PopulateFormBasedOnEntityID(t: getList);                 
            return await Task
                .FromResult(TractModel);
        }

        #endregion POPLUATE FORM

        #region SAVE / UPDATE

        public async void SaveButton(TractMainForm TractModel)
        { 
        
        }

        #endregion SAVE / UPDATE


        #region



        #endregion


        #region



        #endregion


        #region



        #endregion

        #region



        #endregion
        #region



        #endregion

    }
}
