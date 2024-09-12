using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("GlobalConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Stripe API     It is not needed
//builder.Services.AddControllersWithViews();
//builder.Services.AddControllers();
//builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
//builder.Services.AddSingleton<StripeService>();
// THIS isn't needed
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//Using this 
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVisitorCountService, VisitorCountService>();

builder.Services.Configure<DataProtectionTokenProviderOptions>
    (opts => opts.TokenLifespan = TimeSpan.FromHours(1));


builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequiredLength = 8;
});

builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.LoginPath = "/Account/Login";
    opts.AccessDeniedPath = "/Door/Bouncer";

    opts.Cookie.Name = ".AspNetCore.Identity.Application";
    opts.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    opts.SlidingExpiration = true;
});


builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWeatherApiHandler, WeatherApiHandler>();
builder.Services.AddScoped<ICurrencyExchangeRateService, CurrencyExchangeRateService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISubscriptionStatisticsService, SubscriptionStatisticsService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IFillObjectHelper, FillObjectHelper>();
builder.Services.AddScoped<ISubscriptionStatisticsService, SubscriptionStatisticsService>();


//builder.Services.AddTransient<IEmailSender, EmailHelper>();



builder.Services.AddControllersWithViews();

builder.Services.AddMvc();

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
