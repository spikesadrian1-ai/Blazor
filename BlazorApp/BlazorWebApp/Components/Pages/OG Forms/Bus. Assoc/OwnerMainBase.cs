using ApplicationLayer.Entities;
using ApplicationLayer.Entities.Contracts;
using ApplicationLayer.Entities.DropDownList;
using ApplicationLayer.Entities.Owners;
using ApplicationLayer.Interfaces.ApiQueries._Generics;
using ApplicationLayer.Services.DropdownLists;
using ApplicationLayer.Services.Owners.BusinessAssociateOwners.Associates;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.Bus._Assoc
{
    public class OwnerMainBase : ComponentBase
    {
        #region INJECTIONS 

        [Inject]
        public BusinessAssociates _businessAssociates { get; set; }

        [Inject]
        public IDropdownQueryServiceTasks _dropdownQueryServiceTasks { get; set; }
        [Inject]
        public IPopulateForm<OwnerAddressesMain> _populateForm { get; set; }

        #endregion INJECTIONS 

        #region CONSTRUCTORS

        public OwnerMainBase()
        {

        }

        public OwnerMainBase
        (
             BusinessAssociates businessAssociates
           , IDropdownQueryServiceTasks dropdownQueryServiceTasks
        )
        {
            _businessAssociates = businessAssociates;
            _dropdownQueryServiceTasks = dropdownQueryServiceTasks;
        }

        #endregion CONSTRUCTORS
        
        public DomainLayer.Entities.Bus._Assoc.Owners ownersD { get; set; } = new DomainLayer.Entities.Bus._Assoc.Owners();
        public OwnerAddressesMain? owners { get; set; } = new OwnerAddressesMain();
        [Parameter] public bool InitialState { get; set; } //= string.Empty;
        [Parameter] public string? IdFromListView { get; set; } = string.Empty;
        [Parameter] public string? EntityID { get; set; } = string.Empty;
        [Parameter] public string? OwnerID { get; set; } = string.Empty;


        //public async Task OnInitializedAsync(OwnerAddressesMain owners)
        //{
        //    if (string.IsNullOrWhiteSpace(owners.Id.ToString()))
        //    {
        //        await AssociateMainPagePopulateDropdowns();
        //    }
        //    else
        //    {
        //        //await PopulateForm();
        //    }
        //}

        #region USE CASES

        #region CHECK IF OWNER EXIST ALREADY

        public string responseStatus { get; set; } = string.Empty;
        public async Task<OwnerAddressesMain> DoesIdAlreadyExists(OwnerAddressesMain owners, string responseStatus)
            => await _businessAssociates.DoesIdAlreadyExists(owners, responseStatus);

        #endregion CHECK IF OWNER EXIST ALREADY

        #region ADD NEW OWNER

        public async Task<DomainLayer.Entities.Bus._Assoc.Owners> SaveNewOwnerAsync(DomainLayer.Entities.Bus._Assoc.Owners? owners)
            => await _businessAssociates.SaveNewOwnerAsync(owners);

        #endregion ADD NEW OWNER

        #region UPDATE OWNER

        public async Task<DomainLayer.Entities.Bus._Assoc.Owners> UpdateExistingOwnerAsync(DomainLayer.Entities.Bus._Assoc.Owners owners)
            => await _businessAssociates.UpdateExistingOwnerAsync(owners);

        #endregion UPDATE OWNER

        #region DELETE OWNER

        public async void DeleteOwner(DomainLayer.Entities.Bus._Assoc.Owners owners)
        {
            await _businessAssociates.DeleteExistingOwnerAsync(owners); 
        }

        #endregion DELETE OWNER

        #region NEW OWNER ADDRESS POPUP

        public void NewAddressPopupActivated()
        {

        }

        public void AddNewAddress()
        {

        }

        #endregion NEW OWNER ADDRESS POPUP

        //#region ADD NEW ADDRESS TO EXISTING OWNER
        //public async Task<OwnerAddressesMain> AddNewAddress()
        //{
        //    OwnersContactInfo contactInfo = new OwnersContactInfo();
        //    _ownerAddresses.OwnerId = contactInfo.OwnerId;

        //}

        //public async Task<OwnerAddressesMain> AddNewAddress1(OwnersContactInfo contactInfo) {


        //}

        //public async Task<OwnersContactInfo> AddNewAddress2(O) 
        //    =>

        //#endregion ADD NEW ADDRESS TO EXISTING OWNER

        #endregion USE CASES

        #region DROPDOWNS

        public async Task<List<string>> AssociateMainPagePopulateDropdowns()
        {
            //var getTaxIdTypesTask = RetrieveAllTaxIdTypes();
            //var getAddressesTypesTask = RetrieveAllAddressesTypes();
            var getTenureTask = RetrieveTenureCodes();
            //var getNoPayTask = RetrieveAllNoPayCodes();
            //var getGeneralClassTypesTask = RetrieveAllGeneralClassTypes();

            await Task.WhenAll(
                             //  getTaxIdTypesTask
                             //, getAddressesTypesTask
                             //, 
                               getTenureTask
                             //, getNoPayTask
                             //, getGeneralClassTypesTask


                            );

            var report =
                         //$"Property_Types: {getTaxIdTypesTask.Result},"
                         //+
                         //$"Grantors: {getAddressesTypesTask.Result},"
                         //+
                         $"Grantors: {getTenureTask.Result},"
            // soldTo
            // status
                         ;

            return new List<string> { report };
        }

        public List<string> taxIdList { get; set; } = new List<string>();
        //public async Task<List<string>> RetrieveAllTaxIdTypes() =>
        //   taxIdList = await _propertyDropdowns
        //   .RetrieveAllTaxIdTypes();

        public List<string> addressesTypes { get; set; } = new List<string>();
        //public async Task<List<string>> RetrieveAllAddressesTypes() =>
        //    addressesTypes = await _propertyDropdowns
        //    .RetrieveAllAddressesTypes();

        public List<string> tenureCodes { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveTenureCodes() =>
            tenureCodes = await _dropdownQueryServiceTasks
            .RetrieveTenureCodes(tenureCodes);

        public List<string> noPayCodes { get; set; } = new List<string>();
        //public async Task<List<string>> RetrieveAllNoPayCodes() =>
        //    noPayCodes = await _propertyDropdowns
        //    .RetrieveAllNoPayCodes();

        public List<string> generalClassTypes { get; set; } = new List<string>();
        //public async Task<List<string>> RetrieveAllGeneralClassTypes() =>
        //    generalClassTypes = await _propertyDropdowns
        //    .RetrieveAllGeneralClassTypes();

        #endregion DROPDOWNS

        #region POPULATE FORM

        public async Task<OwnerAddressesMain> PopulateFormBasedOnEntityID(OwnerAddressesMain getOwner)
        {
            owners = await _populateForm
                .PopulateFormBasedOnEntityID(t: getOwner);
            return await Task
                .FromResult(owners);
        }

        #endregion POPULATE FORM

        #region CROSSREFS

        // 

        #endregion CROSSREFS

    }
}
