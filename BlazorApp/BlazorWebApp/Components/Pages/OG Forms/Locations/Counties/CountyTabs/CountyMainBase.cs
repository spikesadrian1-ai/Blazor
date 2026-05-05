using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using ApplicationLayer.Entities.Locations.Counties;
using ApplicationLayer.Entities.Locations.States;
using ApplicationLayer.Services.Locations.CountyServices;
using ApplicationLayer.Interfaces.ApiCommands.Locations.Counties;

namespace BlazorWebApp.Components.Pages.OG_Forms.Locations.Counties.CountyTabs
{
    public partial class CountyMainBase : ComponentBase
    {
        /*  CALLS METHODS FROM APPLICATION LAYER BUT UTILIZING THE APPLICATION DTO 
         *  INSTEAD OF THE DOMAIN LAYER TO AVOID ERROR WITH JSON CONVERSION ISSUES         
         * 
         */

        #region INJECTIONS
        [Inject]
        public ICountyQueryServiceTasks CountyQueryServiceTasks { get; set; }
        [Inject]
        public ICountyApiCommands CountyApiCommands { get; set; }

        private readonly ApplicationLayer.Interfaces.UnitOfWork.IUnitOfWork _unitOfWork;
        #endregion INJECTIONS

        #region APP DOMAIN OBJECTS

        public CountyMasterMainForm CountySearch { get; set; } = new CountyMasterMainForm(); //= null;
        public CountyMasterMainForm CountyDropdown { get; set; } = new CountyMasterMainForm();

        public List<StateMainForm> States { get; set; } = new List<StateMainForm>();

        public string State;

        #endregion APP DOMAIN OBJECTS

        #region STATE MANAGEMENT OBJECTS
        public bool NullChecksInitialStateOfTheApplicationHidesAutoPopulatedData()
        {

            if ((CountySearch.CountyName is null) &&
            (CountyDropdown.CountyName != string.Empty) && (CountyDropdown.CountyName is null) &&
            (CountyDropdown.StateId is null)) { }

            return true;
        }

        public bool CountySearchInitiallyUsedFirstWithCountyDropdownHavingAnEmptyString()
        {

            if ((CountySearch.CountyName is null) &&
            (CountyDropdown.CountyName != string.Empty) && (CountyDropdown.CountyName is null) &&
            (CountyDropdown.StateId is null)) { }

            return true;
        }

        public bool CountySearchInitiallyUsedFirstAndRetriedWithCountyDropdownNotHavingAnEmptyString()
        {

            if ((CountySearch.CountyName is null) &&
            (CountyDropdown.CountyName != string.Empty) && (CountyDropdown.CountyName is null) &&
            (CountyDropdown.StateId is null)) { }

            return true;
        }

        public bool InitialState_CoumtySearchNotEmptyOrNull()
        {

            if ((CountySearch.CountyName is null) &&
            (CountyDropdown.CountyName != string.Empty) && (CountyDropdown.CountyName is null) &&
            (CountyDropdown.StateId is null)) { }

            return true;
        }

        #endregion STATE MANAGEMENT OBJECTS

        #region FRONT-END ERROR STRINGS

        // TODO: CHANGE TO ERROR HANDLING. COULD BE A BOOLEAN.
        public string ErrorString;
        public object AddNewCountyErrorString { get; private set; }   //= "County and/or State cannot be empty!";
        public string ByNameErrorString { get; private set; }

        #endregion FRONT-END ERROR STRINGS

        #region COUNTYMAIN

        #region ON INITIALIZED ASYNC

        protected override async Task OnInitializedAsync()
        {
            //TODO: IF USING A ASYNC TASK USE TASK.RUN(...) FOR CPU-BOUND WOK ON A BACKGROUND THREAD
            await RetrieveAllCountiesAsync();
            CountyDropdown.CountyName = ListAllCounties.ToString();
            //CountyDropdown.CountyName = RetrieveAllCountiesAsync().ToString();
        }

        #endregion ON INITIALIZED ASYNC 

        #region SEARCH BY NAME                
        public string /*async Task<string>*/ SearchCountiesAsync(string CountyName)
        {

            if ((CountySearch.CountyName != null) && (CountySearch.CountyName != string.Empty))
            {

                try { /*await*/ CountyQueryServiceTasks.CountyNameSearchAsync(CountyName); }
                catch (Exception) { return ByNameErrorString; }
            }
            return CountyName;
        }
        #endregion SEARCH BY NAME

        public async void SearchBarAndCountyTextboxSync()
        {

            await RetrieveAllCountiesAsync();
        }

        #region SEARCH/DROPDOWN CHANGE EVENT
        //public List<StateMainForm> CountyNameFilledIn(CountyMasterMainForm CountyName) { 
        //    List<StateMainForm> States = null;
        //public List<string> CountyNameFilledIn(string CountyName) { 
        //    List<string> Result = null;
        public string CountyNameFilledIn(string CountyName)
        {
            string Result = State;
            try
            {
                foreach (var county in ListAllCounties.Where(x => x.CountyName.Contains(CountyName)))
                {
                    // TODO: {DOMAIN LAYER ISSUE} CANNOT IMPLICITLY CONVERT SYSTEM.GUID TO INT
                    int Id = county.Id; //column.Guid(Id);
                    string State = county.StateId;

                }
                ///string CountySearch()
                ///{
                ///    StateId = State;
                ///
                ///    return State;
                ///};
            }
            catch (Exception)
            {

            }
            return Result;
        }
        #endregion SEARCH/DROPDOWN CHANGE EVENT

        #region DROPDOWN
        public List<CountyMasterMainForm> ListAllCounties { get; set; } = new List<CountyMasterMainForm>();
        /// TODO: {CHANGE RETURN TYPE TO RETURN 'CountyList} 
        /// OLD VERSION: async Task PopulateCounties(int page = 1, int quantityPerPage = 10)
        public async Task</*IReadOnly*/List<CountyMasterMainForm>> RetrieveAllCountiesAsync()
        {

            return ListAllCounties = await CountyQueryServiceTasks
            //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST 
            .RetrieveAllCountiesAsync((List<CountyMasterMainForm>)ListAllCounties);
        }
        #endregion DROPDOWN       

        #region SAVE NEW COUNTY
        public async void AddNewCountyAsync()
        {
            // OBJECTS RECIEVED FROM FRONT-END AND PASSED TO APPLICATION LAYER
            string CountyName = CountyDropdown.CountyName;
            string State = CountyDropdown.StateId;
            try
            {
                // NULL CHECKS FROM USER INPUT
                if ((CountyName is null) || (State is null))
                // ERROR PROMPT
                { AddNewCountyErrorString = "County and/or State cannot be empty!"; }
                // REQIURED INFO RECIEVED FROM USER 
                // SEND OBJECT(S) TO APP DOMAIN FOR PROCESSING
                else { await CountyApiCommands.SaveNewCounty(CountyName, State); }
                // TODO: UNSURE WHAT ERROR RESPONSES ARE NEEDED FROM APP DOMAIN
                // 1. 
                // 2.
                // OR API 
                // 1. 
                // 2.      

            }
            catch { }
        }
        #endregion SAVE NEW COUNTY

        #endregion COUNTYMAIN

    }
}
