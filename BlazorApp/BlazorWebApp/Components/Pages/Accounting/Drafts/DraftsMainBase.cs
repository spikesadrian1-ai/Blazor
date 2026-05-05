using ApplicationLayer.Entities.Checks;
using ApplicationLayer.Entities.Drafts;
using ApplicationLayer.Services.Accounting.Draft;
using ApplicationLayer.Services.DropdownLists;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.Accounting.Drafts
{
    public class DraftsMainBase : ComponentBase
    {
        #region INJECTIONS
        //[Inject]
        private readonly IDropdownQueryServiceTasks _dropdownQueryServiceTasks; // { get; set; }
        [Inject] public IDraftServices _services { get; set; }

        #endregion INJECTIONS

        #region CONSTRUCTORS

        public DraftsMainBase() { }

        public DraftsMainBase(
            IDraftServices services
            ,
              IDropdownQueryServiceTasks dropdownQueryServiceTasks
        )
        {
            _services = services;
            this._dropdownQueryServiceTasks = dropdownQueryServiceTasks;
        }

        #endregion CONSTRUCTORS

        #region CASCADING PARAMS

        [Parameter] public string ID { get; set; } = string.Empty;
        [Parameter] public string? EntityID { get; set; } = string.Empty;

        [Parameter] public string? OwnerID { get; set; } = string.Empty;

        [Parameter] public bool? InitialState { get; set; } //= string.Empty;

        #endregion CASCADING PARAMS

        public DraftMasterMain draftMasterMain { get; set; } = new DraftMasterMain();

        public async Task OnInitializedAsync(DraftMasterMain draftMasterMain)
        {
            if (string.IsNullOrWhiteSpace(draftMasterMain.Id.ToString()))
            {
                await DraftMainPagePopulateDropdowns();
            }
            else
            {
                //await PopulateForm();
            }
        }


        #region POPULATE FORM

        public async Task<DraftMasterMain> PopulateFormBasedOnEntityID(DraftMasterMain check)
        {
            check.Id = Convert.ToUInt16(ID);
            return check = await _services.PopulateFormBasedOnEntityID(t: check);
        }

        #endregion POPLUATE FORM

        #region SAVE / UPDATE

        public async void SaveButton(DraftMasterMain t)
            => await _services.SaveAsync(t: draftMasterMain);

        #endregion SAVE / UPDATE

        #region DRAFT MAIN PAGE DROPDOWNS

        public async Task<List<string>> DraftMainPagePopulateDropdowns()
        {
            var getPropertyTypesTask = RetrieveAllPropertyIDs();

            await Task.WhenAll(
                               getPropertyTypesTask

                            );

            var report =
                         $"Property_Types: {getPropertyTypesTask.Result},"
                         ;

            return new List<string> { report };
        }

        public List<string> propertyIDList { get; set; } = new List<string>();

        public async Task<List<string>> RetrieveAllPropertyIDs() =>
          propertyIDList = await _dropdownQueryServiceTasks
          .RetrieveListOfPropertyIds();

        #endregion DRAFT MAIN PAGE DROPDOWNS


    }
}
