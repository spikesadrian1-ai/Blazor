using ApplicationLayer.Entities.Checks;
using ApplicationLayer.Services.Accounting.Check;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.Accounting.Checks
{
    public class CheckBase : ComponentBase
    {
        [Inject] public ICheckServices _checkServices { get; set; }

        public CheckBase()
        {
        }

        public CheckBase(
            ICheckServices checkServices)
        {
            _checkServices = checkServices;
        }

        public CheckMasterMainForm checkModelEntity = new CheckMasterMainForm();

        #region CASCADING PARAMS

        [Parameter] public string ID { get; set; } = string.Empty;
        [Parameter] public string? EntityID { get; set; } = string.Empty;

        [Parameter] public string? IdFromListView { get; set; } = string.Empty;

        [Parameter] public bool? InitialState { get; set; } //= string.Empty;

        #endregion CASCADING PARAMS

        protected override async Task OnInitializedAsync()
        {
            //checkMasterMain.Id = Convert.ToInt16(ID);
            //await PopulateFormBasedOnEntityID(checkModelEntity);
            //this.wiDeckMasterEntity.PayoutCode = wiDeckMasterEntity.PayoutCode;
            //this.wiDeckMasterEntity.DeckType = wiDeckMasterEntity.DeckType;
            //this.wiDeckMasterEntity.Product = wiDeckMasterEntity.Product;
        }

        #region POPULATE FORM

        public async Task<CheckMasterMainForm> PopulateFormBasedOnEntityID(CheckMasterMainForm check)
        {
            check.Id = Convert.ToUInt16(ID);
            return check = await _checkServices.PopulateFormBasedOnEntityID(t: check);
        }

        #endregion POPLUATE FORM

        #region SAVE / UPDATE

        public async void SaveButton(CheckMasterMainForm t)
            => await _checkServices.SaveAsync(t: checkModelEntity);

        #endregion SAVE / UPDATE

        [Parameter] public EventCallback OnLeaseIdSelected { get; set; }

        public void PropertyTypeSelectedEvent(EventArgs args)
            => checkModelEntity.PropertyType = args.ToString();

        public void PropertyIdSelectedEvent(EventArgs args)
            => checkModelEntity.PropertyId = args.ToString();

        public void CheckStatusSelectedEvent(EventArgs args)
            => checkModelEntity.CheckStatus = args.ToString();

        public void PayeeSelectedEvent(EventArgs args)
            => checkModelEntity.PayeeId = args.ToString();
    }
}
