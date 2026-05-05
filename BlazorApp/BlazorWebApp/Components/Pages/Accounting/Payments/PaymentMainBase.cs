using ApplicationLayer.Entities.Checks;
using ApplicationLayer.Services.Accounting.Payments;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System;
using ApplicationLayer.Entities.NewPayments;

namespace BlazorWebApp.Components.Pages.Accounting.Payments
{
    public class PaymentMainBase : ComponentBase
    {
        [Inject] public IPaymentServices _checkServices { get; set; }

        public PaymentMainBase()
        {

        }

        public PaymentMainBase(
            IPaymentServices checkServices)
        {
            _checkServices = checkServices;
        }

        public NewPayment paymentEntity = new NewPayment();

        #region CASCADING PARAMS

        [Parameter] public string ID { get; set; } = string.Empty;
        [Parameter] public string? EntityID { get; set; } = string.Empty;

        [Parameter] public string? OwnerID { get; set; } = string.Empty;

        [Parameter] public bool? InitialState { get; set; } //= string.Empty;

        #endregion CASCADING PARAMS

        protected override async Task OnInitializedAsync()
        {
            //checkMasterMain.Id = Convert.ToInt16(ID);
            await PopulateFormBasedOnEntityID(paymentEntity);
            //this.wiDeckMasterEntity.PayoutCode = wiDeckMasterEntity.PayoutCode;
            //this.wiDeckMasterEntity.DeckType = wiDeckMasterEntity.DeckType;
            //this.wiDeckMasterEntity.Product = wiDeckMasterEntity.Product;
        }

        #region POPULATE FORM

        public async Task<NewPayment> PopulateFormBasedOnEntityID(NewPayment check)
        {
            check.Id = Convert.ToUInt16(ID);
            return check = await _checkServices.PopulateFormBasedOnEntityID(t: check);
        }

        #endregion POPLUATE FORM

        #region SAVE / UPDATE

        public async void SaveButton(NewPayment t)
            => await _checkServices.SaveAsync(t: paymentEntity);

        #endregion SAVE / UPDATE

    }
}
