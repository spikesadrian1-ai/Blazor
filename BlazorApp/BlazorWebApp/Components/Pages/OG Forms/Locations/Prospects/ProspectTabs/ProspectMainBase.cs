using ApplicationLayer.Entities.Locations.Counties;
using ApplicationLayer.Entities.Locations.States;
using ApplicationLayer.Entities.Locations.Prospects;
using ApplicationLayer.Interfaces.UnitOfWork;
using ApplicationLayer.Services.DropdownLists;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Entities.Locations.Regions;
using ApplicationLayer.Entities.Locations.Districts;
using ApplicationLayer.Services.Locations.CountyServices;

namespace BlazorWebApp.Components.Pages.OG_Forms.Locations.Prospects.ProspectTabs
{
    public partial class ProspectMainBase : ComponentBase
    {
        /*  CALLS METHODS FROM APPLICATION LAYER BUT UTILIZING THE APPLICATION DTO 
         *  INSTEAD OF THE DOMAIN LAYER TO AVOID ERROR WITH JSON CONVERSION ISSUES         
         * 
         */

        #region INJECTIONS
        [Inject]
        public IDropdownQueryServiceTasks _dropdownQueryServiceTasks { get; set; }
        [Inject]
        public ICountyQueryServiceTasks CountyQueryServiceTasks { get; set; }

        private readonly IUnitOfWork _unitOfWork;
        #endregion INJECTIONS

        #region OBJECTS

        public ProspectMainForm prospects = new ProspectMainForm();

        public CountyMasterMainForm counties = new CountyMasterMainForm();
        public StateMainForm states = new StateMainForm();
        public RegionMasterMainForm regions = new RegionMasterMainForm();
        public DistrictMasterModel districts = new DistrictMasterModel();
        //public OwnerMainAddresses operators = new OwnerMainAddresses();
        //public OwnerMainAddresses lessee = new OwnerMainAddresses();
        //public OwnerMainAddresses landman = new OwnerMainAddresses();

        ////states appear but not in correct dropdown

        #endregion OBJECTS

        public ProspectMainBase(
            //ProspectMainForm prospectMain
            )
        {
            //prospectMain.ProspectId = id;
            //prospectMain.ListOfStates = ListOfStates;
            //prospects.ListOfStates = ListOfStates.ToList();
            //ListOfStates = prospects.ListOfStates.ToList();
        }

        #region ONINITIAIZED
        protected override /*protected*/ async Task OnInitializedAsync()
        {
            await PopulateDropdowns();

            //TODO: IF USING A ASYNC TASK USE TASK.RUN(...) FOR CPU-BOUND WORK ON A BACKGROUND THREAD
            //Task.Run(await RetreiveAllCountiesAsync()
            //    .ContinueWith(RetrieveAllStatesAsync()
            //    .ContinueWith(await RetrieveAllDistrictsAsync().ConfigureAwait(true), continuationAction: TaskContinuationOptions.None)));

        }
        #endregion ONINITIAIZED


        //string Landman = "";

        #region
        //void SelectSubTypesChanged(ChangeEventArgs e)
        public void SelectSubTypesChanged(ChangeEventArgs e)
        {
            //prospects.LandmanId = "";

            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                //Landman = (string)e.Value;
                prospects.Status = (string)e.Value;

            }

            //return prospects.LandmanId;

            //contractMain.SubType = SelectedSubType;
        }

        //public Task<ProspectMainForm> Test()
        //public string Test()
        //{
        //    prospects.Status = "";

        //    if (!string.IsNullOrEmpty(e.Value.ToString()))
        //    {
        //        //Landman = (string)e.Value;
        //        prospects.Status = (string)e.Value;

        //    }

        //    return prospects.Status;

        //    //contractMain.SubType = SelectedSubType;
        //}

        private readonly ProspectMainForm Status;
        public async Task<ProspectMainForm> Test(ProspectMainForm prospects)
        {            
            if (!string.IsNullOrEmpty(prospects.Status))
            {
                string Status = prospects.Status;//.ToString();
            }

            return Status;
        }
        #endregion

        #region DROPDOWNS
        public async Task<List<string>> PopulateDropdowns()
        {

            var getAllcountyTasks = RetreiveAllCountiesAsync();   //_dropdownQueryServiceTasks.RetrieveAllCountiesAsync(ListAllCounties);
            var getAllstateTasks = RetrieveAllStatesAsync(); //_dropdownQueryServiceTasks.RetrieveAllStatesAsync(/*ListOfStates*/);
            var getAllDistrictTasks = RetrieveAllDistrictsAsync();
            var getAllregionTasks = RetrieveAllRegionsAsync();
            //var getAlloperatorTasks = RetreiveAllCountiesAsync();
            //var getAlllesseeTasks = RetreiveAllCountiesAsync();
            //var getAllLandmenTasks = RetreiveAllCountiesAsync();

            await Task.WhenAll(getAllcountyTasks, getAllstateTasks, getAllDistrictTasks, getAllregionTasks); //await Task.WhenAll(RetreiveAllCountiesAsync(), RetrieveAllStatesAsync());

            var report = $"County: {getAllcountyTasks.Result}, States: {getAllstateTasks.Result}";

            return new List<string> { report };
        }

        public List<StateMainForm> ListOfStates { get; set; } = new List<StateMainForm>();
        public async Task<List<StateMainForm>> RetrieveAllStatesAsync()
        {
            return ListOfStates = await _dropdownQueryServiceTasks
            //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST 
            .RetrieveAllStatesAsync(/*ListOfStates*/);
        }

        public List<CountyMasterMainForm> ListAllCounties { get; set; } = new List<CountyMasterMainForm>();
        public async Task</*IReadOnly*/List<CountyMasterMainForm>> RetreiveAllCountiesAsync()
        {
            //try {
            return ListAllCounties = await CountyQueryServiceTasks
            //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST 
            .RetrieveAllCountiesAsync((List<CountyMasterMainForm>)ListAllCounties);

        }

        public List<string> ListOfRegions { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveAllRegionsAsync()
        {
            return ListOfRegions = await _dropdownQueryServiceTasks
            //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST 
            .RetrieveAllRegionsAsync(ListOfRegions);

        }

        public List<string> ListOfDistricts = new List<string>();
        public async Task<List<string>> RetrieveAllDistrictsAsync()
        {
            return ListOfDistricts = await _dropdownQueryServiceTasks
            //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST 
            .RetrieveAllDistrictsAsync(ListOfDistricts);
        }
        #endregion DROPDOWNS

    }
}
