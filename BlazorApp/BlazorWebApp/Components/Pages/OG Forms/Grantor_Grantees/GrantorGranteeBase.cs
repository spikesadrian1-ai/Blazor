using ApplicationLayer.Entities.Grantor_Grantees;
using ApplicationLayer.Services.Owners.GranteesGrantors;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.Grantor_Grantees
{
    public class GrantorGranteeBase : ComponentBase
    {
        #region INJECTIONS

        //[Inject] GranteesGrantorsServices _grantorsServices { get; set; }
        private readonly IGranteesGrantorsServices _granteesGrantorsServices;


        #endregion INJECTIONS

        #region CONSTRUCTORS

        public GrantorGranteeBase() { }

        public GrantorGranteeBase(
            IGranteesGrantorsServices granteesGrantorsServices
            )
        {
            this._granteesGrantorsServices = granteesGrantorsServices;
        }

        #endregion CONSTRUCTORS

        public GrantorsGrantees grantorsGrantees { get; set; }
            = new GrantorsGrantees();

        #region CASCADING PARAMS
        [Parameter] public bool? InitialState { get; set; } 
        [Parameter] public string? ID { get; set; } = string.Empty;
        [Parameter] public string? IdFromListView { get; set; } = string.Empty;//"OK01-NOR-001";
        [Parameter] public string? EntityID { get; set; } = string.Empty;
        [Parameter] public string? LeaseID { get; set; } = string.Empty;
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
