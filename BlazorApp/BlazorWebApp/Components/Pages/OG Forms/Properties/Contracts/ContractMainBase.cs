using ApplicationLayer.Entities.Contracts;
using ApplicationLayer.Interfaces.ApiCommands.Assets.LandProperty.Contract;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Contract;
using ApplicationLayer.Services.DropdownLists;
using ApplicationLayer.UseCases.Assets.Contract.CheckIfContractExists;
using ApplicationLayer.UseCases.Assets.Contract.CreateEntity;
using ApplicationLayer.UseCases.Assets.Contract.DeleteEntity;
using ApplicationLayer.UseCases.Assets.Contract.UpdateEntity;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.Properties.Contracts
{
    public class ContractMainBase : ComponentBase
    {
        #region INJECTIONS
        public DoesItAlreadyExists _doesItAlreadyExists { get; set; }
        public CreateNewContract _createNewContract { get; set; }
        public DeleteEntity _deleteEntity { get; set; }
        public UpdateContract _updateExistingContract { get; set; }
        public IDropdownQueryServiceTasks _dropdownQueryServiceTasks { get; set; }
        [Inject] public ContractApiCommands _contractApiCommands { get; set; }
        [Inject] public IContractDropdowns _contractDropdowns { get; set; }


        #endregion INJECTIONS

        #region CONSTRUCTORS

        public ContractMainBase() { }

        public ContractMainBase(
                ContractApiCommands contractApiCommands
              , DoesItAlreadyExists doesItAlreadyExists
              , CreateNewContract createNewContract
              , UpdateContract updateExistingContract
              , DeleteEntity deleteEntity
              , IDropdownQueryServiceTasks dropdownQueryServiceTasks
              , IContractDropdowns contractDropdowns
            )
        {
            this._contractApiCommands = contractApiCommands;
            this._doesItAlreadyExists = doesItAlreadyExists;
            this._createNewContract = createNewContract;
            this._updateExistingContract = updateExistingContract;
            this._deleteEntity = deleteEntity;
            this._dropdownQueryServiceTasks = dropdownQueryServiceTasks;
            this._contractDropdowns = contractDropdowns;
        }

        #endregion CONSTRUCTORS

        public ContractMainForm2 contractMain { get; set; }
            = new ContractMainForm2();

        #region CASCADING PARAMS

        [Parameter] public string? EntityID { get; set; } = string.Empty;
        [Parameter] public bool? InitialState { get; set; } //= string.Empty;
        [Parameter] public string? IdFromListView { get; set; } = string.Empty;//"OK01-NOR-001";
        [Parameter] public string? NewContractID { get; set; } = string.Empty;
        [Parameter] public string? ContractId { get; set; } = string.Empty;

        #endregion CASCADING PARAMS

        //public async Task OnInitializedAsync(ContractMainForm2 contractMain)
        //{
        //    if (string.IsNullOrWhiteSpace(contractMain.Id.ToString()))
        //    {
        //        await PopulateDropdowns();
        //    }
        //    else
        //    {
        //        //await PopulateForm();
        //    }
        //}

        #region



        #endregion

        #region SEISMIC CHECKBOX

        //bool PerpetualOptionsClause { get; set; }

        // private string ConvertPerpetualOptionClauseToString(ContractMainForm2 contractMainSeismic, bool PerpetualOptionsClause)
        // {
        //     if (PerpetualOptionsClause == true)
        //     {
        //         PerpetualOptionsClause = true;
        //     }

        //     var ConvertPerpetualOptionClause = Convert.ToString(contractMainSeismic.PerpetualOptionClause = PerpetualOptionsClause.ToString());

        //     return ConvertPerpetualOptionClause; //contractMainSeismic.PerpetualOptionClause;
        // }

        #endregion SEISMIC CHECKBOX

        #region USE CASES

        #region CHECK IF ALREADY EXISTS

        public async Task<ContractMainForm2> CheckIfContractAlreadyExists(ContractMainForm2 contract)
            => await _doesItAlreadyExists.CheckIfContractAlreadyExists(contract);

        #endregion CHECK IF ALREADY EXISTS

        #region CREATE

        public async Task<ContractMainForm2> SaveNewLeaseAsync(ContractMainForm2 contract)
            => await _createNewContract.SaveNewLeaseAsync(contract);

        #endregion CREATE

        #region UPDATE

        public async Task<ContractMainForm2> UpdateExistingContract(ContractMainForm2 contract)
            => await _updateExistingContract.UpdateExistingContract(contract);

        #endregion UPDATE

        #region DELETE
        public async Task<ContractMainForm2> DeleteContract(ContractMainForm2 contract)
            => await _deleteEntity.DeleteContract(contract);
        #endregion DELETE

        #endregion USE CASES

        #region POPULATE FORM

        #endregion POPULATE FORM

        #region CONTRACT MAIN PAGE DROPDOWNS
        public async Task<List<string>> PopulateDropdowns()
        {
            var getContractTypesTask = RetrieveContractTypes(); //welltype
            var getSubTypeTask = RetrieveSubTypes(); //WellStatus
            var getStatusTypesTask = RetrieveStatusTypes();    //ACquisitionCodes
            
            await Task.WhenAll(
                               getContractTypesTask
                             , getSubTypeTask
                             , getStatusTypesTask
                             
                            );

            var report = $"Well_Types: {getContractTypesTask.Result},"
                         + $"Well_Status: {getSubTypeTask.Result},"
                         + $"Acquisition_Types: {getStatusTypesTask.Result},"                         
                         ;

            return new List<string> { report };
        }

        public List<string> contractTypes { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveContractTypes() =>
           contractTypes = await _contractDropdowns
           .RetrieveContractTypes();

        public List<string> subTypes { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveSubTypes() =>
            subTypes = await _contractDropdowns
            .RetrieveSubTypes();

        public List<string> statusTypes { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveStatusTypes() =>
            statusTypes = await _contractDropdowns
            .RetrieveStatusTypes();

        #endregion CONTRACT MAIN PAGE DROPDOWNS

        #region CONTRACT MORE PAGE DROPDOWNS
        public async Task<List<string>> PopulateMorePageDropdowns()
        {
            var getContractTypesTask = RetrieveContractMoreDropdownTypes(); //welltype

            await Task.WhenAll(
                               getContractTypesTask

                            );

            var report = $"Associates: {getContractTypesTask.Result},"
                         ;

            return new List<string> { report };
        }

        public List<string> businessAssociatesList { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveContractMoreDropdownTypes() =>
           businessAssociatesList = await _contractDropdowns
           .RetrieveBusinessAssociates();

        #endregion CONTRACT MORE PAGE DROPDOWNS

        #region CONTRACT LOCATION PAGE DROPDOWNS

        public async Task<List<string>> PopulateLocationPageDropdowns()
        {
            var getRegionsTask = RetrieveAllRegionsAsync();
            var getProspectsTask = RetrieveAllProspectsAsync();
            var getDistrictsTask = RetrieveAllDistrictsAsync();
            var getBasinsTask = RetrieveAllBasinsAsync();

            await Task.WhenAll(
                               getRegionsTask
                               , getProspectsTask
                               , getDistrictsTask
                               , getBasinsTask

                            );

            var report = $"Well_Types: {getRegionsTask.Result},"
                         ;

            return new List<string> { report };
        }

        public List<string> regionsList { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveAllRegionsAsync() =>
           regionsList = await _contractDropdowns
           .RetrieveAllRegionsAsync();

        public List<string> prospectList { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveAllProspectsAsync() =>
           prospectList = await _contractDropdowns
           .RetrieveAllProspectsAsync();

        public List<string> districtList { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveAllDistrictsAsync() =>
           districtList = await _contractDropdowns
           .RetrieveAllDistrictsAsync();

        public List<string> basinList { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveAllBasinsAsync() =>
           basinList = await _contractDropdowns
           .RetrieveAllBasinsAsync();

        #endregion CONTRACT LOCATION PAGE DROPDOWNS

    }
}

