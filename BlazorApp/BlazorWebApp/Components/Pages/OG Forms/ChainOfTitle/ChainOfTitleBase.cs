using ApplicationLayer.Entities.Titles;
using ApplicationLayer.Services.Owners.ChainOfTitles;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Components.Pages.OG_Forms.ChainOfTitle
{
    public class ChainOfTitleBase : ComponentBase
    {
        #region INJECTIONS

        //[Inject] ChainOfTitleServices _chainOfTitleServices { get; set; }
        private readonly ChainOfTitleServices _chainOfTitleServices;


        #endregion INJECTIONS

        #region CONSTRUCTORS

        public ChainOfTitleBase() { }

        public ChainOfTitleBase(
            ChainOfTitleServices chainOfTitleServices
            )
        {
            this._chainOfTitleServices = chainOfTitleServices;
        }

        #endregion CONSTRUCTORS
        public ChainOfTitleForm chainOfTitle { get; set; }
            = new ChainOfTitleForm();

        #region CASCADING PARAMS
        [Parameter] public bool? InitialState { get; set; }
        [Parameter] public string? ID { get; set; } = string.Empty;
        [Parameter] public string? IdFromListView { get; set; } = string.Empty;//"OK01-NOR-001";
        [Parameter] public string? EntityID { get; set; } = string.Empty;
        [Parameter] public string? CotID { get; set; } = string.Empty;
        [Parameter] public string? GrantorID { get; set; } = string.Empty;

        #endregion CASCADING PARAMS

        //public async Task OnInitializedAsync(GrantorsGrantees grantorsGrantees)
        //{

        //}

        #region USE CASES

        #region CREATE

        #endregion CREATE

        #region UPDATE

        #endregion UPDATE

        #region DELETE

        #endregion DELETE

        #endregion USE CASES

        #region POPULATE FORM

        #endregion POPULATE FORM

    }
}
