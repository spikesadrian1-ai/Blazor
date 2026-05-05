using ApplicationLayer.Entities;
using ApplicationLayer.Entities.Locations.Prospects.Data;
using ApplicationLayer.Entities.TractOwnerships;
using ApplicationLayer.Interfaces.ApiCommands._Generics;
using ApplicationLayer.Interfaces.ApiCommands.Assets.LandProperty.Contract;
using ApplicationLayer.Interfaces.ApiCommands.Assets.LandProperty.Leases;
using ApplicationLayer.Interfaces.ApiCommands.Assets.LandProperty.Tracts;
using ApplicationLayer.Interfaces.ApiCommands.RelatedProperties.TractMineralOwnership;
using ApplicationLayer.Interfaces.ApiCommands.Locations.Counties;
using ApplicationLayer.Interfaces.ApiQueries._Generics;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Contract;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Leases;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Property;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Tract.TractMain;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Wells.AttachedTracts;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Wells.WellCrossRefs;
using ApplicationLayer.Interfaces.UnitOfWork;
using ApplicationLayer.Services.Assets.Leases;
using ApplicationLayer.Services.Assets.Leases.LeaseRelated;
using ApplicationLayer.Services.Assets.Tracts.Depths;
using ApplicationLayer.Services.Assets.Tracts.TractLeaseServices;
using ApplicationLayer.Services.Assets.Tracts.TractOwners;
using ApplicationLayer.Services.Assets.Tracts.TractStatus;
using ApplicationLayer.Services.DropdownLists;
using ApplicationLayer.Services.Locations.CountyServices;
using ApplicationLayer.Services.Owners.BusinessAssociateOwners.Associates;
using ApplicationLayer.Services.Owners.Filings;
using ApplicationLayer.Services.Owners.GranteesGrantors;
using ApplicationLayer.UseCases.Assets.Leases.CheckIfLeaseExists;
using ApplicationLayer.UseCases.Assets.Leases.CreateEntity;
using ApplicationLayer.UseCases.Assets.Leases.DeleteEntity;
using ApplicationLayer.UseCases.Assets.Leases.UpdateEntity;
using ApplicationLayer.UseCases.Owners.Associates.Addresses.NewAddressCreation;
using ApplicationLayer.UseCases.Owners.Associates.CheckIfOwnerExists;
using ApplicationLayer.UseCases.Owners.Associates.CreateEntity;
using ApplicationLayer.UseCases.Owners.Associates.DeleteEntity;
using ApplicationLayer.UseCases.Owners.Associates.UpdateEntity;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using ApplicationLayer.UseCases.RelatedProperties.TractMineralOwnership.CreateEntity;
using ApplicationLayer.Services.RelatedProperties.TractMineralOwnership;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Leases.LeaseMain;
using ApplicationLayer.Entities.WorkingInterests;
using ApplicationLayer.Interfaces.ApiCommands.RelatedProperties;
using ApplicationLayer.Services.RelatedProperties.WorkingInterest.WorkingInterestDetails;
using ApplicationLayer.UseCases.RelatedProperties.WorkingInterest.zWorkingInterestDetails.CreateEntity;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.MineralOwnership;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.WorkingInterest.zWorkingInterestDetails;
using ApplicationLayer.Entities.Burden;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.Burden;
using ApplicationLayer.Services.RelatedProperties.Burdens;
using ApplicationLayer.UseCases.RelatedProperties.Burdens.CreateEntity;
using ApplicationLayer.Entities.SpecialObligation;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.SpecialObligation;
using ApplicationLayer.Services.RelatedProperties.SpecialObligation;
using ApplicationLayer.UseCases.RelatedProperties.SpecialObligation.CreateEntity;
using ApplicationLayer.Entities.SpecialProvision;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.SpecialProvision;
using ApplicationLayer.Services.RelatedProperties.SpecialProvision;
using ApplicationLayer.UseCases.RelatedProperties.SpecialProvision.CreateEntity;
using ApplicationLayer.UseCases.RelatedProperties.PaymentObligation.CreateEntity;
using ApplicationLayer.Services.RelatedProperties.PaymentObligation;
using ApplicationLayer.Entities.Payments;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.PaymentObligation;
using ApplicationLayer.UseCases.RelatedProperties.WorkingInterest.CreateEntity;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.WorkingInterest;
using ApplicationLayer.Services.RelatedProperties.WorkingInterest;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Wells;
using ApplicationLayer.Services.Assets.Wells;
using ApplicationLayer.Interfaces.ApiCommands.Assets.LandProperty.Well;
using ApplicationLayer.Entities.Owners;
using ApplicationLayer.Interfaces.ApiQueries.Owners.BusinessAssociates.OwnerMain;
using ApplicationLayer.Interfaces.ApiQueries.Owners.Filings;
using ApplicationLayer.Entities.Filings;
using ApplicationLayer.Entities.FilingInformation;
using ApplicationLayer.Entities.Grantor_Grantees;
using ApplicationLayer.Interfaces.ApiQueries.Owners.GrantorsGrantees;
using ApplicationLayer.Entities.Checks;
using ApplicationLayer.Interfaces.ApiQueries.Accounting.Check;
using ApplicationLayer.Entities.Drafts;
using ApplicationLayer.Interfaces.ApiQueries.Accounting.Draft;
using ApplicationLayer.Interfaces.ApiQueries.Accounting.Payments;
using ApplicationLayer.Entities.NewPayments;
using ApplicationLayer.Services.Accounting.Payments;
using ApplicationLayer.Services.Accounting.Draft;
using ApplicationLayer.Services.Accounting.Check;
using ApplicationLayer.Entities.Contracts;
using ApplicationLayer.Services.Owners.ChainOfTitles;
using BlazorWebApp.Components.Shared.Components.Generic_Cross_Ref_Form;
using BlazorWebApp.Components.Pages.OG_Forms.Properties.Wells.WellTabs.SubInternals.WellCrossRefs.RelatedComponents;

