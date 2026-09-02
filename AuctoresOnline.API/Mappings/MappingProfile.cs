using AutoMapper;
using AuctoresOnline.API.Helpers;
using AuctoresOnline.API.Models.Article;
using AuctoresOnline.API.Models.Author;
using AuctoresOnline.API.Models.Editor;
using AuctoresOnline.API.Models.EditorProfile;
using AuctoresOnline.API.Models.Journal;
using AuctoresOnline.API.Models.Misc;
using AuctoresOnline.API.Models.Reviewer;

namespace AuctoresOnline.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ─── Editor ────────────────────────────────────────────────────────────
        CreateMap<CreateEditorRequest, EditorDto>()
            .ForMember(d => d.EditorId, opt => opt.Ignore())
            .ForMember(d => d.EditorStatus, opt => opt.Ignore())
            .ForMember(d => d.EditorCreatedDate, opt => opt.Ignore())
            .ForMember(d => d.EditorUpdatedDate, opt => opt.Ignore())
            .ForMember(d => d.ProfileImage, opt => opt.Ignore())
            .ForMember(d => d.EditorPassword, opt =>
            {
                opt.Condition(src => !string.IsNullOrEmpty(src.EditorPassword));
                opt.MapFrom(src => Md5Helper.Hash(src.EditorPassword!));
            });

        // ─── Editor Profile ────────────────────────────────────────────────────
        CreateMap<CreateEditorProfileRequest, EditorProfileDto>()
            .ForMember(d => d.EditorId, opt => opt.Ignore())
            .ForMember(d => d.EditorStatus, opt => opt.Ignore())
            .ForMember(d => d.EditorCreatedDate, opt => opt.Ignore())
            .ForMember(d => d.EditorUpdatedDate, opt => opt.Ignore())
            .ForMember(d => d.EditorImage, opt => opt.Ignore())
            .ForMember(d => d.EditorPassword, opt =>
            {
                opt.Condition(src => !string.IsNullOrEmpty(src.EditorPassword));
                opt.MapFrom(src => Md5Helper.Hash(src.EditorPassword!));
            });

        // ─── Author ────────────────────────────────────────────────────────────
        CreateMap<CreateAuthorRequest, AuthorDto>()
            .ForMember(d => d.AuthorId, opt => opt.Ignore())
            .ForMember(d => d.AuthorStatus, opt => opt.Ignore())
            .ForMember(d => d.AuthorCreatedDate, opt => opt.Ignore())
            .ForMember(d => d.AuthorUpdatedDate, opt => opt.Ignore())
            .ForMember(d => d.AuthorImage, opt => opt.Ignore())
            .ForMember(d => d.AuthorPassword, opt =>
            {
                opt.Condition(src => !string.IsNullOrEmpty(src.AuthorPassword));
                opt.MapFrom(src => Md5Helper.Hash(src.AuthorPassword!));
            });

        // ─── Reviewer ──────────────────────────────────────────────────────────
        CreateMap<CreateReviewerRequest, ReviewerDto>()
            .ForMember(d => d.ReviewerId, opt => opt.Ignore())
            .ForMember(d => d.ReviewerStatus, opt => opt.Ignore())
            .ForMember(d => d.ReviewerCreatedDate, opt => opt.Ignore())
            .ForMember(d => d.ReviewerUpdatedDate, opt => opt.Ignore())
            .ForMember(d => d.ReviewerImage, opt => opt.Ignore())
            .ForMember(d => d.ReviewerPassword, opt =>
            {
                opt.Condition(src => !string.IsNullOrEmpty(src.ReviewerPassword));
                opt.MapFrom(src => Md5Helper.Hash(src.ReviewerPassword!));
            });

        // ─── Journal ───────────────────────────────────────────────────────────
        CreateMap<CreateJournalRequest, JournalDto>()
            .ForMember(d => d.JournalId, opt => opt.Ignore())
            .ForMember(d => d.JournalStatus, opt => opt.Ignore())
            .ForMember(d => d.JournalCreatedDate, opt => opt.Ignore())
            .ForMember(d => d.JournalUpdatedDate, opt => opt.Ignore())
            .ForMember(d => d.JournalImage, opt => opt.Ignore())
            .ForMember(d => d.IndexingImage, opt => opt.Ignore())
            .ForMember(d => d.CrossrefImage, opt => opt.Ignore());

        // ─── Article ───────────────────────────────────────────────────────────
        CreateMap<CreateArticleRequest, ArticleDto>()
            .ForMember(d => d.ArticleId, opt => opt.Ignore())
            .ForMember(d => d.ArticleStatus, opt => opt.Ignore())
            .ForMember(d => d.ArticleCreatedDate, opt => opt.Ignore())
            .ForMember(d => d.ArticleUpdatedDate, opt => opt.Ignore())
            .ForMember(d => d.ArticlePdf, opt => opt.Ignore())
            .ForMember(d => d.ArticleViews, opt => opt.Ignore())
            .ForMember(d => d.ArticleDownloads, opt => opt.Ignore())
            .ForMember(d => d.JournalName, opt => opt.Ignore())
            .ForMember(d => d.JournalSeoName, opt => opt.Ignore());

        // ─── Banner ────────────────────────────────────────────────────────────
        CreateMap<CreateBannerRequest, BannerDto>()
            .ForMember(d => d.BannerId, opt => opt.Ignore())
            .ForMember(d => d.BannerStatus, opt => opt.Ignore())
            .ForMember(d => d.BannerImage, opt => opt.Ignore())
            .ForMember(d => d.CreatedDate, opt => opt.Ignore())
            .ForMember(d => d.UpdatedDate, opt => opt.Ignore());

        // ─── Collaborator ──────────────────────────────────────────────────────
        CreateMap<CreateCollaboratorRequest, CollaboratorDto>()
            .ForMember(d => d.CollaboratorId, opt => opt.Ignore())
            .ForMember(d => d.CollaboratorStatus, opt => opt.Ignore())
            .ForMember(d => d.CollaboratorImage, opt => opt.Ignore())
            .ForMember(d => d.CreatedDate, opt => opt.Ignore())
            .ForMember(d => d.UpdatedDate, opt => opt.Ignore());

        // ─── Testimonial ───────────────────────────────────────────────────────
        CreateMap<CreateTestimonialRequest, TestimonialDto>()
            .ForMember(d => d.TestimonialId, opt => opt.Ignore())
            .ForMember(d => d.TestimonialStatus, opt => opt.Ignore())
            .ForMember(d => d.TestimonialImage, opt => opt.Ignore())
            .ForMember(d => d.TestimonialCreatedDate, opt => opt.Ignore())
            .ForMember(d => d.TestimonialUpdatedDate, opt => opt.Ignore());
    }
}
