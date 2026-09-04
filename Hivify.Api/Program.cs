using Association.Application;
using Association.Application.Contracts;
using Association.Infrastructure;
using Association.Infrastructure.Persistence;
using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Messeging;
using BuildingBlocks.Infrastructure.CurrentUserProvider;
using BuildingBlocks.Infrastructure.Messeging;
using BuildingBlocks.Infrastructure.Storage;
using BuildingBlocks.Infrastructure.Storage.CloudinaryStorage;
using Complaints.Application;
using Complaints.Application.Contracts;
using Complaints.Infrastructure.Presistence;
using DocumentsMgmt.Application;
using Feeds.Application;
using Feeds.Application.Contracts;
using Feeds.Infrastructure.Presistence;
using Houses.Application;
using Houses.Application.Contracts;
using Houses.Infrastructure.Presistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserMgmt.Application;
using UserMgmt.Application.Contracts;
using UserMgmt.Infrastructure.Identity;
using UserMgmt.Infrastructure.Presistence;

var builder = WebApplication.CreateBuilder(args);

#region API

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
#endregion


#region Database

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContextFactory<HouseDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddDbContextFactory<FeedDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddDbContextFactory<ComplaintDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddDbContextFactory<UserManagementDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

#endregion




#region Authentication & Authorization

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

builder.Services.AddAuthorization();

#endregion


#region Identity

builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;

        options.Stores.SchemaVersion =
            IdentitySchemaVersions.Version3;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<UserManagementDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserDirectory, UserDirectory>();

builder.Services.AddSingleton<
    IEmailSender<ApplicationUser>,
    IdentityNoOpEmailSender>();

#endregion


#region Infrastructure

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUserProvider>();
builder.Services.AddScoped<IFeedRepo, FeedRepo>();
builder.Services.AddScoped<IComplaintRepo, ComplaintRepo>();
builder.Services.AddScoped<IAssociationRepo, AssociationRepo>();
builder.Services.AddScoped<IHouseRepo, HouseRepo>();

#endregion


#region Application

builder.Services.AddScoped<ISender, Sender>();
builder.Services.AddScoped<IQuerySender, QuerySender>();


builder.Services.AddFeedServices();
builder.Services.AddHouseServices();
builder.Services.AddUserMgmtServices();
builder.Services.AddComplaintServices();
builder.Services.AddDocumentServices();

#endregion



builder.Services.AddAssociationServices();
builder.Services.AddAssociationInfrastructure(connectionString);
#region Storage

builder.Services.Configure<CloudinaryOptions>(
    builder.Configuration.GetSection("Cloudinary"));

builder.Services.AddStorageServices();

#endregion


#region AI

builder.Services.AddHivifyAIServices();

#endregion


var app = builder.Build();


#region HTTP Pipeline

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion


app.Run();
