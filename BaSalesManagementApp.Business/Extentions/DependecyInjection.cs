namespace BaSalesManagementApp.Business.Extentions
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IMailService, MailService>();            
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStockTypeService, ProductTypeService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IQrService, QrService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IStockTypeSizeService, StockTypeSizeService>();
            services.AddScoped<IUserCardService, UserCardService>();
            services.AddScoped<IPasswordGeneratorService, PasswordGeneratorService>();
            services.AddScoped<RecaptchaService>();
            services.AddHttpClient<RecaptchaService>();


            // Notification service parameters
            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 5;
                config.IsDismissable = true;
                config.HasRippleEffect = true;
                config.Position = NotyfPosition.BottomRight;
            });
            return services;
        }
    }
}