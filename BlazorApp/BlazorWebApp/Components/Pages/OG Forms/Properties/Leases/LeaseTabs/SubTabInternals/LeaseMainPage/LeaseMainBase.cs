using ApplicationLayer.Entities;
using ApplicationLayer.Entities.SurfaceOwners;
using ApplicationLayer.Entities.WorkingInterests;
using ApplicationLayer.Interfaces.ApiQueries._Generics;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Leases;
using ApplicationLayer.Services.Assets.Leases;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.Properties.Leases.LeaseTabs.SubTabInternals.LeaseMainPage
{
    public class LeaseMainBase : ComponentBase
    {
        #region INJECTIONS

        [Inject] public IPopulateForm<LeaseMainForm2> _populateForm { get; set; }
        [Inject] public LeaseMainServices _leaseMainServices { get; set; }

        [Inject] public LeasePopulateDataGrids _leasePopulateDataGrids { get; set; }

        #endregion INJECTIONS

        #region CONSTRUCTORS

        public LeaseMainBase(){}

        public LeaseMainBase
        (
           //  IPopulateForm<LeaseMainForm2> populateForm
           //, 
             LeaseMainServices leaseMainServices
        )
        {
            //this._populateForm = populateForm;
            this._leaseMainServices = leaseMainServices;
        }

        #endregion CONSTRUCTORS


        #region CASCADING PARAMS
        [Parameter] public string? IdFromListView { get; set; } = string.Empty;//"OK01-NOR-001";
        [Parameter] public int ID { get; set; } //= 0;//= string.Empty;
        [Parameter] public string? LeaseTablePK { get; set; } = string.Empty;
        [Parameter] public string? LeaseID { get; set; } = string.Empty;
        [Parameter] public bool? InitialState { get; set; } //= string.Empty;
        [Parameter] public DateTime? Acquired_Date { get; set; } //= DateTime.Today;
        [Parameter] public DateTime? Input_Date { get; set; } //= DateTime.Today;
        [Parameter] public DateTime? Lease_Date { get; set; } //= DateTime.Today;
        [Parameter] public DateTime? Effective_Date { get; set; } = DateTime.Today;

        [Parameter] public bool? fromLeaseFormState { get; set; }


        #endregion CASCADING PARAMS


        public LeaseMainForm2? leaseModel { get; set; } = new LeaseMainForm2();

        //public async Task OnInitializedAsync(LeaseMainForm2 leaseModel)
        //{
        //    //if (InitialState == false)
        //    ////if (string.IsNullOrEmpty(LeaseID))
        //    //{
        //    //    leaseModel.Id = int.Parse(ID);
        //    //    leaseModel.Lease_ID = IdFromListView;
        //    //    await PopulateFormBasedOnEntityID(leaseModel);

        //    //    return;
        //    //}
        //}

        #region POPULATE FORM

        public async Task<LeaseMainForm2> PopulateFormBasedOnEntityID(LeaseMainForm2 getLease)
            => leaseModel = await _populateForm.PopulateFormBasedOnEntityID(t: getLease);
        //{
        //    leaseModel = await _populateForm.PopulateFormBasedOnEntityID(t: getLease);
        //    return await Task.FromResult(leaseModel);
        //}

        #endregion POPULATE FORM


        #region USE CASES

        public bool initialState { get; set; }  
        public bool leaseIdFromListOfLeases = false;

        // TODO: RENAME METHOD TO:
        public bool NavigationFromTractRelatedPageStateManagement(string LeaseID, bool initialState) 
        {
            if ((!string.IsNullOrEmpty(LeaseID)) && (initialState == true))
            //(LeaseID == null)
            {
                initialState = false;
            }

            return initialState;
        }

        #region ADD NEW LEASE

        public async Task<LeaseMainForm2> SaveNewLeaseAsync(LeaseMainForm2 LeaseModel)
            => await _leaseMainServices.SaveAsync(t: LeaseModel);
            //=> await _leaseMainServices.SaveNewLeaseAsync(/*t: */LeaseModel);

        #endregion ADD NEW LEASE

        #region UPDATE OWNER

        public async Task<LeaseMainForm2> UpdateExistingLeaseAsync(LeaseMainForm2 LeaseModel)
            => await _leaseMainServices.UpdateExistingAsync(t: LeaseModel);

        #endregion UPDATE OWNER

        #region DELETE OWNER
        public async Task<LeaseMainForm2> DeleteExistingLeaseAsync(LeaseMainForm2 LeaseModel)
           => await _leaseMainServices.DeleteExistingAsync(t: LeaseModel);

        #endregion DELETE OWNER

        #endregion USE CASES

        #region POPULATE DATAGRIDS

        public async Task<List<string>> PopulateDataGrids(string LeaseID)
        {
            var report = await _leasePopulateDataGrids.PopulateDropdownsByLeaseID(LeaseID);
            return report;
        }

        //public List<TractOwnership> associatedTractOwnersViaLeaseId { get; set; } =
        //    new List<TractOwnership>();
        //public async Task<List<TractOwnership>> RetrieveAllRelatedMineralOwnersByLeaseIdAsync(string LeaseID) 
        //{
        //    return associatedTractOwnersViaLeaseId = 
        //        await _leasePopulateDataGrids.RetrieveRelatedTractMineralOwners(LeaseID);
        //}

        public List<SurfaceOwnership> ListOfSurfaceOwnersViaLeaseId { get; set; } = new List<SurfaceOwnership>();
        public async Task<List<SurfaceOwnership>> RetrieveAllRelatedSurfaceOwnersByLeaseIdAsync(string LeaseID)
        {
            return ListOfSurfaceOwnersViaLeaseId =
                await _leasePopulateDataGrids.RetrieveAllRelatedSurfaceOwnersByLeaseIdAsync(LeaseID);
        }

        public List<WideckMaster> ListOfWorkingInterestsViaLeaseId { get; set; } = new List<WideckMaster>();
        public async Task</*IReadOnly*/List<WideckMaster>> RetrieveAllRelatedWorkingInterestsByLeaseIdAsync(string LeaseID)
            => await _leasePopulateDataGrids.RetrieveAllRelatedWorkingInterestsByLeaseIdAsync(LeaseID);
        //{
        //    return ListOfWorkingInterestsViaLeaseId =
        //        await _leasePopulateDataGrids.RetrieveAllRelatedWorkingInterestsByLeaseIdAsync(LeaseID);
        //}

        public List<WideckInterest> ListOfWorkingInterestDetailsViaLeaseId { get; set; } = new List<WideckInterest>();
        public async Task<List<WideckInterest>> RetrieveAllRelatedWorkingInterestsDetailsByLeaseIdAsync(string LeaseID)
            => await _leasePopulateDataGrids.RetrieveAllRelatedWorkingInterestsDetailsByLeaseIdAsync(LeaseID);
        //{
        //    return ListOfWorkingInterestDetailsViaLeaseId =

        //    //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST
        //    .RetrieveAllRelatedWorkingInterestsDetailsByLeaseIdAsync(LeaseID);

        //}

        public List<ApplicationLayer.Entities.SpecialObligation.SpecialObligations> ListOfSpecialObligationsViaLeaseId =
            new List<ApplicationLayer.Entities.SpecialObligation.SpecialObligations>();
        public async Task<List<ApplicationLayer.Entities.SpecialObligation.SpecialObligations>> RetrieveAllRelatedSpecialObligationsByLeaseIdAsync(string LeaseID)
            => await _leasePopulateDataGrids.RetrieveAllRelatedSpecialObligationsByLeaseIdAsync(LeaseID);
        //{
        //    return ListOfSpecialObligationsViaLeaseId = 
        //        await _leasePopulateDataGrids.RetrieveAllRelatedSpecialObligationsByLeaseIdAsync(LeaseID);
        //    //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST
        //    .RetrieveAllRelatedSpecialObligationsByLeaseIdAsync(LeaseID);
        //}

        public List<WellTractsConnection> ListOfWellsViaLeaseId { get; set; } = new List<WellTractsConnection>();
        //public async Task<List<WellTractsConnection>> RetrieveAllRelatedWellsByLeaseIdAsync(string LeaseID)
        //    => await _leasePopulateDataGrids.RetrieveAllRelatedWellsByLeaseIdAsync(LeaseID);
        //{
        //    return ListOfWellsViaLeaseId = await _leaseRelatedPageServices
        //    .RetrieveAllRelatedWellsByLeaseIdAsync(LeaseID);
        //}

        public List<ApplicationLayer.Entities.Burden.Burdens> ListAllBurdensViaLeaseId { get; set; } = new List<ApplicationLayer.Entities.Burden.Burdens>();
        public async Task</*IReadOnly*/List<ApplicationLayer.Entities.Burden.Burdens>> RetrieveAllRelatedBurdensByLeaseIdAsync(string LeaseID)
        {
            //try {
            return ListAllBurdensViaLeaseId = await _leasePopulateDataGrids
            //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST
            .RetrieveAllRelatedBurdensByLeaseIdAsync(LeaseID);

        }

        public List<ApplicationLayer.Entities.Payments.PaymentObligations> ListOfPaymentObligationsViaLeaseId { get; set; }
            = new List<ApplicationLayer.Entities.Payments.PaymentObligations>();
        public async Task<List<ApplicationLayer.Entities.Payments.PaymentObligations>> RetrieveAllRelatedPaymentObligationsByLeaseIdAsync(string LeaseID)
        {
            return ListOfPaymentObligationsViaLeaseId = await _leasePopulateDataGrids
            //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST
            .RetrieveAllRelatedPaymentObligationsByLeaseIdAsync(LeaseID);

        }

        public List<ApplicationLayer.Entities.SpecialProvision.SpecialProvisions> ListOfSpecialProvisionsViaLeaseId = new List<ApplicationLayer.Entities.SpecialProvision.SpecialProvisions>();
        public async Task<List<ApplicationLayer.Entities.SpecialProvision.SpecialProvisions>> RetrieveAllRelatedSpecialProvisionsByLeaseIdAsync(string LeaseID)
        {
            return ListOfSpecialProvisionsViaLeaseId = await _leasePopulateDataGrids
            //AFTER MAKING INTO A READONLYLIST HAVE TO IMPLICTLY CAST BACK TO A LIST
            .RetrieveAllRelatedSpecialProvisionsByLeaseIdAsync(LeaseID);
        }

        #endregion POPULATE DATAGRIDS

        

        #region CROSSREFS

        // 

        #endregion CROSSREFS

    }
}
