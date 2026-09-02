using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AuctoresOnline.API.Data;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Mappings;
using AuctoresOnline.API.Repositories.Interfaces;
using AuctoresOnline.API.Repositories.Implementations;
using AuctoresOnline.API.Services.Interfaces;
using AuctoresOnline.API.Services.Implementations;
using AuctoresOnline.API.Services.Interfaces.User;
using AuctoresOnline.API.Services.Implementations.User;

namespace AuctoresOnline.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // ─── AutoMapper ────────────────────────────────────────────────────────
        services.AddAutoMapper(typeof(MappingProfile));

        // ─── Database ──────────────────────────────────────────────────────────
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // ─── Helpers ───────────────────────────────────────────────────────────
        services.AddHttpContextAccessor();
        services.AddScoped<JwtHelper>();
        services.AddScoped<FileUploadHelper>();

        // ─── Repositories ──────────────────────────────────────────────────────
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IJournalRepository, JournalRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IEditorRepository, EditorRepository>();
        services.AddScoped<IEditorProfileRepository, EditorProfileRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IReviewerRepository, ReviewerRepository>();
        services.AddScoped<IBannerRepository, BannerRepository>();
        services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ITestimonialRepository, TestimonialRepository>();
        services.AddScoped<ISubscriberRepository, SubscriberRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IManuscriptRequestRepository, ManuscriptRequestRepository>();
        services.AddScoped<ISpecialIssueRepository, SpecialIssueRepository>();
        services.AddScoped<IApcRepository, ApcRepository>();
        services.AddScoped<IAbstractingIndexingRepository, AbstractingIndexingRepository>();
        services.AddScoped<IPubmedIndexRepository, PubmedIndexRepository>();
        services.AddScoped<IMemberedInRepository, MemberedInRepository>();
        services.AddScoped<IBecomeRequestRepository, BecomeRequestRepository>();

        // ─── Services (Business Layer) ─────────────────────────────────────────
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IJournalService, JournalService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IEditorService, EditorService>();
        services.AddScoped<IEditorProfileService, EditorProfileService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IReviewerService, ReviewerService>();
        services.AddScoped<IBannerService, BannerService>();
        services.AddScoped<ICollaboratorService, CollaboratorService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<ITestimonialService, TestimonialService>();
        services.AddScoped<ISubscriberService, SubscriberService>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IManuscriptService, ManuscriptService>();
        services.AddScoped<ISpecialIssueService, SpecialIssueService>();
        services.AddScoped<IApcService, ApcService>();
        services.AddScoped<IAbstractingIndexingService, AbstractingIndexingService>();
        services.AddScoped<IPubmedIndexService, PubmedIndexService>();
        services.AddScoped<IMemberedInService, MemberedInService>();
        services.AddScoped<IBecomeRequestService, BecomeRequestService>();

        // ─── User (Public) Services ────────────────────────────────────────────
        services.AddScoped<IUserHomeService, UserHomeService>();
        services.AddScoped<IUserJournalService, UserJournalService>();
        services.AddScoped<IUserArticleService, UserArticleService>();
        services.AddScoped<IUserSearchService, UserSearchService>();
        services.AddScoped<IUserMemberRequestService, UserMemberRequestService>();
        services.AddScoped<IUserSpecialIssueService, UserSpecialIssueService>();
        services.AddScoped<IUserDocumentService, UserDocumentService>();
        services.AddScoped<IUserSitemapService, UserSitemapService>();

        // ─── JWT Authentication ────────────────────────────────────────────────
        var jwtKey = configuration["JwtSettings:SecretKey"]!;
        var jwtIssuer = configuration["JwtSettings:Issuer"]!;
        var jwtAudience = configuration["JwtSettings:Audience"]!;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

        services.AddAuthorization();

        // ─── Controllers + Swagger ─────────────────────────────────────────────
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuctoresOnline Admin API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer {token}'",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                    Array.Empty<string>()
                }
            });
        });

        // ─── CORS ──────────────────────────────────────────────────────────────
        services.AddCors(options =>
            options.AddDefaultPolicy(policy =>
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

        return services;
    }
}
