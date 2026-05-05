using ApplicationLayer.Entities;
using ApplicationLayer.Entities.TractOwnerships;
using ApplicationLayer.Interfaces.ApiQueries._Generics;
using ApplicationLayer.Services.Assets.Leases;
using ApplicationLayer.Services.DropdownLists;
using ApplicationLayer.Services.RelatedProperties.TractMineralOwnership;
using ApplicationLayer.UseCases.RelatedProperties.TractMineralOwnership.CreateEntity;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.TractOwners
{
    public class TractOwnerBase : ComponentBase//, IPopulateForm<TractOwnership>
    {
        [Inject] public IPopulateForm<TractOwnership> _populateForm { get; set; }
        [Inject] public ITractMineralOwnershipServices _ownershipServices { get; set; }

        #region CONSTRUCTORS

        public TractOwnerBase()
        {

        }

        public TractOwnerBase
        (
              IPopulateForm<TractOwnership> populateForm
            , 
              ITractMineralOwnershipServices ownershipServices
        )
        {
            this._populateForm = populateForm;
            _ownershipServices = ownershipServices;
        }

        #endregion CONSTRUCTORS


        public TractOwnership ownership { get; set; } = new TractOwnership();
        [Parameter] public string? ID { get; set; } = string.Empty;
        [Parameter] public string? TractID { get; set; } = string.Empty;
        [Parameter] public string? OwnerID { get; set; } = string.Empty;
        [Parameter] public /*List<string>*/ string? Formations { get; set; } = string.Empty; //new List<string>(); //string.Empty;
        [Parameter] public bool InitialState { get; set; } //= string.Empty;
       
        string errorString;

        protected override async Task OnInitializedAsync()
        {
            //if (string.IsNullOrEmpty(TractID))
            if (InitialState == false)
            {
                ownership.Id = int.Parse(ID);
                await PopulateForm(ownership);

                return;
            }

            // if (LeaseID != string.Empty)
            // {
            //     var request = new HttpRequestMessage(HttpMethod.Get,
            //     _config["API_URL"] + $"TractOwnership/SearchAllTractOwnershipByLeaseId/{LeaseID}");/*tractOwnership.*/
            //     var client = _clientFactory.CreateClient();
            //     HttpResponseMessage response = await client.SendAsync(request);

            //     if (response.IsSuccessStatusCode)
            //     {
            //         using var responseStream = await response.Content.ReadAsStreamAsync();
            //         associatedTractOwnersViaTractId = await JsonSerializer
            //                      .DeserializeAsync<List<TractOwnership>>(responseStream);

            //         errorString = null;
            //     }
            //     else
            //     {
            //         errorString = $"There was an error getting Mineral Ownership Data: {response.ReasonPhrase}";
            //     }
            // }

        }

        #region POPULATE FORM

        public async Task<TractOwnership> PopulateForm(TractOwnership t)
        {
            ownership = await _populateForm.PopulateFormBasedOnEntityID(t);
            //ownership = await PopulateFormBasedOnEntityID(t);

            return await Task.FromResult(ownership);
        }

        #endregion POPULATE FORM

        #region
        public async Task<TractOwnership> SaveButton(TractOwnership ownership)
            => await _ownershipServices.SaveAsync(ownership);

        public async void DeleteButton()
            => await _ownershipServices.DeleteExistingAsync(ownership);

        public async Task<TractOwnership> PopulateFormBasedOnEntityID(TractOwnership t)
        => await _ownershipServices.SaveAsync(t);
        #endregion

        #region DROPDOWNS

        //public async Task<List<string>> PopulateDropdowns()
        //{
        //    var getAlltractStatusesTasks = RetrieveAllTractStatusAsync();
        //    var getAllcountyTasks = RetreiveAllCountiesAsync();
        //    var getAllstateTasks = RetrieveAllStatesAsync();
        //    var getAllDistrictTasks = RetrieveAllDistrictsAsync();
        //    var getAllregionTasks = RetrieveAllRegionsAsync();

        //    await Task.WhenAll(
        //                       getAlltractStatusesTasks
        //                     , getAllcountyTasks
        //                     , getAllstateTasks
        //                     , getAllDistrictTasks
        //                     , getAllregionTasks
        //                    );

        //    var report = $"TractStatuses: {getAlltractStatusesTasks.Result}," +
        //                 $"County: {getAllcountyTasks.Result}," +
        //                 $"States: {getAllstateTasks.Result}," +
        //                 $"Districts: {getAllDistrictTasks.Result}," +
        //                 $"Regions: {getAllregionTasks.Result},";

        //    return new List<string> { report };
        //}

        //public List<string> ListOfTractStatuses { get; set; } = new List<string>();
        //public async Task<List<string>> RetrieveAllTractStatusAsync() =>
        //   ListOfTractStatuses = await _statusOfTracts
        //   .RetrieveAllTractStatusAsync();

        //public List<StateMainForm> ListOfStates { get; set; } = new List<StateMainForm>();
        //public async Task<List<StateMainForm>> RetrieveAllStatesAsync() =>
        //    ListOfStates = await _dropdownQueryServiceTasks
        //    .RetrieveAllStatesAsync();

        //public List<CountyMasterMainForm> ListAllCounties { get; set; } = new List<CountyMasterMainForm>();
        //public async Task</*IReadOnly*/List<CountyMasterMainForm>> RetreiveAllCountiesAsync() =>
        //    ListAllCounties = await _countyQueryServiceTasks
        //    .RetrieveAllCountiesAsync((List<CountyMasterMainForm>)ListAllCounties);

        //public List<string> ListOfRegions { get; set; } = new List<string>();
        //public async Task<List<string>> RetrieveAllRegionsAsync() =>
        //    ListOfRegions = await _dropdownQueryServiceTasks
        //    .RetrieveAllRegionsAsync(ListOfRegions);

        //public List<string> ListOfDistricts = new List<string>();
        //public async Task<List<string>> RetrieveAllDistrictsAsync() =>
        //    ListOfDistricts = await _dropdownQueryServiceTasks
        //    .RetrieveAllDistrictsAsync(ListOfDistricts);

        #endregion DROPDOWNS



    }
}