namespace BlazorWebApp
{
    public class Startup
    {

        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.
        //// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //public void ConfigureServices(IServiceCollection services)
        //{

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    services.AddControllersWithViews(options =>
        //    {
        //        /// <summary>
        //        /// AZURE AD POLICY
        //        /// </summary>
        //        //var policy = new AuthorizationPolicyBuilder()
        //        //    .RequireAuthenticatedUser()
        //        //    .Build();
        //        //options.Filters.Add(new AuthorizeFilter(policy));


        //    });

        //    services.AddRazorPages();
        //    services.AddServerSideBlazor();
        //    //services.AddBlazoredModal();

        //    #region AZURE AUTH

        //    /// <summary>
        //    /// ADDING AZURE AD AUTHENTICATION
        //    /// TODO: FIX AZURE CONNECTION AFTER RENEWING ACCOUNT
        //    /// </summary>
        //    //services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
        //    //    .AddAzureAD(options => Configuration.Bind("AzureAd", options));

        //    #endregion AZURE AUTH

        //    #region HTTP

        //    #region ADDING HTTP CLIENT
        //    /// <summary>
        //    /// ADDING HTTP CLIENT
        //    /// </summary>
        //    services.AddHttpClient();

        //    //string Uri = Configuration.GetValue<string>("WebAPI"); MAY NOT NEED
        //    services.AddHttpClient("WebAPI", c =>
        //    {
        //        //c.BaseAddress = new Uri("http://localhost:7000/api/");
        //        //c.DefaultRequestHeaders.Add("Accept", "application/.json");

        //        /// OR
        //        //c.BaseAddress = new Uri(Configuration.GetValue<string>("WebAPI"));

        //        /// OR
        //        c.BaseAddress = new Uri(Configuration.GetConnectionString("API_URL"));

        //        //c.DefaultRequestHeaders.Accept.Clear();
        //        //c.DefaultRequestHeaders
        //        //.Accept
        //        //.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/.json"));




        //    });

        //    //services.AddHttpClient<IHttpClientBuilder,
        //    //               IHttpClientBuilder>(client =>
        //    // client.BaseAddress = new Uri(Configuration.GetSection("WebAPI").Value));

        //    #endregion ADDING HTTP CLIENT

        //    #region ADDING HTTP CONTENT

        //    //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>().AddOptions(
        //    //    (
        //    //        Options => options.
        //    //    ));
        //    //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

        //    #endregion ADDING HTTP CONTENT

        //    #endregion HTTP

        //    //services.AddScoped<ApplicationLayer.DependancyInjection>();
        //    services.AddScoped(typeof(ApplicationLayer.DependancyInjection));
        //    services.AddMediatR(typeof(ApplicationLayer.AssemblyReference).Assembly);

        //    //services.Scan(SelectorModel => SelectorModel
        //    //    .FromAssemblies(
        //    //        ///
        //    //        projectName.TheLayer(i.e.persistence).AssemblyReference.assembly,
        //    //        ///
        //    //        projectName.TheLayer(i.e.infrastructure).AssemblyReference.assembly
        //    //        )
        //    //    .AddClasses(false)
        //    //    .AsImplementationInterfaces()
        //    //    .WithScopedLifetime());

        //    //services.AddControllers()
        //    //    .AddApplicationPart(projectName.TheLayer(i.e.presentation).AssemblyReference.assembly);

        //    #region JSON

        //    ///// <summary>
        //    ///// JSON SERIALIZER
        //    ///// </summary>
        //    //services.AddControllersWithViews().AddNewtonsoftJson(options =>
        //    ////services.AddControllersWithViews().(options =>
        //    //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
        //    //    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
        //    //    = new DefaultContractResolver());


        //    /////
        //    /// JSON SERIALIZER
        //    ///                
        //    //services.AddControllersWithViews()
        //    ////    .AddJsonOptions
        //    ////    (options =>
        //    ////options.JsonSerializerOptions.DictionaryKeyPolicy = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
        //    //    .AddJsonOptions
        //    //    (options => options.JsonSerializerOptions.ReferenceHandler
        //    //    = System.Text.Json.Serialization.ReferenceHandler.Preserve);


        //    services.AddControllersWithViews()
        //        .AddJsonOptions(jsonOptions =>
        //        {
        //            jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;  // Pascal Case
        //        });


        //    #endregion JSON

        //    #region SCOPED APPLICATION LAYER SERVICES

        //    #region CONSTANTS

        //    services.AddScoped<IDropdownQueryServiceTasks, DropdownQueryServiceTasks>();

        //    #endregion CONSTANTS

        //    #region ASSETS

        //    #region CONTRACTS

        //    services.AddScoped<IContractDropdowns, ContractDropdowns>();
        //    services.AddScoped<ContractApiCommands>();
        //    #endregion CONTRACTS

        //    #region LEASES
        //    services.AddScoped<IPopulateForm<LeaseMainForm2>, PopulateLeaseForm>();
        //    services.AddScoped<LeaseApiCommands>();
        //    services.AddScoped<LeaseMainServices>();
        //    services.AddScoped<DoesLeaseExist>();
        //    services.AddScoped<CreateLease>();
        //    services.AddScoped<DeleteLease>();
        //    services.AddScoped<UpdateLease>();
        //    services.AddScoped<LeaseDataGridSearchs>();
        //    services.AddScoped<LeaseRelatedPageServices>();
        //    services.AddScoped</*ILeasePopulateDataGrids,*/ LeasePopulateDataGrids>();
        //    #endregion LEASES

        //    #region PROPERTY

        //    #endregion PROPERTY

        //    #region PROSPECTS
        //    services.AddScoped<IPropertyDropdowns, PropertyDropdowns>();
        //    services.AddScoped<TractsWithinProspect, TractsWithinProspect>();
        //    #endregion PROSPECTS

        //    #region TRACTS

        //    services.AddScoped<IPopulateForm<TractMainForm>, PopulateTractForm>();
        //    services.AddScoped<ISaveFormByEntityID<TractMainForm>, TractApiCommands>();
        //    services.AddScoped<TractDepthsSearchServices>();           
        //    services.AddScoped<IStatusOfTracts, StatusOfTracts>();

        //    #endregion TRACTS

        //    #region WELLS
        //    services.AddScoped<GenericCrossRefForm<ContractMainForm2>, RelatedContracts<ContractMainForm2>>();
        //    services.AddScoped<IPopulateForm<WellMainForm2>, PopulateWellForm>();
        //    services.AddScoped<IWellServices, WellServices>();
        //    services.AddScoped<IWellAPiCommands, WellAPiCommands>();
        //    services.AddScoped<WellCrossRefs>();
        //    services.AddScoped</*IWellPopulateDatagrids,*/ WellPopulateDatagrids>();
        //    #endregion WELLS

        //    #endregion ASSETS

        //    #region ACCOUNTING
            
        //        #region CHECKS

        //        services.AddScoped<IPopulateForm<CheckMasterMainForm>, PopulateCheckForm>();
        //        services.AddScoped<ICheckServices, CheckServices>();

        //        #endregion CHECKS

        //        #region DRAFTS

        //        services.AddScoped<IPopulateForm<DraftMasterMain>, PopulateDraftForm>();
        //        services.AddScoped<IDraftServices, DraftServices>();

        //        #endregion DRAFTS

        //        #region PAYMENTS

        //        services.AddScoped<IPopulateForm<NewPayment>, PopulatePaymentForm>();
        //        services.AddScoped<IPaymentServices, PaymentServices>();

        //        #endregion PAYMENTS

        //    #endregion ACCOUNTING

        //    #region LOCATIONS

        //    #region COUNTIES

        //    services.AddScoped<ICountyApiCommands, CountyApiCommands>();
        //    services.AddScoped<ICountyQueryServiceTasks, CountyQueryServiceTasks>();

        //    #endregion COUNTIES

        //    #region DISTRICTS

        //    #endregion DISTRICTS

        //    #region REGIONS

        //    #endregion REGIONS

        //    #region STATES

        //    #endregion STATES

        //    #endregion LOCATIONS

        //    #region OWNERS

        //    #region BUSINESS / OWNERS / ASSOCIATES
        //    services.AddScoped<IPopulateForm<OwnerAddressesMain>, PopulateOwnerForm>();

        //    services.AddScoped<BusinessAssociates>();
        //    services.AddScoped<DoesOwnerExist>();
        //    services.AddScoped<CreateOwner>();
        //    services.AddScoped<DeleteOwner>();
        //    services.AddScoped<UpdateOwner>();
        //    #endregion BUSINESS / OWNERS / ASSOCIATES

        //    #region ADDRESSES
        //    services.AddScoped<CreateNewAddress>();

        //    #endregion ADDRESSES

        //    #region FILING INFO
        //    services.AddScoped<IPopulateForm<Recordings>, PopulateFilingForm>();
        //    services.AddScoped<FilingInformationServices>();

        //    #endregion FILING INFO

        //    #region GRANTORS AND GRANTEES

        //    services.AddScoped<IPopulateForm<GrantorsGrantees>, PopulateGrantorForm>();
        //    services.AddScoped<IGranteesGrantorsServices, GranteesGrantorsServices>();

        //    #endregion GRANTORS AND GRANTEES

        //    #region CHAIN OF TITLES

        //    //services.AddScoped<IPopulateForm<GrantorsGrantees>, PopulateGrantorForm>();
        //    services.AddScoped<ChainOfTitleServices>();

        //    #endregion CHAIN OF TITLES

        //    #endregion OWNERS

        //    #region RELATED PROPERTIES

        //    #region BURDENS
        //    services.AddScoped<IPopulateForm<Burdens>, PopulateBurdenForm>();
        //    services.AddScoped<BurdensApiCommands>();
        //    services.AddScoped<IBurdenServices, BurdenServices>();
        //    services.AddScoped<CreateBurden>();
        //    #endregion BURDENS

        //    #region PAYMENT OBLIGATIONS
        //    services.AddScoped<IPopulateForm<PaymentObligations>, PopulatePaymentObligationForm>();
        //    services.AddScoped<PaymentObligationApiCommands>();
        //    services.AddScoped<IPaymentObligationServices, PaymentObligationServices>();
        //    services.AddScoped<CreatePaymentObligation>();
        //    #endregion PAYMENT OBLIGATIONS

        //    #region SPECIAL OBLIGATIONS
        //    services.AddScoped<IPopulateForm<SpecialObligations>, PopulateSpecialObligationForm>();
        //    services.AddScoped<SpecialObligationApiCommands>();
        //    services.AddScoped<ISpecialObligationServices, SpecialObligationServices>();
        //    services.AddScoped<CreateSpecialObligation>();
        //    #endregion SPECIAL OBLIGATIONS

        //    #region SPECIAL PROVISIONS
        //    services.AddScoped<IPopulateForm<SpecialProvisions>, PopulateSpecialProvisionForm>();
        //    services.AddScoped<SpecialProvisionApiCommands>();
        //    services.AddScoped<ISpecialProvisionServices, SpecialProvisionServices>();
        //    services.AddScoped<CreateSpecialProvision>();
        //    #endregion SPECIAL PROVISIONS

        //    #region TRACT OWNERSHIP
        //    services.AddScoped<IPopulateForm<TractOwnership>, PopulateMineralTractOwnershipForm>();
        //    services.AddScoped<MineralOwnershipApiCommands>();
        //    services.AddScoped<ITractMineralOwnershipServices, TractMineralOwnershipServices>();
        //    services.AddScoped<TractLeaseServiceTasks>();
        //    services.AddScoped<ITractOwnersQueryServiceTasks, TractOwnersQueryServiceTasks>();
        //    services.AddScoped<CreateMineralOwnership>();
        //    #endregion TRACT OWNERSHIP

        //    #region WORKING INTEREST MAIN
        //    services.AddScoped<IPopulateForm<WideckMaster>, PopulateWorkingInterestForm>();
        //    services.AddScoped<WorkingInterestApiCommands>();
        //    services.AddScoped<IWorkingInterestServices, WorkingInterestServices>();
        //    services.AddScoped<CreateWorkingInterest>();
        //    #endregion WORKING INTEREST MAIN 

        //    #region WORKING INTEREST DETAILS
        //    services.AddScoped<IPopulateForm<WideckDetails>, PopulateWorkingInterestsDetailsForm>();
        //    services.AddScoped<WorkingInterestDetailApiCommands>();
        //    services.AddScoped<IWorkingInterestDetailsServices, WorkingInterestDetailsServices>();
        //    services.AddScoped<CreateWorkingInterestDetails>();
        //    #endregion WORKING INTEREST DETAILS

        //    #endregion RELATED PROPERTIES

        //    #endregion SCOPED APPLICATION LAYER SERVICES


        //    #region CORS

        //    /// <summary>
        //    /// ENABLE CORS
        //    /// </summary>
        //    services.AddCors(c =>
        //    {
        //        c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        //    });

        //    #endregion CORS

        //}

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{

        //    app.UseCors(options => options
        //            .AllowAnyOrigin()
        //            .AllowAnyMethod()
        //            .AllowAnyHeader());


        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Error");
        //        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //        app.UseHsts();
        //    }

        //    app.UseHttpsRedirection();
        //    app.UseStaticFiles();

        //    app.UseRouting();

        //    app.UseAuthentication();
        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //        endpoints.MapBlazorHub();
        //        endpoints.MapFallbackToPage("/_Host");
        //    });
        //}
    }
}

