using ApplicationLayer.Entities;
using ApplicationLayer.Services.Assets.Wells;
using ApplicationLayer.Services.DropdownLists;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.Properties.Wells
{
    public class WellMainBase : ComponentBase
    {
        #region INJECTIONS
        [Inject] public IWellServices _wellServices { get; set; }
        [Inject] public IDropdownQueryServiceTasks _dropdownQueryServiceTasks { get; set; }
        #endregion INJECTIONS

        #region CONSTRUCTORS

        public WellMainBase() { }

        public WellMainBase
        (
            IWellServices wellServices
            //WellMainForm2 _wellMainForm2
        )
        {
            this._wellServices = wellServices;
            //wellModel = _wellMainForm2;
        }

        #endregion CONSTRUCTORS

        public bool WellIdFromListOfLeases = false;

        public WellMainForm2? wellModel { get; set; } = new WellMainForm2();
        
        [Parameter] public bool InitialState { get; set; }
        [Parameter] public string? WellId { get; set; } = string.Empty;
        [Parameter] public string? IdFromListView { get; set; } = string.Empty;//"OK01-NOR-001";
        [Parameter] public string? EntityID { get; set; } = string.Empty;


        //protected async Task OnInitializedAsync()
        //{
        //    //if (InitialState == false)
        //    //{
        //    //    //wellModel.Id = Convert.ToInt16(EntityID);
        //    //    wellModel.WellIdNo = IdFromListView;
        //    //    await _wellServices.PopulateFormBasedOnEntityID(t: wellModel);
        //    //}
        //    //else
        //    //if (InitialState == true)
        //    //{
        //    //    await PopulateDropdowns();
        //    //}

        //    //wellModel.Id = Convert.ToInt16(EntityID);
        //    //wellModel.WellIdNo = IdFromListView;
        //    //await PopulateFormBasedOnEntityID(wellModel);
        //}

        protected async Task OnInitializedAsync(WellMainForm2 wellModel)
        {
            //if (InitialState == false)
            //{
            //    //wellModel.Id = Convert.ToInt16(EntityID);
            //    wellModel.WellIdNo = IdFromListView;
            //    await _wellServices.PopulateFormBasedOnEntityID(t: wellModel);
            //}
            //else
            //if (InitialState == true)
            //{
            //    await PopulateDropdowns();
            //}

            //wellModel.Id = Convert.ToInt16(EntityID);
            //wellModel.WellIdNo = IdFromListView;
            //await PopulateFormBasedOnEntityID(wellModel);
        }


        #region WELL MAIN PAGE DROPDOWNS
        public async Task<List<string>> PopulateDropdowns()
        {
            var getWellTypesTask = RetrieveWellTypes(); //welltype
            var getWellStatusTask = RetrieveWellStatus(); //WellStatus
            var getWellAcquisitionTypesTask = RetrieveAcquisitionTypes();    //ACquisitionCodes
            var getPayoutCodesTask = RetrievePayoutCodes();
            var getAllProspectsTask = RetrieveAllProspectIDs();
            var getDistrictsTask = RetrieveAllDistrictIDs();
            var getAllRegionsTask = RetrieveAllRegions();
            //var getResponsiblePartyTask = RetrieveResponsiblePartyList(); //welltype

            await Task.WhenAll(
                               getWellTypesTask
                             , getWellStatusTask
                             , getWellAcquisitionTypesTask
                             , getPayoutCodesTask
                             , getAllProspectsTask
                             , getDistrictsTask
                             , getAllRegionsTask
                             //, getResponsiblePartyTask
                            );

            var report = $"Well_Types: {getWellTypesTask.Result}," 
                         + $"Well_Status: {getWellStatusTask.Result},"
                         + $"Acquisition_Types: {getWellAcquisitionTypesTask.Result},"
                         + $"Payout_Codes: {getPayoutCodesTask.Result},"
                         + $"Prospects: {getAllProspectsTask.Result},"
                         + $"Districts: {getDistrictsTask.Result},"
                         + $"Regions: {getAllRegionsTask.Result},"
                         //+ $"Associates: {getResponsiblePartyTask.Result},"
                         ;

            return new List<string> { report };
        }

        public List<string> wellTypes { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveWellTypes() =>
           wellTypes = await _dropdownQueryServiceTasks
           .RetrieveWellTypes(wellTypes/*: _dropdownQueryServiceTasks.wellTypes*/);

        public List<string> wellStatusTypes { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveWellStatus() =>
            wellStatusTypes = await _dropdownQueryServiceTasks
            .RetrieveWellStatuses(wellStatusTypes);

        public List<string> wellAcquisitionTypes { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveAcquisitionTypes() =>
            wellAcquisitionTypes = await _dropdownQueryServiceTasks
            .RetrieveWellStatuses(wellAcquisitionTypes);

        public List<string> payoutCodes { get; set; } = new List<string>();
        public async Task<List<string>> RetrievePayoutCodes() =>
            /*_dropdownQueryServiceTasks.*/payoutCodes = await _dropdownQueryServiceTasks
            .RetrievePayoutCodes(payoutCodes);

        public List<string> ListOfDistricts = new List<string>();
        public async Task<List<string>> RetrieveAllDistrictIDs() =>
            ListOfDistricts = await _dropdownQueryServiceTasks
            .RetrieveAllDistrictsAsync(ListOfDistricts);

        public List<string> ListOfRegions { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveAllRegions() =>
            ListOfRegions = await _dropdownQueryServiceTasks
            .RetrieveAllRegionsAsync(ListOfRegions);

        public List<string> prospectList = new List<string>();
        public async Task<List<string>> RetrieveAllProspectIDs() //=>
        {
            //List<ProspectMainForm> ListOfProspects;
            return /*wellModel.*/ prospectList = await _dropdownQueryServiceTasks
            .RetrieveAllProspectsAsync2();
        }

        //public List<string> associateLists { get; set; } = new List<string>();
        //public async Task<List<string>> RetrieveResponsiblePartyList() =>
        //   associateLists = await _dropdownQueryServiceTasks
        //   .RetrieveListOFAssociates();


        #endregion WELL MAIN PAGE DROPDOWNS

        //private async Task<WellMainForm2> PopulateForm()
        //{
        //    throw new NotImplementedException();
        //}

        #region POPULATE FORM

        public async Task<WellMainForm2> PopulateFormBasedOnEntityID(WellMainForm2 t)
            => wellModel = await _wellServices.
            PopulateFormBasedOnEntityID(t);
                
        #endregion POPLUATE FORM
    }
}
