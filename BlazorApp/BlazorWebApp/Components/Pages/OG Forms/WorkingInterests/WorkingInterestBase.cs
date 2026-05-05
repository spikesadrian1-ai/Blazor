using ApplicationLayer.Entities.WorkingInterests;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Leases;
using ApplicationLayer.Services.RelatedProperties.WorkingInterest;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.WorkingInterests
{
    public class WorkingInterestBase : ComponentBase
    {
        [Inject] public IWorkingInterestServices _wiServices { get; set; }
        [Inject] public LeasePopulateDataGrids _leasePopulateDataGrids { get; set; }


        public WorkingInterestBase()
        {

        }

        public WorkingInterestBase
        (
               IWorkingInterestServices wiServices
            , LeasePopulateDataGrids leasePopulateDataGrids
            
        )
        {
            this._wiServices = wiServices;
            this._leasePopulateDataGrids = leasePopulateDataGrids;
        }

        public WideckMaster wiDeckMasterEntity = new WideckMaster();

        [Parameter] public string ID { get; set; } = string.Empty;
        [Parameter] public string? LeaseID { get; set; } = string.Empty;
        [Parameter] public string? TractID { get; set; } = string.Empty;
        [Parameter] public string? SuaId { get; set; } = string.Empty;
        [Parameter] public string? RowId { get; set; } = string.Empty;
        [Parameter] public string? DeckId { get; set; } = string.Empty;
        [Parameter] public string? DeckName { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            wiDeckMasterEntity.Id = Convert.ToInt16(ID);
            await PopulateFormBasedOnEntityID(t: wiDeckMasterEntity);
            //this.wiDeckMasterEntity.PayoutCode = wiDeckMasterEntity.PayoutCode;
            //this.wiDeckMasterEntity.DeckType = wiDeckMasterEntity.DeckType;
            //this.wiDeckMasterEntity.Product = wiDeckMasterEntity.Product;
        }

        #region POPULATE FORM

        public async Task<WideckMaster> PopulateFormBasedOnEntityID(WideckMaster t)
        {
            t.Id = Convert.ToUInt16(ID);
            return wiDeckMasterEntity = await _wiServices.PopulateFormBasedOnEntityID(t);
        }

        #endregion POPLUATE FORM

        #region SAVE / UPDATE

        public async void SaveButton(WideckMaster t)
            => await _wiServices.SaveAsync(t);

        #endregion SAVE / UPDATE

        #region SELECTED DROPDOWNS

        /// <summary>
        /// TRACT ID
        /// </summary>
        public void SelectedTractIdEvent(ChangeEventArgs args)
            => wiDeckMasterEntity.TractId = args.Value.ToString();

        /// <summary>
        /// PAYOUT CODES
        /// </summary>
        //[Parameter] public EventCallback<string> OnPayoutSelected { get; set; }
        //private string _payoutCode;
        public void SelectedPayoutCodeEvent(ChangeEventArgs args)
            => wiDeckMasterEntity.PayoutCode = args.Value.ToString();
        //{
        //    //var payoutCode = args.Value.ToString();
        //    //if (payoutCode != null && payoutCode != _payoutCode)
        //    //{
        //    //    _payoutCode = payoutCode;

        //    //    OnPayoutSelected.InvokeAsync(payoutCode);
        //    //}

        //    wiDeckMasterEntity.PayoutCode = args.Value.ToString();

        //}

        /// <summary>
        /// TRACT ID
        /// </summary>
        public void SelectedSuspenseCodeEvent(ChangeEventArgs args)
            => wiDeckMasterEntity.SuspenseCode = args.Value.ToString();

        /// <summary>
        /// TRACT ID
        /// </summary>
        public void SelectedDeckTypeEvent(ChangeEventArgs args)
            => wiDeckMasterEntity.DeckType = args.Value.ToString();

        /// <summary>
        /// TRACT ID
        /// </summary>
        public void SelectedProductTypeEvent(ChangeEventArgs args)
            => wiDeckMasterEntity.Product = args.Value.ToString();

        #endregion SELECTED DROPDOWNS
    }
}
