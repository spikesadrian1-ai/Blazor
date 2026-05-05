using ApplicationLayer.Entities;
using ApplicationLayer.Entities.Drafts;
using ApplicationLayer.Entities.Royalty_ORRIs;
using ApplicationLayer.Services.Assets.Leases.LeaseRelated;
using ApplicationLayer.Services.DropdownLists;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.Royalty_ORRIs
{
    public class ORRIMain : ComponentBase
    {

        [Inject]
        public LeaseRelatedPageServices _leaseRelatedPageServices { get; set; }
        [Inject]
        public IDropdownQueryServiceTasks _dropdownQueryServiceTasks { get; set; }

        public ORRIMain() { }


        #region CASCADE PARAMS
        public Royalty? royaltyEntity { get; set; } = new Royalty();
        [Parameter] public bool? InitialState { get; set; } //= string.Empty;
        [Parameter] public string? LeaseID { get; set; } = string.Empty;
        [Parameter] public string? TractId { get; set; } = string.Empty;
        [Parameter] public string? EntityId { get; set; } = string.Empty;
        [Parameter] public string? EasementId { get; set; } = string.Empty;
        [Parameter] public string? SuaId { get; set; } = string.Empty;
        [Parameter] public string? RowId { get; set; } = string.Empty;
        [Parameter] public string? OwnerId { get; set; } = string.Empty;
        [Parameter] public bool? fromTractFromState { get; set; } //= string.Empty;
        [Parameter] public bool? fromLeaseFormState { get; set; }

        #endregion CASCADE PARAMS

        //public async void OnInitializedAsync() 
        //{
        //    await RetrieveOwnersAsync();
        //}
        //public async Task OnInitializedAsync(Royalty royaltyEntity)
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

        public async Task OnAfterRenderAsync(bool firstRender)
        {
            //if (InitialState == false)
            ////if (string.IsNullOrEmpty(LeaseID))
            //{
            //    leaseModel.Id = int.Parse(ID);
            //    leaseModel.Lease_ID = IdFromListView;
            //    await PopulateFormBasedOnEntityID(leaseModel);

            //    return;
            //}
        }


        //#region POPULATE FORM

        //public async Task<DraftMasterMain> PopulateFormBasedOnEntityID(DraftMasterMain check)
        //{
        //    check.Id = Convert.ToUInt16(ID);
        //    return check = await _services.PopulateFormBasedOnEntityID(t: check);
        //}

        //#endregion POPLUATE FORM

        #region SAVE / UPDATE

        public async void SaveButton(Royalty t)
        // => await _services.SaveAsync(t: draftMasterMain);
        {
            throw new System.NotImplementedException();
        }

        #endregion SAVE / UPDATE

        #region DROPDOWNS

        //public async Task<List<string>> PopulateDropdowns()
        //{
        //    //var getOwnersTask = RetrieveOwnersAsync();
        //    // var getTractsTask = RetreiveTractsAsync();
        //    // var getRoyOwnersTask = RetrieveRoyOwnersAsync();
        //    // var getWIOwnersTask = RetrieveWIOwnersAsync();
        //    var getOrriTypesTask = RetrieveOrriTypesAsync();
        //    // var getOrriReductionsTask = RetrieveOrriReductionsAsync();
        //    // var getPayoutCodesTask = RetrievePayoutCodesAsync();
        //    // var getSubstancesTask = RetrieveSubstancesAsync();

        //    await Task.WhenAll(
        //                        // getOwnersTask
        //                      // , getTractsTask
        //                      // , getRoyOwnersTask
        //                      // , getWIOwnersTask                        
        //                        getOrriTypesTask
        //                    // , getOrriReductionsTask
        //                    // , getPayoutCodesTask
        //                    // , getSubstancesTask
        //                    );

        //    var report = //$"Owners: {getOwnersTask.Result}," +
        //                 //              $"Tracts: {getTractsTask.Result}," +
        //                 //              $"RoyOwners: {getRoyOwnersTask.Result}," +
        //                 //              $"WIOwners: {getWIOwnersTask.Result}," +                     
        //                $"OrriTypes: {getOrriTypesTask.Result},"
        //                // $"OrriReductions: {getOrriReductionsTask.Result}," +
        //                // $"PayoutCodes: {getPayoutCodesTask.Result}," +
        //                // $"Substances: {getSubstancesTask.Result},"
        //                ;

        //    return new List<string> { report };
        //}

        public ICollection<string>? associateLists { get; set; } //= new ICollection<string>();
        public async Task<ICollection<string>> RetrieveOwnersAsync() =>
           /*(List<string>)*/(associateLists = await _dropdownQueryServiceTasks.RetrieveListOFAssociates());


        // public List<StateMainForm> TractList { get; set; } = new List<StateMainForm>();
        // public async Task<List<StateMainForm>> RetreiveTractsAsync() =>
        //     TractList = await _leaseRelatedPageServices.RetrieveAllTractLeasesAsync(LeaseID);

        // public List<CountyMasterMainForm> RoyOwnerList { get; set; } = new List<CountyMasterMainForm>();
        // public async Task</*IReadOnly*/List<CountyMasterMainForm>> RetrieveRoyOwnersAsync() =>
        //     RoyOwnerList = await _leaseRelatedPageServices.

        // public List<RegionMasterMainForm> WIOwnersList { get; set; } = new List<RegionMasterMainForm>();
        // public async Task<List<RegionMasterMainForm>> RetrieveWIOwnersAsync() =>
        //     WIOwnersList = await _leaseRelatedPageServices


        // TODO: FIGURE OUT WHY IM GETTING AN ERROR BUT NOT ON THE 'TRACT STATUS' DROPDOWN
        // ON THE CREATETRACTFORM
        public List<string> OrriTypeList = new List<string>();
        public async Task<List<string>> RetrieveOrriTypesAsync() =>
            OrriTypeList = await _leaseRelatedPageServices.RetrieveORRITypes();

        // public List<DistrictMasterModel> OrriReductionTypes = new List<DistrictMasterModel>();
        // public async Task<List<DistrictMasterModel>> RetrieveOrriReductionsAsync() =>
        //     OrriReductionTypes = await _dropdownQueryServiceTasks
        //     .RetrieveAllDistrictsAsync(ListOfDistricts);

        // public List<DistrictMasterModel> PayoutCodesList = new List<DistrictMasterModel>();
        // public async Task<List<DistrictMasterModel>> RetrievePayoutCodesAsync() =>
        //     PayoutCodesList = await _dropdownQueryServiceTasks
        //     .RetrieveAllDistrictsAsync(ListOfDistricts);

        // public List<DistrictMasterModel> SubstanceTypeList = new List<DistrictMasterModel>();
        // public async Task<List<DistrictMasterModel>> RetrieveSubstancesAsync() =>
        //     SubstanceTypeList = await _dropdownQueryServiceTasks
        //     .RetrieveAllDistrictsAsync(ListOfDistricts);

        #endregion DROPDOWNS

        /// <summary>
        /// POPULATES THE TRACT_ID COMBO BOX WITH ALL RELEVENT TRACT ID'S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lease_IDTextBox8_TextChanged(object sender, EventArgs e)
        {
            //SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Tracts_LeaseConnection WHERE Lease_ID LIKE '" + lease_IDTextBox8.Text + "%'", sqlCon);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    comboBox1.Items.Add(dt.Rows[i]["Tract_ID"]);
            //}
        }


        /// <summary>
        ///     INTERESTS COMBOBOX SELECTED VALUE CHANGED IF/ELSE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Parameter] public EventCallback InterestTypeSelectedValueChanged { get; set; }
        [Parameter] public EventCallback<Royalty> OnChanged { get; set; }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            //ValueChanged = OnChanged;
            //interest_TypeComboBox_SelectedValueChanged = InterestTypeSelectedValueChanged;
        }
        public void InterestTypeComboBox_SelectedValueChanged(ChangeEventArgs args)
        //public void interest_TypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        //public void interest_TypeComboBox_SelectedValueChanged(EventArgs e)
        {
            royaltyEntity.InterestType = args.Value.ToString();
            //var _interestType = royaltyEntity.InterestType;

            //return _interestType;
            //_ = e.Value.ToString();
            //if (leaseSelected != null && leaseSelected != _leaseIdSelected)
            //{
            //    _leaseIdSelected = leaseSelected;

            //    InterestTypeSelectedValueChanged.InvokeAsync(leaseSelected);
            //}

            if ((string)args.Value == "Royalty Interest")
            //if (royaltyEntity.PayoutCode == "Royalty Interest")
            {
                ////    TAKEN CARE OF BY THE UI
                //oRRI_TypeComboBox.Visible = false;
                //oRRI_Reduction_TypeComboBox.Visible = false;
                //listBox2.Visible = false; ////RoyOwnerIDDropdown
                //listBox3.Visible = true;  ////WIOwnerIDDropdown

                ////////
                //listBox4.Items.Clear();  ////OwnerIDDropdown
                //listBox3.Items.Clear();

                //listBox2.Items.Clear();
                //listBox1.Items.Clear();  ////FEATURE NOT AVAILABLE YET: USED WHEN USNIG THE MULTI SELECT FEATURE
                //comboBox1.ResetText();   ////FEATURE NOT AVAILABLE YET: USED WHEN USNIG THE MULTI SELECT FEATURE
                associateLists.Clear();
                TractId = string.Empty;
                EntityId = string.Empty;
            }
            else
            if ((string)args.Value == "Excess Royalty")
                //(royaltyEntity.InterestType.Equals("Excess Royalty"))
                {
                //oRRI_TypeComboBox.Visible = false;
                //oRRI_Reduction_TypeComboBox.Visible = false;
                //listBox2.Visible = false;
                //listBox3.Visible = false;

                ////////
                //listBox4.Items.Clear();
                //listBox1.Items.Clear();
                //comboBox1.ResetText();
                associateLists.Clear();
                TractId = string.Empty;
                //iDTextBox3.ResetText();
            }
            //else
            //if (royaltyEntity.InterestType.Equals("ORRI"))
            //{
            //    oRRI_TypeComboBox.Visible = true;
            //    oRRI_Reduction_TypeComboBox.Visible = true;
            //    listBox2.Visible = false;
            //    listBox3.Visible = true;

            //    ////////              
            //    listBox4.Items.Clear();
            //    listBox3.Items.Clear();
            //    listBox1.Items.Clear();
            //    comboBox1.ResetText();
            //    associateLists.Clear();
            //    TractId = string.Empty;
            //    iDTextBox3.ResetText();
            //}
            //else
            //if (royaltyEntity.InterestType.Equals("Non-Participating Royalty Interest"))
            //{
            //    oRRI_TypeComboBox.Visible = false;
            //    oRRI_Reduction_TypeComboBox.Visible = false;
            //    listBox2.Visible = true;
            //    listBox3.Visible = false;

            //    //////
            //    listBox4.Items.Clear();
            //    listBox1.Items.Clear();
            //    comboBox1.ResetText();
            //    associateLists.Clear();
            //    TractId = string.Empty;
            //    iDTextBox3.ResetText();
            //}
            //else if (royaltyEntity.InterestType.Equals("Profit Share")
            //        || (royaltyEntity.InterestType.Equals("Special")
            //        || (royaltyEntity.InterestType.Equals("Unleased Mineral Interest")
            //        || (royaltyEntity.InterestType.Equals("Net Profits")))))
            //{
            //    oRRI_TypeComboBox.Visible = false;
            //    oRRI_Reduction_TypeComboBox.Visible = false;
            //    listBox2.Visible = false;
            //    listBox3.Visible = false;

            //    //////
            //    tract_IDcomboBox2.ResetText();
            //    associateLists.Clear();
            //    TractId = string.Empty;
            //    iDTextBox3.ResetText();
            //}
        }

        public void OwnerIDComboBox_SelectedValueChanged(ChangeEventArgs args)
            => royaltyEntity.OwnerId = args.Value.ToString();
        public void TractIDComboBox_SelectedValueChanged(ChangeEventArgs args)
            => royaltyEntity.TractId = args.Value.ToString();
        public void RoyOwnerIDComboBox_SelectedValueChanged(ChangeEventArgs args)
            => royaltyEntity.RoyOwnerId = args.Value.ToString();
        public void InterestFormulaEnteredOrChanged(ChangeEventArgs args)
            => royaltyEntity.InterestFormula = args.Value.ToString();
        public void WIOwnerIDComboBox_SelectedValueChanged(ChangeEventArgs args)
            => royaltyEntity.WiOwnerId = args.Value.ToString();
        public void InterestCalcsEnteredOrChanged(ChangeEventArgs args) 
            => royaltyEntity.Interest = (decimal?)args.Value;
        //public void InterestTypeComboBox_SelectedValueChanged(ChangeEventArgs args)
        //    => royaltyEntity.InterestType = args.Value.ToString();
        public void PayoutCodeType_SelectedValueChanged(ChangeEventArgs args)
            => royaltyEntity.PayoutCode = args.Value.ToString();
        public void ReductionType_SelectedValueChanged(ChangeEventArgs args)
            => royaltyEntity.OrriReductionType = args.Value.ToString();
        public void EffectiveDate_SelectedValueChanged(ChangeEventArgs args)
            => royaltyEntity.EffectiveDate = (DateTime?)args.Value;
        public void ProductType_SelectedValueChanged(ChangeEventArgs args)
            => royaltyEntity.Substance = args.Value.ToString();
        public void ExpirationDate_SelectedValueChanged(ChangeEventArgs args)
            => royaltyEntity.ExpireDate = (DateTime?)args.Value;

        /// <summary>
        ///     WORKING INTERESTS LISTBOX
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (LeaseID != string.Empty)
            //{
            //    //////////////////////////////////////
            //    tract_IDTextBox2.Text = (comboBox1.Text.ToString());
            //    tract_IDcomboBox2.Text = comboBox1.Text.ToString();
            //    //////////////////////////////////////
            //    // Check SQL Connection
            //    if (sqlCon.State == ConnectionState.Closed)
            //        //        // And if closed, then open
            //        sqlCon.Open();
            //    //////////////////////////////////////
            //    if (royaltyEntity.InterestType.Equals(String.Empty))
            //    {
            //        ////
            //        //throw Exception.ReferenceEquals.ToString();
            //        MessageBox.Show("Please Select an Interest Type.");
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Royalty Interest") || (royaltyEntity.InterestType.Equals("ORRI")))
            //    {
            //        //SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM dbo.WIDeckMaster WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM dbo.WIDeckInterest WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        DataTable dt1 = new DataTable();
            //        sda1.Fill(dt1);
            //        for (int i = 0; i < dt1.Rows.Count; i++)
            //        {
            //            listBox3.Items.Add(dt1.Rows[i]["Owner_ID"]);
            //        }

            //        dt1.Clear();
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Excess Royalty"))
            //    {

            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Non-Participating Royalty Interest"))
            //    {
            //        SqlDataAdapter sda5 = new SqlDataAdapter("SELECT * FROM dbo.Royalty WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        //SqlDataAdapter sda5 = new SqlDataAdapter("SELECT * FROM dbo.Tract_OWnership WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        DataTable dt5 = new DataTable();
            //        sda5.Fill(dt5);
            //        for (int i = 0; i < dt5.Rows.Count; i++)
            //        {
            //            listBox2.Items.Add(dt5.Rows[i]["Owner_ID"]);
            //        }

            //        dt5.Clear();
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Profit Share") || (royaltyEntity.InterestType.Equals("Special")
            //        || (royaltyEntity.InterestType.Equals("Unleased Mineral Interest") || (royaltyEntity.InterestType.Equals("Net Profits")))))
            //    {

            //    }
            //}
            //else
            //if (EasementId != string.Empty)
            //{
            //    //////////////////////////////////////
            //    tract_IDTextBox2.Text = (comboBox1.Text.ToString());
            //    tract_IDcomboBox2.Text = comboBox1.Text.ToString();
            //    //////////////////////////////////////
            //    // Check SQL Connection
            //    if (sqlCon.State == ConnectionState.Closed)
            //        //        // And if closed, then open
            //        sqlCon.Open();
            //    //////////////////////////////////////
            //    if (royaltyEntity.InterestType.Equals(String.Empty))
            //    {
            //        ////
            //        //throw Exception.ReferenceEquals.ToString();
            //        MessageBox.Show("Please Select an Interest Type.");
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Royalty Interest") || (royaltyEntity.InterestType.Equals("ORRI")))
            //    {
            //        //SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM dbo.WIDeckMaster WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM dbo.WIDeckInterest WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        DataTable dt1 = new DataTable();
            //        sda1.Fill(dt1);
            //        for (int i = 0; i < dt1.Rows.Count; i++)
            //        {
            //            listBox3.Items.Add(dt1.Rows[i]["Owner_ID"]);
            //        }

            //        dt1.Clear();
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Excess Royalty"))
            //    {

            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Non-Participating Royalty Interest"))
            //    {
            //        SqlDataAdapter sda5 = new SqlDataAdapter("SELECT * FROM dbo.Royalty WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        //SqlDataAdapter sda5 = new SqlDataAdapter("SELECT * FROM dbo.Tract_OWnership WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        DataTable dt5 = new DataTable();
            //        sda5.Fill(dt5);
            //        for (int i = 0; i < dt5.Rows.Count; i++)
            //        {
            //            listBox2.Items.Add(dt5.Rows[i]["Owner_ID"]);
            //        }

            //        dt5.Clear();
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Profit Share") || (royaltyEntity.InterestType.Equals("Special")
            //        || (royaltyEntity.InterestType.Equals("Unleased Mineral Interest") || (royaltyEntity.InterestType.Equals("Net Profits")))))
            //    {

            //    }
            //}
            //else
            //if (SuaId != string.Empty)
            //{
            //    //////////////////////////////////////
            //    tract_IDTextBox2.Text = (comboBox1.Text.ToString());
            //    tract_IDcomboBox2.Text = comboBox1.Text.ToString();
            //    //////////////////////////////////////
            //    // Check SQL Connection
            //    if (sqlCon.State == ConnectionState.Closed)
            //        //        // And if closed, then open
            //        sqlCon.Open();
            //    //////////////////////////////////////
            //    if (royaltyEntity.InterestType.Equals(String.Empty))
            //    {
            //        ////
            //        //throw Exception.ReferenceEquals.ToString();
            //        MessageBox.Show("Please Select an Interest Type.");
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Royalty Interest") || (royaltyEntity.InterestType.Equals("ORRI")))
            //    {
            //        //SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM dbo.WIDeckMaster WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM dbo.WIDeckInterest WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        DataTable dt1 = new DataTable();
            //        sda1.Fill(dt1);
            //        for (int i = 0; i < dt1.Rows.Count; i++)
            //        {
            //            listBox3.Items.Add(dt1.Rows[i]["Owner_ID"]);
            //        }

            //        dt1.Clear();
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Excess Royalty"))
            //    {

            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Non-Participating Royalty Interest"))
            //    {
            //        SqlDataAdapter sda5 = new SqlDataAdapter("SELECT * FROM dbo.Royalty WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        //SqlDataAdapter sda5 = new SqlDataAdapter("SELECT * FROM dbo.Tract_OWnership WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        DataTable dt5 = new DataTable();
            //        sda5.Fill(dt5);
            //        for (int i = 0; i < dt5.Rows.Count; i++)
            //        {
            //            listBox2.Items.Add(dt5.Rows[i]["Owner_ID"]);
            //        }

            //        dt5.Clear();
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Profit Share") || (royaltyEntity.InterestType.Equals("Special")
            //        || (royaltyEntity.InterestType.Equals("Unleased Mineral Interest") || (royaltyEntity.InterestType.Equals("Net Profits")))))
            //    {

            //    }
            //}
            //else
            //if (RowId != string.Empty)
            //{
            //    //////////////////////////////////////
            //    tract_IDTextBox2.Text = (comboBox1.Text.ToString());
            //    tract_IDcomboBox2.Text = comboBox1.Text.ToString();
            //    //////////////////////////////////////
            //    // Check SQL Connection
            //    if (sqlCon.State == ConnectionState.Closed)
            //        //        // And if closed, then open
            //        sqlCon.Open();
            //    //////////////////////////////////////
            //    if (royaltyEntity.InterestType.Equals(String.Empty))
            //    {
            //        ////
            //        //throw Exception.ReferenceEquals.ToString();
            //        MessageBox.Show("Please Select an Interest Type.");
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Royalty Interest") || (royaltyEntity.InterestType.Equals("ORRI")))
            //    {
            //        //SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM dbo.WIDeckMaster WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM dbo.WIDeckInterest WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        DataTable dt1 = new DataTable();
            //        sda1.Fill(dt1);
            //        for (int i = 0; i < dt1.Rows.Count; i++)
            //        {
            //            listBox3.Items.Add(dt1.Rows[i]["Owner_ID"]);
            //        }

            //        dt1.Clear();
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Excess Royalty"))
            //    {

            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Non-Participating Royalty Interest"))
            //    {
            //        SqlDataAdapter sda5 = new SqlDataAdapter("SELECT * FROM dbo.Royalty WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        //SqlDataAdapter sda5 = new SqlDataAdapter("SELECT * FROM dbo.Tract_OWnership WHERE Tract_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            //        DataTable dt5 = new DataTable();
            //        sda5.Fill(dt5);
            //        for (int i = 0; i < dt5.Rows.Count; i++)
            //        {
            //            listBox2.Items.Add(dt5.Rows[i]["Owner_ID"]);
            //        }

            //        dt5.Clear();
            //    }
            //    else
            //    if (royaltyEntity.InterestType.Equals("Profit Share") || (royaltyEntity.InterestType.Equals("Special")
            //        || (royaltyEntity.InterestType.Equals("Unleased Mineral Interest") || (royaltyEntity.InterestType.Equals("Net Profits")))))
            //    {

            //    }
            //}
        }


        /// <summary>
        ///     WORKING INTERESTS LISTBOX
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void listBox3_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    ////////////////////////////////
        //    // SENDS SELECTIONS TO COMBOBOX
        //    ListBox.SelectedObjectCollection selectedownerIDs = listBox3.SelectedItems;
        //    foreach (string owner_ID in selectedownerIDs)
        //    {
        //        owner_IDcomboBox3.Items.Add(owner_ID);
        //        iDTextBox3.AppendText(owner_ID);
        //        listBox1.SelectedItems.Add(owner_ID);
        //    }


        //    ////
        //    ////
        //    //ComboBox.ObjectCollection selectedownerIDs1 = owner_IDcomboBox3.Items;
        //    //foreach (string owner_ID1 in selectedownerIDs1)
        //    //{
        //    //    owner_IDcomboBox3.Items.Add(owner_ID1);
        //    //    iDTextBox3.AppendText(owner_ID1);
        //    //}


        //    //Remove duplicates from LISTBOX array
        //    List<object> Ownerlist2 = new List<object>();
        //    foreach (object o in listBox1.SelectedItems)
        //    {
        //        if (!Ownerlist2.Contains(o))
        //        {
        //            Ownerlist2.Add(o);
        //        }
        //    }


        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //    //Remove duplicates from COMBOBOX array
        //    List<object> Ownerlist = new List<object>();
        //    foreach (object o1 in owner_IDcomboBox3.Items)
        //    {
        //        if (!Ownerlist.Contains(o1))
        //        {
        //            Ownerlist.Add(o1);
        //        }
        //    }


        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    ///

        //    associateLists.Clear();
        //    owner_IDcomboBox3.Items.AddRange(Ownerlist.ToArray());  // string does not show up at the '0' position

        //    ///
        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    ///

        //    ////associateLists.Clear();
        //    //owner_IDcomboBox3.GetItemText(Ownerlist.ToArray());     // doesnt work when "" is unCommented out, string does not show up at the '0' position          

        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    ///

        //    //associateLists.Clear();
        //    //owner_IDcomboBox3.SelectedItem.Equals(Ownerlist.ToArray());  // string does not show up at the '0' position
        //    //owner_IDcomboBox3.Text.Equals(Ownerlist.ToArray());  // string does not show up at the '0' position

        //    ///
        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    ///

        //    listBox1.Items.Clear();
        //    listBox1.Items.AddRange(Ownerlist.ToArray());


        //    ///
        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    ///

        //    iDTextBox3.Text.ToList();
        //    iDTextBox3.SelectedText.ToString();
        //    //iDTextBox3.SelectedText.ToList();
        //    //iDTextBox3.Text.Split();
        //    //iDTextBox3.SelectedText.Equals(Ownerlist.ToArray());
        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //}

        //private void interest_FormulaTextBox_Leave(object sender, EventArgs e)
        //{
        //    //// THAT OCCURS WHEN MORE THAN ONE DECIMAL IS ADDED TO ACREAGE FIELDS
        //    // decimal Tract_Total, Tract_Producing, Tract_Non_Producing, Tract_Other, Tract_Outside, interest_Formula, Producing, Non_Producing, Other, Outside, Total;

        //    if (interest_FormulaTextBox.Text.Contains('.'))
        //    {
        //        ////
        //        //decimal.TryParse(Tract_ProducingTextBox.Text, out Tract_Producing);
        //        //decimal.TryParse(Tract_Non_ProducingTextBox.Text, out Tract_Non_Producing);
        //        //decimal.TryParse(Tract_OtherTextBox.Text, out Tract_Other);
        //        //decimal.TryParse(Tract_OutsideTextBox.Text, out Tract_Outside);
        //        //decimal.TryParse(interest_FormulaTextBox.Text, out interest_Formula);
        //        //decimal.TryParse(tract_TotalTextBox1.Text, out Tract_Total);

        //        ////
        //        //decimal.TryParse(producingTextBox.Text, out Producing);
        //        //decimal.TryParse(non_ProducingTextBox.Text, out Non_Producing);
        //        //decimal.TryParse(otherTextBox.Text, out Other);
        //        //decimal.TryParse(outsideTextBox.Text, out Outside);
        //        //decimal.TryParse(totalTextBox.Text, out Total);

        //        // CALCULATIONS
        //        //Producing = formula * Tract_Producing;
        //        //Non_Producing = formula * Tract_Non_Producing;
        //        //Other = formula * Tract_Other;
        //        //Outside = formula * Tract_Outside;

        //        //
        //        //if (Tract_Producing > 0)
        //        //    producingTextBox.Text = (Producing.ToString());
        //        //if (Tract_Non_Producing > 0)
        //        //    non_ProducingTextBox.Text = (Non_Producing.ToString());
        //        //if (Tract_Other > 0)
        //        //    otherTextBox.Text = (Other.ToString());
        //        //if (Tract_Outside > 0)
        //        //    outsideTextBox.Text = (Outside.ToString());
        //        //// PASTES FORMULA TEXTBOX ENTRY INTO INTEREST TEXTBOX
        //        ////
        //        interestTextBox.Text = (interest_FormulaTextBox.Text.ToString());
        //        //Total = Other + Outside + Producing + Non_Producing;
        //        //totalTextBox.Text = (Total.ToString());
        //    }
        //    else if (interest_FormulaTextBox.Text.Contains('/'))
        //    {
        //        //decimal.TryParse(formulaTextBox.Text, out formula);
        //        //decimal.TryParse(tract_TotalTextBox1.Text, out Tract_Total);
        //        //Total = formula * Tract_Total;
        //        //if (Total > 0)
        //        //    totalTextBox.Text = (Total.ToString()); 
        //        //// PASTES FORMULA TEXTBOX ENTRY INTO INTEREST TEXTBOX
        //        ////
        //        interestTextBox.Text = (interest_FormulaTextBox.ToString());
        //    }
        //}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //////////////////////////////////////
            //if (royaltyEntity.InterestType.Equals(String.Empty))
            //{
            //    ////
            //    //throw Exception.ReferenceEquals.ToString();
            //    MessageBox.Show("Please Select an Interest Type before Tract ID.");
            //}
        }

        private void owner_IDcomboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlDataAdapter sda = new SqlDataAdapter("SELECT Full_Name FROM dbo.OwnerAddressesMain WHERE Owner_ID LIKE '" + comboBox1.Text + "%' ", sqlCon);
            ////  SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM dbo.OwnerAdressesMain WHERE Owner_ID = '" + comboBox1.Text + "%' ", sqlCon);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            ////
            ////
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //comboBox2.Items.Add(dt.Rows[i]["Full_Name"]);
            //comboBox2.Items.Add(dt);
            //comboBox2.Text.ToString(dt);  
            //}
        }

    }
}

