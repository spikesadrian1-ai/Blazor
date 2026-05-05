using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.Locations.Prospects.ProspectTabs
{
    public class DistrictMainBase : ComponentBase
    {
        #region CASCADING PARAMS

        [Parameter] public string? EntityID { get; set; } = string.Empty;
        [Parameter] public bool? InitialState { get; set; } //= string.Empty;
        [Parameter] public string? IdFromListView { get; set; } = string.Empty;//"OK01-NOR-001";
        [Parameter] public string? NewContractID { get; set; } = string.Empty;
        [Parameter] public string? ContractId { get; set; } = string.Empty;

        #endregion CASCADING PARAMS
    }
}
