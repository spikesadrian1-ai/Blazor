using ApplicationLayer.Entities;
using ApplicationLayer.Entities.Depths;
using ApplicationLayer.Entities.TractOwnerships;
using ApplicationLayer.Services.Assets.Tracts.Depths;
using ApplicationLayer.Services.Assets.Tracts.TractLeaseServices;
using ApplicationLayer.Services.Assets.Tracts.TractOwners;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.Properties.Tracts.TractTabs.SubTabInternals.TractMorePage
{
    public class TractMorePageBase : ComponentBase
    {
        [Inject]
        public TractDepthsSearchServices _tractDepthsSearchServices { get; set; }
        [Inject]
        public TractLeaseServiceTasks _tractLeaseServiceTasks { get; set; }
        //[Inject]
        //public TractOwnersQueryServiceTasks _tractOwnersQueryServiceTasks { get; set; } 

        public TractMorePageBase() { }

        public bool dropdonboxNameSelected;
        public bool boolDropdonboxNameSelected;
        public static string TractID = "OK01-NOR-001";

        public TractOwnership OwnerSearch { get; set; } =
            new TractOwnership(); //= null;


        //protected override async Task OnInitializedAsync()
        //{
        //    await PopulateDataGrids();

        //    //// ADDED TO IMPLEMENT LOADING INDICATOR
        //    //await Task.Run(PopulateDistricts);
        //}

        #region DATAGRIDS

        public async Task<List<string>> PopulateDataGrids()
        {
            var getAllTractDepthsTasks = GetAllTractDepthsAsync(TractID);
            //var getAllTractLeasesTasks = GetAllTractOwnersByTractID(TractId);
            //var getAllstateTasks = RetrieveAllStatesAsync();
            //var getAllDistrictTasks = RetrieveAllDistrictsAsync();
            //var getAllregionTasks = RetrieveAllRegionsAsync();

            await Task.WhenAll(
                 getAllTractDepthsTasks
                //, getAllTractLeasesTasks
                //, getAllstateTasks
                //, getAllDistrictTasks
                //, getAllregionTasks
                );

            //await Task.WhenAll(RetreiveAllCountiesAsync(), RetrieveAllStatesAsync());

            var report = $"Depths: {getAllTractDepthsTasks.Result},"
                         //+
                         //$"Leases: {getAllTractLeasesTasks.Result}," 
                         //+
                         //$"States: {getAllstateTasks.Result}," +
                         //$"Districts: {getAllDistrictTasks.Result}," +
                         //$"Regions: {getAllregionTasks.Result},"
                         ;

            return new List<string> { report };
        }

        public List<string> TractDepth { get; set; } = new List<string>();
        public async Task<List<string>> GetAllTractDepthsAsync(string TractId) =>
            TractDepth = await _tractDepthsSearchServices
            .RetrieveAllTractDepthsAsync(TractId);

        //public List<TractOwnership> associatedTractOwnersViaTractId { get; set; } = new 
        //    List<TractOwnership>();
        //public async Task<List<TractOwnership>> GetAllTractOwnersByTractID(string TractId) =>
        //    associatedTractOwnersViaTractId = await _tractOwnersQueryServiceTasks
        //    .RetrieveAllTractOwnersByTractID(TractId);


        //public List<EasementMainForm> ListAllEasements { get; set; } = new
        //    List<EasementMainForm>();
        //public async Task<List<EasementMainForm>> RetreiveAllCountiesAsync()
        //{
        //    //try {
        //    return ListAllEasements = await _countyQueryServiceTasks
        //    //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST
        //    .RetrieveAllCountiesAsync((List<EasementMainForm>)ListAllEasements);

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

        #endregion DATAGRIDS
    }
}
